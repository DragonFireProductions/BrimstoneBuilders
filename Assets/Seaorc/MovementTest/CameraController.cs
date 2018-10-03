using System.Collections;
using System.Collections.Generic;

using Assets.Meyer.TestScripts.Player;

using UnityEngine;
using UnityEngine.Assertions;

public class CameraController : MonoBehaviour
{

    [Header("Transforms")]
    [SerializeField] PlayerController Player;
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

    public static CameraController controller;

    void Awake( ) {
        controller = this;
    }
    // Use this for initialization
    void Start() {

        mode = CameraMode.Player;

        CamRig = transform.parent.parent.transform;

        Playercam = transform.parent.transform;

        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        Assert.IsNotNull(Player, "Cannot find object with tag of player");
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        ColonyCam = GameObject.Find("ColonyCam").transform;
        Assert.IsNotNull(ColonyCam, "ColonyCam cannot be found!");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchMode();
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

        if ( mode == CameraMode.EnemyAttacking ){

        }

        if ( mode == CameraMode.PlayerAttacking ){
            
        }
       
    }

    void SwitchMode()
    {
        if (mode == CameraMode.Colony)
        {
            StartCoroutine(ToPlayer());
        }
        else
        {
            Player.SetControlled(false);
            StartCoroutine(ToColony());
        }
    }

    public void PlayerAttack_Switch( ) {
        if ( TurnBased.Instance.IsPlayerTurn ){
            mode = CameraMode.Transition;
            StartCoroutine( ToPlayer_Attack( ) );
        }
    }

    public void EnemyAttack_Switch(GameObject Enemy ) {
        if ( !TurnBased.Instance.IsPlayerTurn ){
            mode = CameraMode.Transition;

            StartCoroutine( ToEnemy_Attack( Enemy ) );
        }
    }

    IEnumerator ToPlayer_Attack() {

        while (Vector3.Distance(transform.position,  Character.player.transform.position + Character.player.transform.right * 5 ) > 10f){
            transform.position = Vector3.Lerp( transform.position, Character.player.transform.position, 10 * Time.deltaTime);
            transform.LookAt( Character.player.transform );

            yield return new WaitForEndOfFrame( );
        }
        yield return new WaitForSeconds(2);

        mode = CameraMode.PlayerAttacking;
    }

    IEnumerator ToEnemy_Attack(GameObject enemy ) {
        mode = CameraMode.Transition;

        while (transform.position !=  enemy.transform.position + enemy.transform.forward * 5){
            //transform.position = Vector3.Lerp(transform.position, enemy.transform.position + enemy.transform.forward * 5, 15 * Time.deltaTime);
            //transform.LookAt(enemy.transform);
            yield return new WaitForEndOfFrame();;
        }

        mode = CameraMode.EnemyAttacking;
    }

    IEnumerator ToColony()
    {
        mode = CameraMode.Transition;

        while (Vector3.Distance(transform.position, ColonyCam.position) > .5f)
        {
            transform.position = Vector3.Lerp(transform.position, ColonyCam.position, TransitionSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, ColonyCam.rotation, TransitionSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        transform.position = ColonyCam.position;
        transform.rotation = ColonyCam.rotation;
        Zoom = 0;
        mode = CameraMode.Colony;
    }

    /// <summary>
    /// Controls camera for player attack
    /// </summary>
    /// <param name="enemy">the enemy to focus on</param>
    public void EnemyAttacking(GameObject enemy ) {
        mode = CameraMode.PlayerAttacking;
        
    }

    public void PlayerAttacking(GameObject player)
    {
        
        
    }
    IEnumerator ToPlayer()
    {
        mode = CameraMode.Transition;
        Zoom = 0;

        while (Vector3.Distance(transform.position, Playercam.position) > .5f)
        {

            transform.position = Vector3.Lerp(transform.position, Playercam.position, TransitionSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, Playercam.rotation, TransitionSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        transform.position = Playercam.position;
        transform.rotation = Playercam.rotation;

        mode = CameraMode.Player;
        Player.SetControlled(true);
    }

}

enum CameraMode
{
    Colony, Player, Transition, PlayerAttacking, EnemyAttacking
}
