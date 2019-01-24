﻿using System.Collections;

using Assets.Meyer.TestScripts.Player;

using UnityEngine;
using UnityEngine.Assertions;

public class CameraController : MonoBehaviour {

    [ Header( "Transforms" ) , SerializeField ]
    private Transform playerTransform;

    [ SerializeField ] private Transform camRig;

    [ SerializeField ] public Transform playercam;

    [ SerializeField ] private Transform colonyCam;

    [ Header( "Basic Settings" ) , SerializeField ]
    private float camSpeed;

    [ SerializeField ] private float transitionSpeed;

    [ SerializeField ] private float mouseSensitivity;

    [ Header( "Zoom Settings" ) , SerializeField ]
    private float colonyWidth;

    [ SerializeField ] private float colonyHight;

    [ SerializeField ] private float maxZoom;

    [ SerializeField ] private float minZoom;

    private float zoom;

    private float timer;

    public static CameraController Controller;

    private bool isColony;

    private bool switched;

    // Use this for initialization
    private void Awake( ) {
        Controller = this;

        if ( Controller == null ){
            Controller = this;
        }
        else if ( Controller != this ){
            Destroy( gameObject );
        }

        camRig = transform.parent.parent.transform;

        playercam = transform.parent.transform;

        playerTransform = GameObject.FindGameObjectWithTag( "Player" ).transform;

        colonyCam = GameObject.Find( "ColonyCam" ).transform;
        Assert.IsNotNull( colonyCam , "ColonyCam cannot be found!" );
        SwitchMode( CameraMode.PLAYER );
    }

    // Update is called once per frame
    private void Update( ) {
        timer += Time.deltaTime;

        if ( Input.GetKeyDown( KeyCode.Tab ) ){
            isColony = !isColony;
            switched = true;
        }

        if ( isColony && switched ){
            switched = false;
            SwitchMode( CameraMode.COLONY );
        }

        if ( !isColony && switched ){
            switched = false;
            SwitchMode( CameraMode.PLAYER );
        }

        if ( Mode == CameraMode.COLONY ){
            if ( Input.GetKey( KeyCode.Mouse2 ) ){
                var l_x = -Input.GetAxis( "Mouse X" );
                l_x *= 100;

                var l_y = -Input.GetAxis( "Mouse Y" );
                l_y *= 100;

                transform.Translate( l_x * Time.deltaTime , l_y * Time.deltaTime , 0 );
            }
            else{
                var l_x = Input.GetAxis( "Horizontal" );
                l_x *= camSpeed;
                var l_y = Input.GetAxis( "Vertical" );
                l_y *= camSpeed;

                transform.Translate( l_x * Time.deltaTime , l_y * Time.deltaTime , 0 );
            }

            zoom += Input.GetAxis( "Mouse ScrollWheel" ) * Time.deltaTime * mouseSensitivity;
            zoom =  Mathf.Clamp( zoom , -maxZoom , minZoom );

            if ( transform.position.x > colonyWidth ){
                transform.position = new Vector3( colonyWidth , transform.position.y , transform.position.z );
            }
            else if ( transform.position.x < -colonyWidth ){
                transform.position = new Vector3( -colonyWidth , transform.position.y , transform.position.z );
            }

            if ( transform.position.z > colonyHight ){
                transform.position = new Vector3( transform.position.x , transform.position.y , colonyHight );
            }
            else if ( transform.position.z < -colonyHight ){
                transform.position = new Vector3( transform.position.x , transform.position.y , -colonyHight );
            }

            transform.position = new Vector3( transform.position.x , colonyCam.position.y + zoom , transform.position.z );
        }

        if ( Mode == CameraMode.PLAYER && !StaticManager.UiInventory.ItemsInstance.windowIsOpen ){
            camRig.position = playerTransform.position;

            var   l_cameraray   = Camera.main.ScreenPointToRay( Input.mousePosition );
            var   l_groundplane = new Plane( Vector3.up , Vector3.zero );
            float l_raylength;

            var plane = new Plane( Vector3.up , playerTransform.position );
            var ray   = Camera.main.ScreenPointToRay( Input.mousePosition ); //sends a line according to the z and x axis

            

            float l_rotation = 0;

            if ( Input.GetKey( KeyCode.Mouse2 ) ){
                l_rotation =  Input.GetAxis( "Mouse X" );
                l_rotation *= mouseSensitivity;
            }
            else if ( Input.GetKey( KeyCode.A ) ){
                l_rotation = -mouseSensitivity;
            }
            else if ( Input.GetKey( KeyCode.D ) ){
                l_rotation = mouseSensitivity;
            }

            l_rotation *= Time.deltaTime;

            camRig.Rotate( 0 , l_rotation , 0 );

            if ( Input.GetKey( KeyCode.W ) ){
                zoom += Time.timeScale * mouseSensitivity;
            }
            else if ( Input.GetKeyDown( KeyCode.S ) ){
                zoom -= Time.timeScale * mouseSensitivity;
            }

            zoom = Mathf.Clamp( zoom , -minZoom , maxZoom );

            transform.localPosition = new Vector3( 0 , 0 , zoom );
        }
    }

    public void SwitchMode( CameraMode _mode , Companion _companion = null ) {
        if ( _mode == CameraMode.PLAYER ){
            StartCoroutine( ToPlayer( ) );
        }
        else if ( _mode == CameraMode.COLONY ){
            StartCoroutine( ToColony( ) );
        }
    }

    private IEnumerator ToColony( ) {
        Mode = CameraMode.TRANSITION;

        var l_camPos = new Vector3( Character.Player.transform.position.x , colonyCam.position.y , Character.Player.transform.position.z );

        while ( Vector3.Distance( transform.position , colonyCam.position ) > .5f ){
            transform.position = Vector3.Lerp( transform.position , l_camPos , transitionSpeed               * Time.deltaTime );
            transform.rotation = Quaternion.Slerp( transform.rotation , colonyCam.rotation , transitionSpeed * Time.deltaTime );

            yield return new WaitForEndOfFrame( );
        }

        transform.position = colonyCam.position;
        transform.rotation = colonyCam.rotation;
        zoom               = 0;
        Mode               = CameraMode.COLONY;
    }

    private IEnumerator ToPlayer( ) {
        Mode = CameraMode.TRANSITION;
        zoom = 0;
        var l_pos = playercam.position;
        var l_rot = playercam.rotation;

        while ( Vector3.Distance( transform.position , playercam.position ) > .5f ){
            transform.position = Vector3.Lerp( transform.position , l_pos , transitionSpeed     * Time.deltaTime );
            transform.rotation = Quaternion.Slerp( transform.rotation , l_rot , transitionSpeed * Time.deltaTime );

            yield return new WaitForEndOfFrame( );
        }

        transform.position = playercam.position;
        transform.rotation = playercam.rotation;

        Mode = CameraMode.PLAYER;
    }

    public CameraMode Mode { get; set; }

}

public enum CameraMode {

    COLONY ,

    PLAYER ,

    TRANSITION

}