using System.Collections;
using System.Collections.Generic;
using System.Threading;

using Assets.Meyer.TestScripts.Player;

using Kristal;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class LeaderNav : CompanionNav {

    private Collider[] colliders;

    private float count;

    private float displaytimer;

    public Enemy enemy;

    public ParticleSystem enemySystem;

	private RaycastHit hit;

    private Ray l_ray;

    private LayerMask mask;

    [SerializeField] private TextMeshProUGUI message;

    private ParticleSystem rain;

    private Vector3 rainPosition;

    //[SerializeField] private TextMeshProUGUI head;
    //[SerializeField] private TextMeshProUGUI left_arm;
    //[SerializeField] private TextMeshProUGUI right_arm;
    //[SerializeField] private TextMeshProUGUI body;
    //[SerializeField] private TextMeshProUGUI legs;
    //[SerializeField] private TextMeshProUGUI feet;

    //[SerializeField] private RawImage image;
    //[SerializeField] private RawImage a_head;
    //[SerializeField] private RawImage a_left_arm;
    //[SerializeField] private RawImage a_right_arm;
    //[SerializeField] private RawImage a_body;
    //[SerializeField] private RawImage a_legs;
    //[SerializeField] private RawImage a_feet;
    //private bool showArmor = false;

	private ParticleSystem selected;

    private bool timerEnabled = false;

    public List < Companion > highlightedCompanions;

    private void Start( ) {
		base.Start();
        highlightedCompanions = new List < Companion >();
		hit = new RaycastHit();
		character = GetComponent < Character >( );
		message = GameObject.Find( "GoForward" ).GetComponent < TextMeshProUGUI >( );
		mask = LayerMask.GetMask("Enemy");
        character.projector.gameObject.SetActive(true);

	}

	// Update is called once per frame
	protected override void Update () {

        if ( Input.GetMouseButton( 0 ) && !StaticManager.UiInventory.ItemsInstance.windowIsOpen ){
            l_ray = Camera.main.ScreenPointToRay( Input.mousePosition );

	        if ( Physics.Raycast(l_ray, out hit) ){
		        if ( hit.collider.name == "Terrain" ){
					SetState = state.MOVE;

                    if ( enemy ){
                    enemy.projector.gameObject.SetActive(false);

                    }
		            StaticManager.particleManager.Play(ParticleManager.states.Click, hit.point);

                    if ( enemySystem ){
                        Destroy( enemySystem.gameObject );
		        }
                }
            }
        }

		if ( Input.GetMouseButtonDown( 0 ) ){
			if ( Physics.Raycast( l_ray , out hit ) ){
				if (hit.collider.tag == "ShopKeeper" && !StaticManager.UiInventory.ItemsInstance.windowIsOpen) //Left Click
				{
					StaticManager.UiInventory.ShowWindow( StaticManager.UiInventory.ItemsInstance.ShopUI.obj );
					StaticManager.Instance.Freeze( );
				}
			}
		}

		if ( Input.GetMouseButtonUp( 0 ) && !StaticManager.UiInventory.ItemsInstance.windowIsOpen && count < 0.4 ){
            if ( Physics.Raycast( l_ray , out hit ) ){
                if ( hit.collider.tag == "Enemy" ){
                    if ( enemy ){
                    enemy.projector.gameObject.SetActive(false);

                    }
                    Destroy( enemySystem );
			        enemy = hit.collider.GetComponent < Enemy >( );
			        SetState = state.ENEMY_CLICKED;
                    enemy.projector.gameObject.SetActive(true);
		        }
                else if (hit.collider.tag == "Post"){
			        message.text = hit.collider.name == "End" ? "The End is Neigh!" : "Go Forth!";

			        //Debug.Log("got the post");
			        StartCoroutine( show( ) );
		        }
                
                //
            }

        }


		if ( StaticManager.UiInventory.ItemsInstance.windowIsOpen == false ){
            if ( Input.GetMouseButtonDown( 1 ) ){
                var l_ray = Camera.main.ScreenPointToRay( Input.mousePosition );
                var hit1  = new RaycastHit( );

				if ( Physics.Raycast( l_ray , out hit1 ) ){
                    if ( hit1.collider.tag == "Companion" || hit1.collider.tag == "Player" ){
						StaticManager.UiInventory.ShowWindow( StaticManager.UiInventory.ItemsInstance.PlayerStats );
						StaticManager.UiInventory.UpdateStats( hit1.collider.GetComponent < BaseCharacter >( ).attachedWeapon.WeaponStats , StaticManager.UiInventory.ItemsInstance.AttachedWeapon );
						StaticManager.UiInventory.UpdateStats( hit1.collider.GetComponent < BaseCharacter >( ).stats ,                      StaticManager.UiInventory.ItemsInstance.CharacterStats, false );
						StaticManager.UiInventory.ItemsInstance.PlayerStats.transform.Find( "WeaponImage" ).GetComponent < RawImage >( ).texture = hit1.collider.GetComponent < BaseCharacter >( ).attachedWeapon.WeaponStats.icon;
					}
				}
			}
		}

		if ( Input.GetMouseButtonUp( 1 ) ){
				StaticManager.UiInventory.CloseWindow(StaticManager.UiInventory.ItemsInstance.PlayerStats );
			}
		
		if ( !StaticManager.RealTime.Attacking ){
			 colliders = Physics.OverlapSphere(transform.position, 10, mask);

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

				if ( enemy == null ){
					SetState = state.FREEZE;

					return;
				}

                if ( Vector3.Distance( enemy.transform.position , gameObject.transform.position ) > 3 ){
                    SetState = state.ENEMY_CLICKED;
                }

                character.transform.LookAt(enemy.transform.position);
                    character.AnimationClass.Play(AnimationClass.states.AttackTrigger);
                    character.attachedWeapon.AnimationClass.Play(AnimationClass.weaponstates.EnabledTrigger);

                break;
			case state.MOVE:
				Agent.SetDestination( hit.point );

				break;
			case state.ENEMY_CLICKED:

				if (Vector3.Distance(enemy.transform.position, gameObject.transform.position) < 3  ){
					SetState = state.ATTACKING;
				}
				else{
					Agent.SetDestination( enemy.transform.position );
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

    private IEnumerator show( ) {
		message.enabled = true;

		yield return  new WaitForSeconds(2);

		message.enabled = false;
	}

    private IEnumerator yield( ) {
		yield return new WaitForSeconds(0.5f);

		colliders = Physics.OverlapSphere(transform.position, 20, mask);

        foreach ( var l_collider in colliders ){
            StaticManager.RealTime.Attacking = true;
            StaticManager.RealTime.Enemies.Add(l_collider.gameObject.GetComponent<Enemy>());
	        StaticManager.RealTime.Attacking = true;
			StaticManager.RealTime.SetAttackEnemies();
            SetState = state.ATTACKING;
        }
    }

    //private IEnumerator CompanionSpawn()
    //{
    //    for (; i < friends.Length;)
    //    {
    //        var newFriend = Instantiate(friends[i].gameObject);
    //        Companion[] companion = newFriend.gameObject.GetComponentsInChildren<Companion>();

    //        foreach (var comp in companion)
    //        {
    //            Vector3 position = Random.insideUnitSphere * spawnRadius + this.gameObject.transform.position;
    //            //position.y = StaticManager.Character.gameObject.transform.position.y;
    //           // StaticManager.particleManager.Play()
    //            yield return new WaitForSeconds(1.0f);
    //            comp.Nav.Agent.Warp(position);
    //            comp.GetComponent<CompanionNav>().transform.position = this.gameObject.transform.position;
    //        }
    //    }
    //}

    //private void Despawn()
    //{
    //    for (; i < friends.Length;)
    //    {
    //        Destroy(friends[i].gameObject);
    //    }
    //}

}