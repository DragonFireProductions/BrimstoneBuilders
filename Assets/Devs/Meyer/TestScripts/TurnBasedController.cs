using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Timers;

using Assets.Meyer.TestScripts;
using Assets.Meyer.TestScripts.Player;

using Kristal;

using UnityEngine;
using UnityEngine.AI;

using Random = UnityEngine.Random;

public enum DistanceCheck {

    HasReachedDestination = 0,
    HasNotReachedDestination,
    NavMeshNotEnabled


}
public class TurnBasedController : MonoBehaviour {

    public List < Enemy > Enemies;

    public List < Companion > Companions;

    public Companion CompanionSelected;

    public Enemy EnemySelected;

    public CompanionLeader CompanionLeader;

    public Enemy EnemyLeader;

    public bool AttackMode;

    public static TurnBasedController instance;

    public int CompanionCount;

    //private float coroutineTimer;

    public void Start( ) {
        if ( instance == null ){
            instance = this;
        }
        else if ( instance != this ){
            Destroy( gameObject );
        }

        DontDestroyOnLoad( gameObject );

    }

    private bool initalized = false;
    private void Update( ) {
        UIInventory.instance.ViewEnemyStats();
        if ( AttackMode ){

            //If enemies & companions aren't lined up
            if ( !initalized ){
                Initalize();
                isPlayerTurn = true;
            }

            //if initalized & it's the players turn & either the player hasn't selected a companion OR they havent selected an enemy to attack
            if ( initalized && isPlayerTurn && (!hasSelectedCompanion || !isEnemySelected)){
                //then
                //Select companion
                SelectCompanion();
                //Select enemy
                SelectEnemy();
            }

            //if it's the players turn and they have selected an enemy & have selected a companion
            if ( isPlayerTurn && isEnemySelected) { 
                //Take players turn
                PlayersTurn( );
            }
            //if it's the enemys turn
            else if ( isEnemyTurn ){
               
                //take enemys turn
                EnemysTurn();
            }
        }
    }
    private bool setUpEnimies = false;
    private bool hasCompanionsLinedUp = false;

    void Initalize( ) {
        //if enemies are lined up
        if (setUpEnimies)
        {
            setUpEnimies = false;

            //line up players
            LineUpCompanions(Companions, CompanionLeader);
        }

        if (hasCompanionsLinedUp)
        {
            hasCompanionsLinedUp = false;
            Companions.Add(CompanionLeader);
            initalized = true;
        }
    }

    public void HasCollided( Enemy enemy ) {
        if ( !AttackMode ){
            Enemies     = enemy.Leader.EnemyGroup;
            EnemyLeader = enemy.Leader.Leader;
            Companions  = Character.instance.leader.CompanionGroup;
            CompanionLeader = Character.instance.gameObject.GetComponent < CompanionLeader >( );
            AttackMode  = true;
            Character.player.GetComponent < PlayerController >( ).enabled = false;
            CompanionCount = Companions.Count;
            Companions.Add(CompanionLeader);

            StartBattle( );
        }
    }

    public void StartBattle( ) {
        StartCoroutine( LineUpLeader( ) );
    }

    private bool hasLinedUp = false;

    private bool hasCompanions = false;

    private bool hasPlayers = false;

    private IEnumerator LineUpLeader( ) {
        CharacterUtility.instance.EnableObstacle( EnemyLeader.Nav.Agent , true );
        EnemyLeader.Nav.Agent.stoppingDistance = 0;
        EnemyLeader.Nav.SetDestination( Character.player.transform.position + Character.player.transform.forward * 15.0f );
        
        while ( CharacterUtility.instance.NavDistanceCheck( EnemyLeader.Nav.Agent ) == DistanceCheck.HasNotReachedDestination ){
            yield return new WaitForEndOfFrame( );
        }
        EnemyLeader.gameObject.transform.LookAt( Character.player.transform );
        LineUpCompanions( );
    }

    private void LineUpCompanions( ) {
        var right = 2;
        var left  = 2;

        var forward = -1;

        for ( var k = 0 ; k < Enemies.Count ; k += 2 ){
            Enemies[ k ].Nav.SetState = EnemyState.Battle;

            forward -= 1;
            var move = EnemyLeader.gameObject.transform.position + EnemyLeader.gameObject.transform.right * right + EnemyLeader.transform.forward * -forward ;

            Enemies[ k ].Nav.Agent.stoppingDistance = 0.0f;
            Enemies[ k ].Nav.Agent.SetDestination( move );
            right += 2;
        }

        forward = -1;
        for ( var k = 1 ; k < Enemies.Count ; k += 2 ){
            Enemies[k].Nav.SetState = EnemyState.Battle;
            forward -= 1;
            var moveleft = EnemyLeader.gameObject.transform.position + -EnemyLeader.gameObject.transform.right * left + EnemyLeader.transform.forward * -forward;
            Enemies[ k ].Nav.Agent.stoppingDistance = 0;
            Enemies[ k ].Nav.Agent.SetDestination( moveleft );

            left += 2;
        }

        for ( var i = 0 ; i < Enemies.Count ; i++ ){
            //CharacterUtility.instance.EnableObstacle( Enemies[ i ].Nav.Agent , true );
            StartCoroutine( Turn( Enemies[ i ], i ) );
        }
    }


    private bool addedLeader = false;

    private int p = 0;
    private void LineUpCompanions(List <Companion> companions, CompanionLeader companion_leader) {
        CameraController.controller.SwitchMode(CameraMode.Battle);
        var right = 2;
        var left = 2;
        var forward = -1;

        for (var k = 0; k < CompanionCount; k += 2){
            forward -= 1;
            companions[ k ].Nav.State = CompanionNav.CompanionState.Attacking;
            companions[ k ].Nav.State = CompanionNav.CompanionState.Attacking;
            var move = companion_leader.gameObject.transform.position + companion_leader.gameObject.transform.right * right + CompanionLeader.transform.forward * -forward;

            companions[k].Nav.Agent.stoppingDistance = 0.0f;
            companions[k].Nav.Agent.SetDestination(move);
            right += 2;
        }

        forward = -1;
        for (var k = 1; k < CompanionCount; k += 2){
            forward -= 1;
            companions[k].Nav.State = CompanionNav.CompanionState.Attacking;

            var moveleft = companion_leader.gameObject.transform.position + -companion_leader.gameObject.transform.right * left + CompanionLeader.transform.forward * -forward;
            companions[k].Nav.Agent.stoppingDistance = 0;
            companions[k].Nav.Agent.SetDestination(moveleft);

            left += 2;
        }

        for (var i = 0; i < CompanionCount; i++)
        {
            //CharacterUtility.instance.EnableObstacle( Enemies[ i ].Nav.Agent , true );
            StartCoroutine(Turn(companions[i], i));
        }
    }

    private IEnumerator Turn( Enemy enemy, int i ) {

        while ( CharacterUtility.instance.NavDistanceCheck( enemy.Nav.Agent ) == DistanceCheck.HasNotReachedDestination){
            yield return new WaitForEndOfFrame( );
        }
            CharacterUtility.instance.EnableObstacle( enemy.Nav.Agent );

            enemy.gameObject.GetComponent < Collider >( ).isTrigger =
                    !enemy.gameObject.GetComponent < Collider >( ).isTrigger;

        float timer = 0;

        var r = Quaternion.LookRotation(Character.player.transform.position - enemy.transform.position);
        while ( enemy.transform.rotation !=  r && timer < 3){
            enemy.transform.rotation = Quaternion.Slerp( enemy.transform.rotation , r , 5 * Time.deltaTime );
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        if ( timer > 3 ){
            enemy.transform.rotation = r;
        }

        timer = 0;
        enemy.gameObject.GetComponent < Collider >( ).isTrigger =
                    !enemy.gameObject.GetComponent < Collider >( ).isTrigger;

         p++;


        if ( p >= Enemies.Count){

               setUpEnimies = true;
        }


    }

    private IEnumerator Turn(Companion companion, int i) {
        
        while (CharacterUtility.instance.NavDistanceCheck(companion.Nav.Agent) == DistanceCheck.HasNotReachedDestination )
        {
            yield return new WaitForEndOfFrame();
        }
        
        CharacterUtility.instance.EnableObstacle(companion.Nav.Agent);

        companion.gameObject.GetComponent<Collider>().isTrigger =
                !companion.gameObject.GetComponent<Collider>().isTrigger;


        var r = Quaternion.LookRotation(EnemyLeader.transform.position - companion.transform.position);
        while (companion.transform.rotation != r)
        {
            companion.transform.rotation = Quaternion.Slerp(companion.transform.rotation, r, 5 * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        companion.gameObject.GetComponent<Collider>().isTrigger =
                !companion.gameObject.GetComponent<Collider>().isTrigger;

        p++;

        if (p >= Companions.Count - 1)
        {
                hasCompanionsLinedUp = true;
        }
    }

    private IEnumerator Turn( NavMeshAgent enemy , Vector3 toRotateTopos, Quaternion rotation) {
        hasRotated = false;

        if ( CharacterUtility.instance.NavDistanceCheck( enemy ) == DistanceCheck.HasNotReachedDestination){
            yield return new WaitForEndOfFrame( );
        }

        else{
            CharacterUtility.instance.EnableObstacle( enemy );

            enemy.gameObject.GetComponent < Collider >( ).isTrigger =
                    !enemy.gameObject.GetComponent < Collider >( ).isTrigger;

            float timer = 0;
            var   r     = Quaternion.LookRotation( toRotateTopos - enemy.transform.position );

            while ( enemy.transform.rotation != r && timer < 3 ){
                enemy.transform.rotation =  Quaternion.Slerp( enemy.transform.rotation , r , 5 * Time.deltaTime );
                timer                    += Time.deltaTime;

                yield return new WaitForEndOfFrame( );
            }

            if ( timer > 3 ){
                enemy.transform.rotation = r;
            }

            timer = 0;

            enemy.gameObject.GetComponent < Collider >( ).isTrigger =
                    !enemy.gameObject.GetComponent < Collider >( ).isTrigger;

            hasRotated = true;
        }
    }

    private int index;

    private bool hasSelectedCompanion = false;
    private void SelectCompanion( ) {
        UIInventory.instance.StatWindowShow(true);
        if ( Input.GetKeyDown( KeyCode.P ) && isPlayerTurn && AttackMode ){
            hasSelectedCompanion = true;
            if ( index >= Companions.Count - 1 || Companions[ index ].gameObject == null ){
                index = 0;
            }
                CompanionSelected                                                        = Companions[ index ];
                CompanionSelected.gameObject.GetComponent < Renderer >( ).material.color = Color.red;

            if ( index == 0 ){
                Companions[ Companions.Count - 1 ].gameObject.GetComponent < Renderer >( ).material.color = Color.blue;

            }
            else{
                Companions[ index- 1 ].gameObject.GetComponent < Renderer >( ).material.color = Color.blue;
            }
            UIInventory.instance.UpdateCompanionStats(CompanionSelected.gameObject.GetComponent<Stat>());

            CameraController.controller.SwitchMode( CameraMode.ToOtherPlayer , CompanionSelected );
            index += 1;
        }
        if ( CompanionSelected != null ){
            hasSelectedCompanion = true;

        }
    }

    private bool isPlayerTurn;

    private bool switchToPlayerTurn;

    private bool isEnemySelected;

    private bool isReached;

    private bool isReturned;

    private bool hasRotated;

    private void PlayersTurn( ) {

        // if player reached enemy
        if ( isReached){
            isReached = false;
            // return to main point
            StartCoroutine( returnToPoint( PlayerStartPos , CompanionSelected.agent ) );
        }
        // if player returned to point and hasn't turned yet
        if ( isReturned &&!hasRotated ){
            isReturned = false;
            //turn to original rotation
            StartCoroutine( Turn( CompanionSelected.agent ,EnemyLeader.transform.position, PlayerStartRotation ) );
        }
        //if it turned
        if ( hasRotated ){
            hasRotated          = false;
            isEnemyTurn         = true;
            isPlayerTurn        = false;
        }
    }

    private IEnumerator returnToPoint( Vector3 point , NavMeshAgent enemy ) {
        enemy.destination = point;

        while ( CharacterUtility.instance.NavDistanceCheck( enemy ) == DistanceCheck.HasNotReachedDestination ){
            yield return new WaitForEndOfFrame( );
        }

        isReturned = true;
    }

    private Vector3 PlayerStartPos;

    private Quaternion PlayerStartRotation;

    public static int stoppingDistance = 0;

    public IEnumerator NavDistanceCheck(NavMeshAgent agent)
    {
        while (CharacterUtility.instance.NavDistanceCheck(agent) == DistanceCheck.HasNotReachedDestination)
        {
            yield return new WaitForEndOfFrame();
        }

        isReached = true;
    }

    private void SelectEnemy( ) {
        if ( Input.GetMouseButtonDown( 0 ) && hasSelectedCompanion ){
            var l_hitInfo = new RaycastHit( );
            var hit       = Physics.Raycast( Camera.main.ScreenPointToRay( Input.mousePosition ) , out l_hitInfo );

            if ( hit ){
                Debug.Log( "Hit " + l_hitInfo.transform.gameObject.name );

                if ( l_hitInfo.transform.gameObject.tag == "Enemy" && CompanionSelected.gameObject != null ){
                    PlayerStartPos      = CompanionSelected.transform.position;
                    PlayerStartRotation = CompanionSelected.transform.localRotation;

                    CharacterUtility.instance.EnableObstacle( CompanionSelected.agent , true );
                    CompanionSelected.agent.stoppingDistance = stoppingDistance;

                    CompanionSelected.agent.SetDestination( l_hitInfo.transform.gameObject.transform.position + l_hitInfo.transform.gameObject.transform.forward * 2 );
                    EnemySelected = l_hitInfo.transform.gameObject.GetComponent < Enemy >( );
                    StartCoroutine( NavDistanceCheck( CompanionSelected.agent ) );

                    isEnemySelected = true;
                }
                else{
                    Debug.Log( "Enemy not selected" );
                }
            }
            else{
                Debug.Log( "No hit" );
            }

            Debug.Log( "Mouse is down" );
        }
    }

    private bool isEnemyTurn;

    private bool switchToEnemiesTurn;

    private bool isRandomPlayerSelected = false;

    private void EnemysTurn( ) {

        //if enemy hasn't selected a random player
        if (!isRandomPlayerSelected  ){
            isRandomPlayerSelected = true;

            //select random player
            SelectRandomPlayer();
        }

        //if enemy has reached random player
        if ( isReached ){
            isReached = false;

            // return to starting point
            StartCoroutine( returnToPoint( EnemyStartPos , SelectedEnemy.Nav.Agent ) );
        }

        //if enemy has returned to starting point
        if ( isReturned ){
            isReturned = false;

            // turn to face original direction 
            StartCoroutine( Turn( SelectedEnemy.Nav.Agent ,Character.player.transform.position, EnemyStartRotation ) );
        }
        // if enemy has turn to face original direction
        if ( hasRotated ){
            hasRotated   = false;
            //take players turn
            isPlayerTurn = true;
            isEnemyTurn  = false;
            isRandomPlayerSelected = false;
            hasSelectedCompanion = false;
            isEnemySelected = false;
        }
    }

    private Companion SelectedCompanion;

    private Enemy SelectedEnemy;

    private Vector3 EnemyStartPos;

    private Quaternion EnemyStartRotation;

    private void SelectRandomPlayer( ) {
        var randomEnemy = Random.Range( 0 , Enemies.Count - 1 );
        SelectedEnemy = Enemies[ randomEnemy ];

        var randomPlayer = Random.Range( 0 , Companions.Count - 1 );
        SelectedCompanion = Companions[ randomPlayer ];

        EnemyStartPos      = SelectedEnemy.transform.position;
        EnemyStartRotation = SelectedEnemy.transform.localRotation;

        CharacterUtility.instance.EnableObstacle( SelectedEnemy.Nav.Agent , true );
        SelectedEnemy.Nav.Agent.stoppingDistance = 3;

        SelectedEnemy.GetComponent < NavMeshAgent >( ).SetDestination( SelectedCompanion.gameObject.transform.position + SelectedCompanion.gameObject.transform.forward * 2 );

        StartCoroutine( NavDistanceCheck( SelectedEnemy.Nav.Agent) );
    }

}