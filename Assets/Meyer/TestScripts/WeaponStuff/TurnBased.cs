using System;
using System.Collections;
using System.Collections.Generic;

using Assets.Meyer.TestScripts.Player;

using Kristal;

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

	 private bool attackMode = true;

	private Vector3 PlayerStartPos;

	private Quaternion PlayerStartRotation;

    private Vector3 EnemyStartPos;

    private Quaternion EnemyStartRotation;

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

		CompanionList.Add(Character.player);

	}

	public bool IsPlayerTurn {
		get { return isPlayerTurn;}
		set { isPlayerTurn = value; }
	}
	// Update is called once per frame
	void Update () {
		SelectCompanion( );
		SelectEnemy();
	}
    
    public void AddCompanions( GameObject companion) {
		CompanionList.Add(companion);
	}
    public void AddEnemy(GameObject enemy)
    {
        EnemyList.Add(enemy);
    }
    public void Switch( ) {
		isPlayerTurn = !isPlayerTurn;
		isEnemyTurn = !isEnemyTurn;

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
                 if (l_hitInfo.transform.gameObject.tag == "Enemy")
                 {
                     PlayerStartPos = selectedCompanion.transform.position;
                     PlayerStartRotation = selectedCompanion.transform.rotation;

                    selectedCompanion.GetComponent<NavMeshAgent>().stoppingDistance = 3.0f;

                    selectedCompanion.GetComponent<NavMeshAgent>().SetDestination(l_hitInfo.transform.gameObject.transform.position);

                     StartCoroutine(PlayerTurnAttack());
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

    IEnumerator PlayerTurnAttack()
    {
        yield return new WaitForSeconds(5);

        selectedCompanion.GetComponent<NavMeshAgent>().stoppingDistance = 0;

        selectedCompanion.GetComponent<NavMeshAgent>().SetDestination(PlayerStartPos);

        selectedCompanion.transform.rotation = PlayerStartRotation;

        while (Vector3.Distance(selectedCompanion.transform.position, PlayerStartPos) > 0.5f)
        {
            yield return new WaitForEndOfFrame();
        }
        Switch();
        SelectPlayer();

    }

    public void SelectPlayer()
    {
        int ran = Random.Range(0, TurnBased.Instance.CompanionList.Count - 1);
        GameObject l_selectedEnemy = EnemyList[ran];

        int l_ran = Random.Range(0, TurnBased.Instance.CompanionList.Count);
		l_selectedEnemy.GetComponent<NavMeshAgent>().stoppingDistance = 3.0f;
        EnemyStartRotation = l_selectedEnemy.transform.rotation;
        EnemyStartPos = l_selectedEnemy.transform.position;
        l_selectedEnemy.GetComponent<EnemyNav>().SetDestination(TurnBased.Instance.CompanionList[l_ran].transform.position);
        StartCoroutine(EnemyTurnAttack(l_selectedEnemy));

    }

    IEnumerator EnemyTurnAttack(GameObject _selected_enemy)
    {
        yield return new WaitForSeconds(5);

	    _selected_enemy.GetComponent < NavMeshAgent >( ).stoppingDistance = 0;
        _selected_enemy.GetComponent<EnemyNav>().SetDestination(EnemyStartPos);
        _selected_enemy.transform.rotation = EnemyStartRotation;

        while (Vector3.Distance(_selected_enemy.transform.position, EnemyStartPos) > 3.0f)
        {
            yield return new WaitForEndOfFrame();
        }
        Switch();

    }
	

    public void SelectCompanion( ) {
		if ( Input.GetKeyDown(KeyCode.P) ){
			
			SelectedCompaion = CompanionList[ i ];
			SelectedCompaion.GetComponent<Renderer>().material.color = Color.red;

			if ( i != 0 ){
                CompanionList[i - 1].GetComponent<Renderer>().material.color = Color.blue;
            }
			else{
                CompanionList[CompanionList.Count - 1].GetComponent<Renderer>().material.color = Color.blue;

            }

            Debug.Log(CompanionList[i].name);
			i++;

			if ( i >= CompanionList.Count ){
				i = 0;
			}
		}
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

}
