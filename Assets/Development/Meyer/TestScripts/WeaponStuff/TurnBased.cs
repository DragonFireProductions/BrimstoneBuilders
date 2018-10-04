using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

using Assets.Meyer.TestScripts;
using Assets.Meyer.TestScripts.Player;

using Kristal;

using TMPro;

using UnityEditor.VersionControl;

using UnityEngine;
using UnityEngine.AI;

using Random = UnityEngine.Random;

public class TurnBased : MonoBehaviour {

	private GameObject player;

	private bool isPlayerTurn = true;

	private bool isEnemyTurn = false;

	private CameraController cameraController;

	public static TurnBased Instance;

	public List < GameObject > CompanionList;

	public List < GameObject > EnemyList;

	private int i = 0;

	private GameObject selectedCompanion;

	 private bool attackMode = false;

	private Vector3 PlayerStartPos;

	private Quaternion PlayerStartRotation;

    private Vector3 EnemyStartPos;

    private Quaternion EnemyStartRotation;

	[ SerializeField ] private float playerAttackTime;

	[ SerializeField ] private float enemyAttackTime;

	[ SerializeField ] private float stoppingDistance;

	private bool complete = false;

	private bool needstoSwitch = false;

    void Awake( ) {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
            Destroy(gameObject);

		 DontDestroyOnLoad(gameObject);
    }
	// Use this for initialization
	void Start () {

		Instance = this;

		player = Character.player.gameObject;

		cameraController = GameObject.Find( "Main Camera" ).GetComponent < CameraController >( );

		UIInventory.instance.CompanionStatShowWindow(false);

		CompanionList = Character.player.GetComponent < CompanionGroup >( ).Group;
	}

	public bool IsPlayerTurn {
		get { return isPlayerTurn;}
		set { isPlayerTurn = value; }
	}
	// Update is called once per frame
	void Update () {
		SelectCompanion( );
		SelectEnemy();
		ViewEnemyStats();
		Check( );
	}
    public void Switch( ) {
		isPlayerTurn = !isPlayerTurn;
		isEnemyTurn = !isEnemyTurn;

	}

	public void Check( ) {
       
	}

	public void SelectEnemy() {
        if (Input.GetMouseButtonDown(0) && TurnBased.Instance.AttackMode && TurnBased.Instance.IsPlayerTurn)
        {
             Debug.Log("Mouse is down");
			
             RaycastHit l_hitInfo = new RaycastHit();
             bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out l_hitInfo);
             if (hit)
             {
                 Debug.Log("Hit " + l_hitInfo.transform.gameObject.name);
                 if (l_hitInfo.transform.gameObject.tag == "Enemy" && selectedCompanion != null)
                 {
					 Switch();
                     PlayerStartPos = selectedCompanion.transform.position;
                     PlayerStartRotation = selectedCompanion.transform.localRotation;
					 
					 CharacterUtility.instance.EnableObstacle(SelectedCompaion, true);
                    selectedCompanion.GetComponent<NavMeshAgent>().stoppingDistance = stoppingDistance;

					 CharacterUtility.instance.EnableObstacle(l_hitInfo.transform.gameObject, true);

                    selectedCompanion.GetComponent<NavMeshAgent>().SetDestination(l_hitInfo.transform.gameObject.transform.position + (l_hitInfo.transform.gameObject.transform.forward * 2) );

					 Task t = new Task(PlayerTurnAttack(l_hitInfo.transform.gameObject) );
		
                }
                else
                 {
                     Debug.Log("Enemy not selected");
                 }
             }
             else
             {
                 Debug.Log("No hit");
             }
             Debug.Log("Mouse is down");
        }

		
    }
    public IEnumerator rotateTowards(GameObject game_object, Quaternion rotation, string CoroutineName)
    {

        while (game_object.transform.localRotation != rotation)
        {
	        game_object.transform.rotation = Quaternion.RotateTowards(game_object.transform.rotation, rotation, 90 * Time.deltaTime);
			
            yield return new WaitForEndOfFrame();
        }
        CharacterUtility.instance.EnableObstacle(game_object, false);

	    if ( CoroutineName != null ){
	    StartCoroutine( CoroutineName );
        }

    }
    IEnumerator PlayerTurnAttack(GameObject selectedEnemy) {
	    float dis = selectedCompanion.GetComponent < NavMeshAgent >( ).remainingDistance - selectedCompanion.GetComponent < NavMeshAgent >( ).stoppingDistance;
	    var st = selectedCompanion.GetComponent < NavMeshAgent >( ).pathStatus;
	    while ( !CharacterUtility.instance.NavDistanceCheck( selectedCompanion ) ) 
        {
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(playerAttackTime);

	    CharacterUtility.instance.EnableObstacle(selectedEnemy.transform.gameObject, false);

        CharacterUtility.instance.EnableObstacle(selectedCompanion, true);

        selectedCompanion.GetComponent<NavMeshAgent>().stoppingDistance = 0;

	    float health = selectedEnemy.GetComponent < Stat >( ).Health;

        selectedEnemy.GetComponent<Stat>().Health -= DamageCalc.Instance.CalcAttack(selectedEnemy, selectedCompanion);
		

        UIInventory.instance.ShowNotification("You have selected to attack " + selectedEnemy.transform.gameObject.name + "!\n" + selectedEnemy.name + " health was: " + health + "\n Health is now " + selectedEnemy.transform.gameObject.GetComponent<Stat>().Health, 15);

        selectedCompanion.GetComponent<NavMeshAgent>().SetDestination(PlayerStartPos);


	    while ( !CharacterUtility.instance.NavDistanceCheck(selectedCompanion)){
		    yield return new WaitForEndOfFrame( );
	    }

        if (selectedEnemy.GetComponent<Stat>().Health >= 0)
        {
			StartCoroutine(rotateTowards(selectedCompanion, PlayerStartRotation, "SelectPlayer"));

        }
        else if (selectedEnemy.GetComponent<Stat>().Health < 0 && selectedEnemy != selectedEnemy.GetComponent<Enemy>().Leader){
	        GameObject leader = selectedEnemy.GetComponent < Enemy >( ).Leader;

            selectedEnemy.GetComponent<Enemy>().Leader.GetComponent<EnemyGroup>().Remove(selectedEnemy);
            if (EnemyList.Count == 0){

	            Destroy( leader );
	            Character.player.GetComponent < PlayerController >( ).enabled = true;
                CameraController.controller.SwitchMode(CameraMode.Player);

                attackMode = false;
	            UIInventory.instance.CompanionStatShowWindow( false );

				GameObject[] obj = GameObject.FindGameObjectsWithTag( "Guard" );
                for (int i = 0; i < obj.Length; i++)
                {
                    obj[i].GetComponent<Guard>().enabled = true;
                    if (Vector3.Distance(obj[i].transform.position, Character.player.transform.position + (Character.player.transform.position + (Vector3.forward * 15))) < 0.4)
                    {
                        obj[i].GetComponent<NavMeshAgent>().destination = obj[i].transform.forward + (Vector3.forward * 30);
                    }
                }
                this.enabled = false;
            }
        }
    }

    public void SelectPlayer()
    {
        int ran = Random.Range(0, EnemyList.Count - 1);
        GameObject l_selectedEnemy = EnemyList[ran];

        int l_ran = Random.Range(0, TurnBased.Instance.CompanionList.Count);

		CharacterUtility.instance.EnableObstacle( l_selectedEnemy, true );

		l_selectedEnemy.GetComponent<NavMeshAgent>().stoppingDistance = stoppingDistance;
        EnemyStartRotation = l_selectedEnemy.transform.localRotation;
        EnemyStartPos = l_selectedEnemy.transform.position;

        l_selectedEnemy.GetComponent<EnemyNav>().SetDestination(TurnBased.Instance.CompanionList[l_ran].transform.position + (TurnBased.Instance.CompanionList[l_ran].transform.forward * 2)  );

	    StartCoroutine(EnemyTurnAttack(l_selectedEnemy, TurnBased.Instance.CompanionList[l_ran]));

    }

    IEnumerator EnemyTurnAttack(GameObject _selected_enemy, GameObject _selectedCompanion)
    {
	    while (!CharacterUtility.instance.NavDistanceCheck(_selected_enemy)  ){
		    yield return new WaitForEndOfFrame( );
	    }
        yield return new WaitForSeconds(enemyAttackTime);

	    float health = _selectedCompanion.GetComponent < Stat >( ).Health;

        _selectedCompanion.GetComponent<Stat>().Health -= DamageCalc.Instance.CalcAttack( _selected_enemy , _selectedCompanion );

        

	    UIInventory.instance.ShowNotification(_selected_enemy.name + " has selected to attack " + _selectedCompanion.name + "\n Health was: " + health + "\n" + _selected_enemy.name+ "health is now " + _selectedCompanion.transform.gameObject.GetComponent<Stat>().Health, 15);

        CharacterUtility.instance.EnableObstacle(_selectedCompanion, true);

	    CharacterUtility.instance.EnableObstacle(_selected_enemy, true);
	    _selected_enemy.GetComponent < NavMeshAgent >( ).stoppingDistance = 0;
        _selected_enemy.GetComponent<EnemyNav>().SetDestination(EnemyStartPos);

        while (!CharacterUtility.instance.NavDistanceCheck(_selected_enemy))
        {
            yield return new WaitForEndOfFrame();
        }

	    StartCoroutine( CharacterUtility.instance.rotateTowards( _selected_enemy , EnemyStartRotation ) );
		CharacterUtility.instance.EnableObstacle(_selected_enemy, false);

        if (_selectedCompanion.GetComponent<Stat>().Health < 0)
        {
            if (_selectedCompanion == selectedCompanion){
	            needstoSwitch = true;
            }
            if (_selectedCompanion == Character.player)
            {
                //GameOver
            }
            else
            {
                Character.player.GetComponent<CompanionGroup>().Remove(_selectedCompanion);
            }
        }
        Switch();

    }
	

    public void SelectCompanion( ) {
		if ( (Input.GetKeyDown(KeyCode.P) && isPlayerTurn && attackMode) || needstoSwitch){
			needstoSwitch = false;
			UIInventory.instance.CompanionStatShowWindow(true);
			SelectedCompaion = CompanionList[ i ];
			SelectedCompaion.GetComponent<Renderer>().material.color = Color.red;
			UIInventory.instance.UpdateCompanionStats(selectedCompanion.gameObject.GetComponent<Stat>());
			if ( i != 0 ){
                CompanionList[i - 1].GetComponent<Renderer>().material.color = Color.blue;
				CharacterUtility.instance.EnableObstacle(CompanionList[i - 1], false);
            }
            else
            {
                CompanionList[CompanionList.Count - 1].GetComponent<Renderer>().material.color = Color.blue;
				CharacterUtility.instance.EnableObstacle(CompanionList[CompanionList.Count - 1], false);
            }

            Debug.Log(CompanionList[i].name);
			i++;

			if ( i >= CompanionList.Count ){
				i = 0;
			}

			if ( selectedCompanion != Character.player ){
				CameraController.controller.SwitchMode(CameraMode.ToOtherPlayer, SelectedCompaion.GetComponent<Companion>());

            }
			else{
				CameraController.controller.SwitchMode(CameraMode.ToPlayerBattle);
			}
        }
	}

	public void ViewEnemyStats( ) {
        if (Input.GetMouseButton(1) )
        {
            Debug.Log("Mouse is down");

            RaycastHit l_hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out l_hitInfo);
            if (hit)
            {
                Debug.Log("Hit " + l_hitInfo.transform.gameObject.name);
                if (l_hitInfo.transform.gameObject.tag == "Enemy")
                {
                  UIInventory.instance.UpdateStats(l_hitInfo.transform.gameObject.GetComponent<Stat>());
                }
                else if (l_hitInfo.transform.gameObject.tag == "Player" || l_hitInfo.transform.gameObject.tag == "Companion")
                {
	                UIInventory.instance.UpdateStats(l_hitInfo.transform.gameObject.GetComponent<Stat>());
	                
                }
				else
	            {
					Debug.Log("Nothing is selected");
	            }
            }
            else
            {
                Debug.Log("No hit");
            }
            Debug.Log("Mouse is down");

        }
        else
        {
			UIInventory.instance.StatWindowShow(false);
        }
    }

	void ViewSelectedCompanionStats() {

	}

	public void lineUp( GameObject leader, List <GameObject> groups) {
		
		int right = 2;
		int left = 2;
		
		for ( int k = 0 ; k < groups.Count; k += 2 ){

			if ( groups[ k ] != leader ){


				var move = leader.transform.position + ( leader.transform.right * right );

				groups[ k ].GetComponent < NavMeshAgent >( ).stoppingDistance = 0.0f;
				groups[ k ].GetComponent < NavMeshAgent >( ).SetDestination( move );
				right += 2;
			}
		}

		for ( int k = 1 ; k < groups.Count ; k += 2 ){
			if ( groups[ k ] != leader ){
				var moveleft = leader.transform.position + ( -leader.transform.right * left );
				groups[ k ].GetComponent < NavMeshAgent >( ).stoppingDistance = 0;
				groups[ k ].GetComponent < NavMeshAgent >( ).SetDestination( moveleft );

				left += 2;
			}
		}

		for ( int k = 0 ; k < groups.Count; k++ ){
			if (groups[ k ] != leader ){
				StartCoroutine( turn( groups[ k ] , leader , groups ) );
			}
		}
		
    }

	private IEnumerator turn(GameObject obj, GameObject leader, List <GameObject> groups) {
		while ( !CharacterUtility.instance.NavDistanceCheck(obj) ){
			yield return new WaitForEndOfFrame();
		}

		if ( obj != Character.player ){
			obj.GetComponent < Collider >( ).isTrigger = !obj.GetComponent < Collider >( ).isTrigger;
        }

		while ( obj.transform.forward != leader.transform.forward ){

            obj.transform.rotation = Quaternion.Lerp(obj.transform.rotation, leader.transform.rotation, 5 * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        if (obj != Character.player)
        {
	        obj.GetComponent<Collider>().isTrigger = !obj.GetComponent<Collider>().isTrigger;
        }

		if ( obj == groups[groups.Count - 1] ){
			complete = true;
			StartCoroutine( checkTurn( groups , leader ) );
		}


    }

	private IEnumerator checkTurn( List < GameObject > group, GameObject leader) {
		complete = false;
		for ( int j = 0 ; j < group.Count ; j++ ){
			while ( group[ i ].transform.rotation != leader.transform.rotation ){
				var to = Quaternion.Slerp( group[ i ].transform.rotation , leader.transform.rotation , 5 * Time.deltaTime );
                group[ i ].transform.rotation = Quaternion.Slerp(group[ i ].transform.rotation, leader.transform.rotation, 5 * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
		}
	}

    public IEnumerator lineUpLeader( GameObject leader, List <GameObject> group) {
		CharacterUtility.instance.EnableObstacle( leader, true );
        leader.GetComponent<NavMeshAgent>().stoppingDistance = 0;
        leader.GetComponent < NavMeshAgent >( ).SetDestination( Character.player.transform.position + ( Character.player.transform.forward * 9.0f ) );

		while ( !leader.GetComponent<NavMeshAgent>().hasPath) {
			yield return new WaitForEndOfFrame();
		}
        while (leader.GetComponent<NavMeshAgent>().remainingDistance > 0.0f) { 

            yield return new WaitForEndOfFrame();
        }
		leader.transform.LookAt(Character.player.transform);
		lineUp(leader , group);
    }

    public GameObject SelectedCompaion {
		get { return selectedCompanion; }
		set { selectedCompanion = value; }
	}
    public bool AttackMode
    {
        get { return attackMode; }
        set { attackMode = value; }
    }
	public bool TurnComplete {
		get { return complete; }
		set { complete = value; }
	}
	
}
