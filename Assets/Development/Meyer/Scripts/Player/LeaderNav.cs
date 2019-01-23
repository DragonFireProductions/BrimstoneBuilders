﻿using System.Collections;

using Kristal;

using TMPro;

using UnityEngine;
using UnityEngine.EventSystems;

public class LeaderNav : CompanionNav {

    private Collider[] colliders;

    private float count;

    [ HideInInspector ] public Enemy enemy;

    [ HideInInspector ] public ParticleSystem enemySystem;

    private RaycastHit hit;

    private Ray l_ray;

    private LayerMask mask;

    [ SerializeField ] private TextMeshProUGUI message;

    private bool timerEnabled = false;

    private void Start( ) {
        hit            = new RaycastHit( );
        mask           = LayerMask.GetMask( "Enemy" );
        battleDistance = 4;
    }

    private void FixedUpdate( ) {
        speed        = Mathf.Lerp( speed , ( transform.position - lastPosition ).magnitude / Time.deltaTime , 0.75f );
        lastPosition = transform.position;
    }

    // Update is called once per frame
    protected override void Update( ) {
        character.AnimationClass.animation.SetFloat( "Walk" , Agent.velocity.magnitude / Agent.speed );

        if ( Input.GetMouseButton( 0 ) && !StaticManager.UiInventory.ItemsInstance.windowIsOpen && !EventSystem.current.IsPointerOverGameObject( ) ){
            l_ray = Camera.main.ScreenPointToRay( Input.mousePosition );

            if ( Physics.Raycast( l_ray , out hit ) ){
                if ( hit.collider.name == "Terrain" ){
                    SetState = state.MOVE;

                    if ( enemy ){
                        enemy.projector.gameObject.SetActive( false );
                    }

                    StaticManager.particleManager.Play( ParticleManager.states.Click , hit.point );

                    if ( enemySystem ){
                        Destroy( enemySystem.gameObject );
                    }
                }
            }
        }

        if ( Input.GetMouseButtonDown( 1 ) ){
            Agent.isStopped = true;
            SetState        = state.FREEZE;

            if ( character.attachedWeapon is GunType && Physics.Raycast( l_ray , out hit ) ){
                if ( hit.collider.tag != "Companion" && hit.collider.tag != "Player" ){
                    character.attachedWeapon.Use( );

                    if ( State != state.MOVE ){
                        StartCoroutine( rotate( ) );
                    }
                }
            }
        }

        if ( Input.GetMouseButtonDown( 0 ) ){
            if ( Physics.Raycast( l_ray , out hit ) ){
                if ( hit.collider.tag == "ShopKeeper" && !StaticManager.UiInventory.ItemsInstance.windowIsOpen ) //Left Click
                {
                    StaticManager.UiInventory.ShowWindow( StaticManager.UiInventory.ItemsInstance.ShopUI.obj );

                    foreach ( var l_currencyManagerShop in StaticManager.currencyManager.shops ){


                        if ( hit.collider.gameObject != l_currencyManagerShop ){
                            l_currencyManagerShop.GetComponent<Shop>().ShopContainer.SetActive(false);
                        }
                    }

                    hit.collider.GetComponent < Shop >( ).ShopContainer.SetActive( true );
                    StaticManager.currencyManager._shop = hit.collider.GetComponent < Shop >( );
                   
                    StaticManager.currencyManager.SwitchToBuy( );
                    StaticManager.Instance.Freeze( );
                }
            }
        }

        if ( Input.GetMouseButtonUp( 0 ) && !StaticManager.UiInventory.ItemsInstance.windowIsOpen && count < 0.4 ){
            if ( Physics.Raycast( l_ray , out hit ) ){
                if ( hit.collider.tag == "Enemy" ){
                    if ( enemy ){
                        enemy.projector.gameObject.SetActive( false );
                    }

                    Destroy( enemySystem );
                    enemy    = hit.collider.GetComponent < Enemy >( );
                    SetState = state.ENEMY_CLICKED;
                    enemy.projector.gameObject.SetActive( true );
                }
                else if ( hit.collider.tag == "Post" ){
                    message.text = hit.collider.name == "End" ? "The End is Neigh!" : "Go Forth!";
                    
                    StartCoroutine( show( ) );
                }
            }
        }

       

        if ( StaticManager.UiInventory.ItemsInstance.windowIsOpen == false ){
            if ( Input.GetMouseButtonDown( 1 ) ){
                var l_ray = Camera.main.ScreenPointToRay( Input.mousePosition );
                var hit1  = new RaycastHit( );

                if ( Physics.Raycast( l_ray , out hit1 ) ){
                    if ( hit1.collider.tag == "Companion" || hit1.collider.tag == "Player" ){
                        //	//		StaticManager.UiInventory.ShowWindow( StaticManager.UiInventory.ItemsInstance.PlayerStats );
                        //	StaticManager.UiInventory.UpdateStats( hit1.collider.GetComponent < BaseCharacter >( ).attachedWeapon.stats , StaticManager.UiInventory.ItemsInstance.AttachedWeapon );
                        //			StaticManager.UiInventory.UpdateStats( hit1.collider.GetComponent < BaseCharacter >( ).stats ,                      StaticManager.UiInventory.ItemsInstance.CharacterStats, false );
//						StaticManager.UiInventory.ItemsInstance.PlayerStats.transform.Find( "WeaponImage" ).GetComponent < RawImage >( ).texture = hit1.collider.GetComponent < BaseCharacter >( ).attachedWeapon.stats.icon;
                    }
                }

                character.attachedWeapon.Use( );
            }
        }
        
        if ( !StaticManager.RealTime.Attacking ){
            colliders = Physics.OverlapSphere( transform.position , 10 , mask );

            if ( colliders.Length > 0 ){
                StaticManager.RealTime.Attacking = true;
                StartCoroutine( yield( ) );
            }
        }

        if ( StaticManager.RealTime.Enemies.Count == 0 ){
            StaticManager.RealTime.Attacking = false;
        }

        switch ( State ){
            case state.ATTACKING:

                if ( character.attachedWeapon is SwordType ){ }

                if ( enemy == null ){
                    SetState = state.FREEZE;

                    return;
                }

                if ( Vector3.Distance( enemy.transform.position , gameObject.transform.position ) > 3 ){
                    SetState = state.ENEMY_CLICKED;
                }

                transform.LookAt( enemy.transform.position );
                character.attachedWeapon.Use( );

                break;

                break;
            case state.MOVE:
                Agent.SetDestination( hit.point );

                break;
            case state.ENEMY_CLICKED:

                if ( Vector3.Distance( enemy.transform.position , gameObject.transform.position ) < 3 ){
                    SetState = state.ATTACKING;
                }
                else{
                    Agent.SetDestination( enemy.transform.position );
                }

                if ( character.attachedWeapon is GunType ){
                    character.attachedWeapon.Use( );
                    SetState = state.IDLE;
                }

                break;
            case state.IDLE:

                break;
            case state.FOLLOW:

                break;
            case state.FREEZE:

                break;
            default:

                break;
        }
    }
    public IEnumerator rotate()
    {
        Plane playerPlane = new Plane(Vector3.up, transform.position);

        // Generate a ray from the cursor position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Determine the point where the cursor ray intersects the plane.
        // This will be the point that the object must look towards to be looking at the mouse.
        // Raycasting to a Plane object only gives us a distance, so we'll have to take the distance,
        //   then find the point along that ray that meets that distance.  This will be the point
        //   to look at.
        float hitdist = 0.0f;
        // If the ray is parallel to the plane, Raycast will return false.
        if (playerPlane.Raycast(ray, out hitdist))
        {

            // Get the point along the ray that hits the calculated distance.
            Vector3 targetPoint = ray.GetPoint(hitdist);

            // Determine the target rotation.  This is the rotation if the transform looks at the target point.
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);

            while (transform.rotation != targetRotation && State != state.MOVE)
            {
                // Smoothly rotate towards the target point.
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5 * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }



        }
    }
    private IEnumerator show( ) {
        message.enabled = true;

        yield return new WaitForSeconds( 2 );

        message.enabled = false;
    }

    private IEnumerator yield( ) {
        yield return new WaitForSeconds( 0.5f );

        colliders = Physics.OverlapSphere( transform.position , 20 , mask );

        foreach ( var l_collider in colliders ){
            StaticManager.RealTime.Attacking = true;
            StaticManager.RealTime.Enemies.Add( l_collider.gameObject.GetComponent < Enemy >( ) );
            StaticManager.RealTime.Attacking = true;
            StaticManager.RealTime.SetAttackEnemies( );
            SetState = state.ATTACKING;
        }
    }

}