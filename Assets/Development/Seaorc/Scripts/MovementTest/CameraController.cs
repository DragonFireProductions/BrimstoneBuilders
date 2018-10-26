using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Assets.Meyer.TestScripts;
using Assets.Meyer.TestScripts.Player;

using UnityEngine;
using UnityEngine.Assertions;


public class CameraController : MonoBehaviour
{

    [Header("Transforms")]
    [SerializeField] Transform PlayerTransform;
    [SerializeField] Transform CamRig;
    [SerializeField] Transform Playercam;
    [SerializeField] Transform ColonyCam;

    [Header("Basic Settings")]
    [SerializeField] float CamSpeed;
    [SerializeField] float TransitionSpeed;
    [SerializeField] float MouseSensitivity;

    [Header("Zoom Settings")]
    [SerializeField] float ColonyWidth;
    [SerializeField] float ColonyHight;
    [SerializeField] float MaxZoom;
    [SerializeField] float MinZoom;

    CameraMode mode;
    float Zoom;

    private float timer;

    public static CameraController controller;

    private bool isColony = false;

    private bool switched = false;
    void Awake()
    {
        controller = this;
    }
    // Use this for initialization
    void Start()
    {
        if (controller == null)
        {
            controller = this;
        }
        else if (controller != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        SwitchMode( CameraMode.Player );

        CamRig = transform.parent.parent.transform;

        Playercam = transform.parent.transform;
        
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        ColonyCam = GameObject.Find("ColonyCam").transform;
        Assert.IsNotNull(ColonyCam, "ColonyCam cannot be found!");
    }

    // Update is called once per frame
    void Update() {

        timer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Tab)){
            isColony = !isColony;
            switched = true;
        }

        if ( isColony && switched ){
            switched = false;
            SwitchMode(CameraMode.Colony);
        }

        if ( !isColony && switched ){
            switched = false;
            SwitchMode(CameraMode.Player);

        }
        if (mode == CameraMode.Colony)
        {
            if (Input.GetKey(KeyCode.Mouse2))
            {
                float X = -Input.GetAxis("Mouse X");
                X *= 100;

                float Y = -Input.GetAxis("Mouse Y");
                Y *= 100;

                transform.Translate(X * Time.deltaTime, Y * Time.deltaTime, 0);
            }
            else
            {
                float X = Input.GetAxis("Horizontal");
                X *= CamSpeed;
                float Y = Input.GetAxis("Vertical");
                Y *= CamSpeed;

                transform.Translate(X * Time.deltaTime, Y * Time.deltaTime, 0);
            }

            Zoom += Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * MouseSensitivity;
            Zoom = Mathf.Clamp(Zoom, -MaxZoom, MinZoom);

            if (transform.position.x > ColonyWidth)
            {
                transform.position = new Vector3(ColonyWidth, transform.position.y, transform.position.z);
            }
            else if (transform.position.x < -ColonyWidth)
            {
                transform.position = new Vector3(-ColonyWidth, transform.position.y, transform.position.z);
            }

            if (transform.position.z > ColonyHight)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, ColonyHight);
            }
            else if (transform.position.z < -ColonyHight)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -ColonyHight);
            }

            transform.position = new Vector3(transform.position.x, ColonyCam.position.y + Zoom, transform.position.z);
        }
        if (mode == CameraMode.Player)
        {
            CamRig.position = PlayerTransform.position;

            Ray cameraray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane groundplane = new Plane(Vector3.up, Vector3.zero);
            float raylength;


            if (groundplane.Raycast(cameraray, out raylength))
            {
                Vector3 pointtolook = cameraray.GetPoint(raylength);

                PlayerTransform.LookAt(new Vector3(pointtolook.x, PlayerTransform.position.y, pointtolook.z));
            }

            float Rotation = 0;

            if (Input.GetKey(KeyCode.Mouse2))
            {
                Rotation = Input.GetAxis("Mouse X");
                Rotation *= MouseSensitivity;
            }
            else if (Input.GetKey(KeyCode.Q))
                Rotation = -MouseSensitivity;
            else if (Input.GetKey(KeyCode.E))
                Rotation = MouseSensitivity;

            Rotation *= Time.deltaTime;

            CamRig.Rotate(0, Rotation, 0);

            Zoom += Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * MouseSensitivity;
            Zoom = Mathf.Clamp(Zoom, -MinZoom, MaxZoom);

            transform.localPosition = new Vector3(0, 0, Zoom);
        }

        if (mode == CameraMode.Battle)
        {
            
        }

    }

    public void UpdatePlayer(GameObject player)
    {
        PlayerTransform = player.transform;
    }
   public void SwitchMode(CameraMode _mode, Companion companion = null)
    {
        if (_mode == CameraMode.Player)
        {
            StartCoroutine(ToPlayer());
        }
        else if (_mode == CameraMode.Colony)
        {
            StaticManager.character.controller.SetControlled(false);
            StartCoroutine(ToColony());
        }
        else if ( _mode == CameraMode.Battle ){
            StartCoroutine( ToBattle(companion ) );
        }
        else if ( _mode == CameraMode.ToOtherPlayer ){

            StartCoroutine( ToAnotherPlayer( companion ) );
        }
        else if ( _mode == CameraMode.Freeze ){
            mode = CameraMode.Battle;
        }
    }

    IEnumerator ToColony()
    {
        mode = CameraMode.Transition;

        var CamPos = new Vector3(Character.player.transform.position.x, ColonyCam.position.y, Character.player.transform.position.z);
        while (Vector3.Distance(transform.position, ColonyCam.position) > .5f)
        {
            transform.position = Vector3.Lerp(transform.position, CamPos, TransitionSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, ColonyCam.rotation, TransitionSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        transform.position = ColonyCam.position;
        transform.rotation = ColonyCam.rotation;
        Zoom = 0;
        mode = CameraMode.Colony;
       StaticManager.character.controller.SetControlled(false);
        
    }
    IEnumerator ToPlayer()
    {
        mode = CameraMode.Transition;
        Zoom = 0;
        var pos = Playercam.position;
        var rot = Playercam.rotation;
        while (Vector3.Distance(transform.position, Playercam.position) > .5f)
        {

            transform.position = Vector3.Lerp(transform.position, pos, TransitionSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, TransitionSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        transform.position = Playercam.position;
        transform.rotation = Playercam.rotation;

        mode = CameraMode.Player;
    }

  IEnumerator ToBattle(Companion companion ) {
        mode = CameraMode.Transition;
        Zoom = 0;

        while (Vector3.Distance(transform.position, companion.CamHolder.transform.position) > 0.4f ){
            var position = Vector3.Lerp(transform.position, companion.camHolder.transform.position, 0.3f * Time.deltaTime);
            transform.position = position;
            transform.rotation = Quaternion.Slerp(transform.rotation, companion.camHolder.transform.rotation, Time.deltaTime * 0.3f);

            yield return new WaitForEndOfFrame();
        }

        var r = Quaternion.LookRotation((Character.player.transform.position + (Character.player.transform.forward * 10)) - transform.position);
        timer = 0;
        while (transform.rotation != r && timer < 2.0f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, r, 5 * Time.deltaTime);
            timer += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }
        if (timer >= 2.0f)
        {
            transform.LookAt(Character.player.transform.position + (Character.player.transform.forward * 10));
        }

        StaticManager.character.controller.SetControlled( true );
        mode = CameraMode.Battle;

    }

    IEnumerator ToPlayerBattle(Companion companion ) {
        mode = CameraMode.Transition;
        Zoom = 0;

        while (Vector3.Distance(transform.position, companion.camHolder.transform.position) > 0.4f)
        {
            var position = Vector3.Lerp(transform.position, companion.camHolder.transform.position, TransitionSpeed * Time.deltaTime);
            transform.position = position;
            transform.rotation = Quaternion.Slerp(transform.rotation, companion.camHolder.transform.rotation, Time.deltaTime * TransitionSpeed);

            yield return new WaitForEndOfFrame();
        }
        
        mode = CameraMode.Battle;
    }


    IEnumerator ToAnotherPlayer(Companion companion ) {
        mode = CameraMode.Transition;
        Zoom = 0;
        timer = 0;
        var pos = companion.CamHolder.transform.position;

       

        while ( Vector3.Distance( transform.position , pos ) > 0.4f && timer < 2.0f )
        {
            var position = Vector3.Lerp(transform.position,pos , TransitionSpeed * Time.deltaTime);
            transform.position = position;
           
            yield return new WaitForEndOfFrame();
        }
       
        if (timer >= 2.0f)
        {
            transform.position = companion.CamHolder.transform.position;
        }
       
        var r = Quaternion.LookRotation( ( Character.player.transform.position + (Character.player.transform.forward * 10) ) - transform.position );
        timer = 0;
        while (transform.rotation != r && timer < 2.0f)
        {
            transform.rotation =  Quaternion.Slerp(transform.rotation, r, 5 * Time.deltaTime);
            timer                    += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }
        if (timer >= 2.0f)
        {
            transform.LookAt(Character.player.transform.position + (Character.player.transform.forward * 10));
        }
       


        mode = CameraMode.Battle;
    }
    public CameraMode Mode {
        get { return mode; }
        set { mode = value;  }
    }

}

public enum CameraMode
{
    Colony, Player, Transition, Battle, ToOtherPlayer, Freeze
}
