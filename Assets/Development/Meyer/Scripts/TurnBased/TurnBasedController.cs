using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;

using Assets.Meyer.TestScripts;
using Assets.Meyer.TestScripts.Player;

using Kristal;

using TMPro;

using UnityEngine;
using UnityEngine.AI;

using Random = UnityEngine.Random;

namespace Assets.Meyer.TestScripts
{
    public class 
        TurnBasedController : MonoBehaviour {

        public bool AttackMode;

        public static TurnBasedController instance;

        [SerializeField] float turnTime = 1;

        [ SerializeField ] private float navTime = 3;

        [ SerializeField ] private int enemyPauseTime = 3;

        //private float coroutineTimer;

        public enum enumCheck
        {

            HasCompleted, Stuck, Running, Hit

        }

        public enum boolCheck {

            isTrue, isFalse

        }

        public struct blockingCheck {

            public bool isBlocking;
            
            public bool isNotBlocking;
          
            public bool block;

        }
       
        public struct checkStruct {

            public  blockingCheck blocking;
             
            public  bool isTurn;
          
            public  bool hasSelectedEnemy;
            
            public  bool hasSelectedCompanion;
         
            public  bool hasRotated;
           
            public  bool hasReached;
       
            public  bool hasReturned;
         
            public  bool hasDamaged;

            public bool hasLinedUp;

            public bool hasCompanionsLinedUp;

            public int count;
          
            public  BaseCharacter selectedAttacker;
          
            public  BaseCharacter selectedVictim;
         
            public  BaseCharacter leader;

            public List < BaseCharacter > characters;

            public bool AllAttackptsAre0;

        }

        public checkStruct _enemy;

        public checkStruct _player;


        public void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(this);
            }

            _player.leader = Character.player.GetComponent<CompanionLeader>();
        }

        private float timer = 0;

        private bool addedEnemyLeader;

        private void Update() {
            timer += Time.deltaTime;
           

            if (AttackMode)
            {
                //If enemies & companions aren't lined up
                if (!initalized)
                { 
                    Initalize();
                }

                if ( initalized && !addedEnemyLeader){
                    _player.isTurn = true;
                    addedEnemyLeader = true;
                    _enemy.characters.Add(_enemy.leader);
                }
                
                if ( addedEnemyLeader && _player.isTurn && !isPaused){
                    PlayersTurn();

                }
                
                //if it's the enemys turn
                if (_enemy.isTurn)
                {

                    //take enemys turn
                    EnemysTurn();
                }

                if ( _player.AllAttackptsAre0 && _enemy.AllAttackptsAre0 ){
                    StaticManager.uiInventory.ShowGameOver(true);
                    AttackMode = false;
                }
            }
        }
        private bool initalized;

        private void Initalize()
        {
            if (_player.hasCompanionsLinedUp)
            {
                foreach ( Companion VARIABLE in _player.characters ){
                    VARIABLE.material.color = VARIABLE.BaseColor;
                }

                foreach ( Enemy VARIABLE in _enemy.characters ){
                    VARIABLE.material.color = VARIABLE.BaseColor;
                }
                _player.hasCompanionsLinedUp = false;
                _player.characters.Insert(0, _player.leader);
                initalized = true;
            }
        }

        public void HasCollided(Enemy enemy)
        {
            if (!AttackMode){
                _player.leader = Character.player.GetComponent < CompanionLeader >( );
                this._enemy.characters = enemy.Leader.characters;
                this._enemy.leader =(Enemy)enemy.Leader.Leader;
                _player.characters = _player.leader.characters;
                AttackMode = true;

                if ( _player.leader.agent.enabled == true ){
                _player.leader.agent.isStopped = true;
                }

                foreach ( var VARIABLE in _player.characters ){
                    StaticManager.utility.EnableObstacle(VARIABLE.agent, true);
                }

                foreach ( Enemy VARIABLE in this._enemy.characters ){
                    VARIABLE.Nav.SetState = BaseNav.state.Battle;
                }

                _player.leader.agent.stoppingDistance = 0.1f;
                StaticManager.character.controller.SetControlled(false);
                this._enemy.leader.Nav.SetState = BaseNav.state.Battle;
                Debug.Log("Enemycount: " + this._enemy.characters.Count + "           HasCollided- line: 141");

                StartBattle();
            }
        }

        public void Block( ) {
            
            if ( (addedEnemyLeader && _player.isTurn && !_player.hasSelectedEnemy && _player.selectedAttacker.stats.AttackPoints > 3) ){
                _player.selectedAttacker.AnimationClass.Stop(AnimationClass.states.Selected);
                _player.blocking.block = true;
                _player.selectedAttacker.isBlocking = true;
                _player.selectedAttacker.stats.AttackPoints = 0;
                _player.hasRotated = false;
                _enemy.isTurn = true;
                _player.isTurn = false;
                _player.hasSelectedEnemy = false;
                
                StaticManager.uiInventory.UpdateCompanionStats(_player.selectedAttacker.stats);

                StaticManager.uiInventory.ShowNotification("You have chosen to block", 5);
            }
            else if (_player.selectedAttacker.stats.AttackPoints < 4){
                StaticManager.uiInventory.ShowNotification("Uh-Oh, Not enough attack points!", 3);
                StaticManager.uiInventory.AppendNotification("It costs 4 attack points to block!");
            }
        }

        public void StartBattle()
        {
            CameraController.controller.SwitchMode(CameraMode.Battle, (Companion)_player.leader);

            StartCoroutine(LineUpLeader());
        }

        private GameObject obj;
        private IEnumerator LineUpLeader() {
            obj = new GameObject("basePos");
            obj.transform.position = Character.player.transform.position + Character.player.transform.forward * 15.0f;
            obj.transform.LookAt(Character.player.transform);
            StaticManager.utility.EnableObstacle(_enemy.leader.Nav.Agent, true);
            _enemy.leader.Nav.Agent.stoppingDistance = 0;
            _enemy.leader.Nav.SetDestination(Character.player.transform.position + Character.player.transform.forward * 15.0f);
            Debug.Log("Enemycount: " + _enemy.characters.Count + "           LineUpLeader- line: 162");

            LineUpCompanions();
            LineUpEnemies();

            while (StaticManager.utility.NavDistanceCheck(_enemy.leader.Nav.Agent) == DistanceCheck.HasNotReachedDestination)
            {
                yield return new WaitForEndOfFrame();
            }
            _enemy.leader.gameObject.transform.LookAt(Character.player.transform);
        }


        [SerializeField] private int index = 0;
        

        private void SelectCompanion() {
            if ( Input.GetKeyDown(KeyCode.RightBracket) ){
                index += 1;
  
            }

            if ( Input.GetKeyDown(KeyCode.LeftBracket) ){
                index -= 1;
            }
               StaticManager.uiInventory.CompanionStatShowWindow(true);
            if (index < 0)
            {
                index = _player.characters.Count - 1;
            }
            if (index >= _player.characters.Count || _player.characters[index].gameObject == null)
                {
                    index = 0;
                }

            if (_player.selectedAttacker != null)
            {
                _player.selectedAttacker.AnimationClass.Stop(AnimationClass.states.Selected);
                _player.selectedAttacker.material.color = _player.selectedAttacker.BaseColor;
            }

                _player.selectedAttacker = _player.characters[index];
               

                StaticManager.uiInventory.UpdateCompanionStats(_player.selectedAttacker.gameObject.GetComponent<Stat>());

                CameraController.controller.SwitchMode(CameraMode.ToOtherPlayer,(Companion) _player.selectedAttacker);
                

                _player.selectedAttacker.AnimationClass.Play(AnimationClass.states.Selected);

           
                _player.AllAttackptsAre0 = checkAttackpts( _player.characters );
        
        }

       
        private void PlayersTurn() {
            
            if (!_player.hasSelectedCompanion){
                
                if ( _enemy.selectedAttacker ){
                    _enemy.selectedAttacker.AnimationClass.Stop(AnimationClass.states.Selected);
                }
                SelectCompanion();
            }
            if ( !_player.hasSelectedEnemy && !_player.selectedAttacker.isBlocking && _player.selectedAttacker.stats.AttackPoints > 0)
            {
                SelectEnemy();
            }
            else if (!_player.hasSelectedEnemy && _player.hasSelectedCompanion && (_player.selectedAttacker.isBlocking || _player.selectedAttacker.stats.AttackPoints <= 0)){
                
                _player.hasSelectedCompanion = false;
                _player.hasRotated = false;
                _enemy.isTurn = true;
                _player.isTurn = false;
                _player.selectedAttacker.AnimationClass.Stop(AnimationClass.states.Selected);
            }
            // if player reached _enemy
            if (_player.hasReached)
            {
                _player.hasReached = false;

                StartCoroutine(damage(_player.selectedVictim, _player, _bool => _player.hasDamaged = _bool));

            }

            if (_player.hasDamaged)
            {
                _player.hasDamaged = false;
                _player.selectedVictim.material.color = _player.selectedVictim.BaseColor;
                StartCoroutine(returnToPoint(PlayerStartPos, _player.selectedAttacker.agent, _result => _player.hasReturned = _result));
                _player.selectedAttacker.AnimationClass.Play(AnimationClass.states.Selected);

            }
            // if player returned to point and hasn't turned yet
            if (_player.hasReturned && !_player.hasRotated){
                _player.hasReturned = false;
                _player.selectedAttacker.AnimationClass.Stop(AnimationClass.states.Selected);

                //turn to original rotation
                StartCoroutine(Turn(_player.selectedAttacker.agent, _enemy.leader.transform.position, PlayerStartRotation,_bool=> _player.hasRotated = _bool));
            }

            //if it turned
            if (_player.hasRotated && _player.selectedAttacker.stats.AttackPoints <= 0 && !_player.selectedAttacker.isBlocking)
            {
                _player.hasRotated = false;
                _enemy.isTurn = true;
                _player.isTurn = false;
                _player.hasSelectedEnemy = false;
                _player.hasSelectedCompanion = false;
                
            }
            else if (_player.hasRotated && _player.selectedAttacker.stats.AttackPoints > 0 && !_player.selectedAttacker.isBlocking){
                _player.selectedAttacker.AnimationClass.Play(AnimationClass.states.Selected);
                _player.isTurn = true;
                _enemy.isTurn = false;
                _enemy.hasSelectedEnemy = false;
                // change to true if player shouldn't switch after first selecting
                _player.hasSelectedCompanion = false;
                _player.hasSelectedEnemy = false;
                _player.hasRotated = false;
            }
        }
        private IEnumerator damage(BaseCharacter victim, checkStruct attacker, Action <bool> hasDamaged)
        {
            StaticManager.uiInventory.ShowNotification("3 attack points have been subtracted", 3);

            attacker.selectedAttacker.AnimationClass.Play(AnimationClass.states.Attacking);

            yield return new WaitForSeconds(2);

            attacker.selectedAttacker.AnimationClass.Stop(AnimationClass.states.Attacking);
            
            attacker.selectedAttacker.stats.AttackPoints -= attacker.selectedAttacker.stats.attackCost;
            
            if (attacker.selectedAttacker.stats.AttackPoints < 0)
            {
                attacker.selectedAttacker.stats.AttackPoints = 0;
            }
            if (!victim.isBlocking)
            {
                victim.Damage(attacker.selectedAttacker);

            }
            else{
                StaticManager.uiInventory.ShowNotification("Victim has blocked!", 3);
                victim.isBlocking = false;
            }
           
            hasDamaged( true );
        }

        private bool result;
        private IEnumerator returnToPoint(Vector3 point, NavMeshAgent enemy, Action<bool> myVariableResult) {
            enemy.SetDestination(point);

            while (StaticManager.utility.NavDistanceCheck(enemy) == DistanceCheck.HasNotReachedDestination)
            {
                yield return new WaitForEndOfFrame();
            }

            myVariableResult( true );
        }

        private Vector3 PlayerStartPos;

        private Quaternion PlayerStartRotation;

        public static int stoppingDistance = 0;

        public IEnumerator NavDistanceCheck(NavMeshAgent agent, Action <bool> hasReached) {
            timer = 0;
            while (StaticManager.utility.NavDistanceCheck(agent) == DistanceCheck.HasNotReachedDestination && timer < navTime)
            {
                yield return new WaitForEndOfFrame();
            }

            hasReached( true );
        }

        private bool continueAttack = false;

        private bool dontcontinue = true;

        private RaycastHit l_hitInfo;

        private Enemy hitenemy;

       
        private void SelectEnemy()
        {
            if (Input.GetMouseButtonDown(0) )
            {
                if ( _player.selectedAttacker.stats.AttackPoints <= 0 ){
                    StaticManager.uiInventory.ShowNotification("Uh-oh, this player has no attack points left!", 5);
                }
                l_hitInfo = new RaycastHit();
                var hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out l_hitInfo);

                if (hit)
                {
                    Debug.Log("Hit " + l_hitInfo.transform.gameObject.name);

                    if (l_hitInfo.transform.gameObject.tag == "Enemy" && _player.selectedAttacker.gameObject != null)
                    {
                        hitenemy =  l_hitInfo.transform.gameObject.GetComponent<Enemy>();
                        StaticManager.uiInventory.itemsInstance.AttackConfirmation.SetActive(true);
                        Time.timeScale = 0;
                        _player.hasSelectedCompanion = true;
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
        public void continueattack()
        {

            hitenemy.transform.gameObject.GetComponent<BaseCharacter>().material.color = hitenemy.transform.gameObject.GetComponent<BaseCharacter>().IsChosenByEnemy;
            CameraController.controller.SwitchMode(CameraMode.Freeze);
            PlayerStartPos = _player.selectedAttacker.transform.position;
            PlayerStartRotation = _player.selectedAttacker.transform.localRotation;

            StaticManager.utility.EnableObstacle( _player.selectedAttacker.agent, true);
             _player.selectedAttacker.agent.stoppingDistance = stoppingDistance;

             _player.selectedAttacker.agent.SetDestination(hitenemy.transform.gameObject.transform.position + hitenemy.transform.gameObject.transform.forward * 2);
             _player.selectedVictim = hitenemy.transform.gameObject.GetComponent<Enemy>();
            StartCoroutine(NavDistanceCheck( _player.selectedAttacker.agent, _bool => _player.hasReached = _bool));

            _player.hasSelectedEnemy = true;
        }

        private bool isPaused = false;
        private void EnemysTurn()
        {
            if (!_enemy.hasSelectedCompanion && !isPaused && ((_enemy.selectedAttacker == null || _enemy.selectedAttacker.isBlocking || _enemy.selectedAttacker.stats.AttackPoints < 1 || !_enemy.isTurn)))
            {
                if (_enemy.selectedAttacker)
                _enemy.selectedAttacker.RegenerateAttackPoints(true);
                if (_player.selectedAttacker)
                    _player.selectedAttacker.RegenerateAttackPoints(true);

                SelectRandomEnemy();
            }
            
            if (_enemy.hasSelectedCompanion && !_enemy.hasSelectedEnemy && !_enemy.selectedAttacker.isBlocking && _enemy.selectedAttacker.stats.AttackPoints > 0)
            {
                SelectRandomPlayer();
            }
            else if (!_enemy.hasSelectedEnemy && _enemy.hasSelectedCompanion && (_enemy.selectedAttacker.isBlocking || _enemy.selectedAttacker.stats.AttackPoints <= 0))
            {
                _enemy.hasSelectedCompanion = false;
                _enemy.hasRotated = false;
                _player.isTurn = true;
                _enemy.isTurn = false;
            }

            // if player reached _player
            if (_enemy.hasReached)
            {
                _enemy.hasReached = false;

                StartCoroutine(damage(_enemy.selectedVictim, _enemy, _bool => _enemy.hasDamaged = _bool));

            }

            if (_enemy.hasDamaged)
            {
                _enemy.hasDamaged = false;
                _enemy.selectedVictim.material.color = _enemy.selectedVictim.BaseColor;
                StartCoroutine(returnToPoint(EnemyStartPos, _enemy.selectedAttacker.agent, _result => _enemy.hasReturned = _result));

            }
            // if player returned to point and hasn't turned yet
            if (_enemy.hasReturned && !_enemy.hasRotated)
            {
                _enemy.hasReturned = false;

                //turn to original rotation
                StartCoroutine(Turn(_enemy.selectedAttacker.agent, _player.leader.transform.position, EnemyStartRotation, _bool => _enemy.hasRotated = _bool));
            }

            //if it turned
            if (_enemy.hasRotated && _enemy.selectedAttacker.stats.AttackPoints <= 0 && !_enemy.selectedAttacker.isBlocking){
                _player.selectedAttacker.isBlocking = false;
                _enemy.hasRotated = false;
                _player.isTurn = true;
                _enemy.isTurn = false;
                _enemy.hasSelectedEnemy = false;
            }
            else if (_enemy.hasRotated && _enemy.selectedAttacker.stats.AttackPoints > 0 && !_enemy.selectedAttacker.isBlocking)
            {
                _enemy.isTurn = true;
                _player.isTurn = false;
                _player.hasSelectedEnemy = false;
                _enemy.hasSelectedCompanion = true;
                _enemy.hasSelectedEnemy = false;
                _enemy.hasRotated = false;

            }
        }

       

        private Vector3 EnemyStartPos;

        private Quaternion EnemyStartRotation;

        private int selected = 0;

        private void SelectRandomEnemy()
        {

            if ( selected > _enemy.characters.Count - 1 ){
                selected = 0;
            }
            if ( _enemy.characters.Count == 1 ){
                _enemy.selectedAttacker = _enemy.leader;
            }
            else{
                _enemy.selectedAttacker = _enemy.characters[selected];
            }

            selected++;
           _enemy.selectedAttacker.AnimationClass.Play(AnimationClass.states.Selected);

            int isBlocking = Random.Range( 0 , 35 );

            if ( isBlocking < 35 && _enemy.selectedAttacker.stats.AttackPoints > 3 ){
                _enemy.selectedAttacker.AnimationClass.Play(AnimationClass.states.Selected);
                _enemy.selectedAttacker.isBlocking = true;
                _enemy.selectedAttacker.stats.AttackPoints = 0;
                _enemy.hasRotated = false;
                _player.isTurn = true;
                _enemy.isTurn = false;
                _enemy.hasSelectedEnemy = false;
                _enemy.hasSelectedCompanion = false;
                StartCoroutine( Pause( enemyPauseTime , _bool => _enemy.hasSelectedCompanion = _bool, _bool1 => isPaused = _bool1 ) );

                return;
            }
            else{
                _enemy.selectedAttacker.isBlocking = false;
            }
            
            _enemy.AllAttackptsAre0 = checkAttackpts( _enemy.characters );

            _enemy.hasSelectedCompanion = true;


        }

        public IEnumerator Pause(int time, Action <bool> _bool, Action <bool> _isPaused ) {
            StaticManager.uiInventory.ShowNotification("Enemy has chosen to block", 5);
            _isPaused( true );
            yield return new WaitForSeconds(time);
            _bool( true );
            _isPaused( false );

        }

        bool checkAttackpts( List <BaseCharacter> character ) {
            foreach ( var VARIABLE in character ){
                if ( VARIABLE.stats.AttackPoints > 0 ){
                    return false;
                }
            }
            return true;
        }
        void SelectRandomPlayer( ) {
            if (_player.characters.Count == 1)
            {
                _enemy.selectedVictim = _player.characters[0];
            }
            else
            {
                var randomPlayer = Random.Range(1, _player.characters.Count - 1);
                _enemy.selectedVictim = _player.characters[randomPlayer];
            }
            EnemyStartPos = _enemy.selectedAttacker.transform.position;
            EnemyStartRotation = _enemy.selectedAttacker.transform.localRotation;

            StaticManager.utility.EnableObstacle(_enemy.selectedAttacker.Nav.Agent, true);
            _enemy.selectedAttacker.material.color = _enemy.selectedAttacker.IsChosenBySelf;
            _enemy.selectedVictim.material.color = _enemy.selectedVictim.IsChosenByEnemy;

            _enemy.selectedAttacker.GetComponent<NavMeshAgent>().SetDestination(_enemy.selectedVictim.gameObject.transform.position + _enemy.selectedVictim.gameObject.transform.forward * 2);

            StartCoroutine(NavDistanceCheck(_enemy.selectedAttacker.Nav.Agent, _bool => _enemy.hasReached = _bool));

            _enemy.hasSelectedEnemy = true;
        }
        

        public void BattleWon( ) {
            
            StaticManager.character.controller.SetControlled( true );
            CameraController.controller.SwitchMode(CameraMode.Player);
            _player.selectedAttacker.AnimationClass.Stop(AnimationClass.states.Selected);
            StaticManager.uiInventory.ShowBattleWon(true);
            for ( int i= 0; i < _player.characters.Count; i++ ){
                if ( _player.characters[i] != _player.leader ){
                        StaticManager.utility.EnableObstacle( _player.characters[ i ].Nav.Agent , true );
                        _player.characters[ i ].Nav.SetState = BaseNav.state.Follow;
                        _player.characters[i].RegenerateAttackPoints(false);
                    }
                else{
                    _player.characters[ i ].material.color = _player.characters[ i ].LeaderColor;
                    _player.characters[i].RegenerateAttackPoints(false);
                    _player.characters.RemoveAt(i);
                    i--;
                }
                   
            }

            for ( int i = 0 ; i < _enemy.characters.Count ; i++ ){
                _enemy.characters[i].stats.RegenerateAttackPoints();
            }
            StaticManager.uiInventory.CompanionStatShowWindow(false);
            Destroy(this);
        }


        private void LineUpEnemies()
        {
            if ( _enemy.characters.Count == 0 ){
                _enemy.hasCompanionsLinedUp = true;
                return;
            }
            var right = 2;
            var left = 2;
            var adjustright = 2;
            var adjustleft = 2;

            var forward = -1;

            for (var k = 0; k < _enemy.characters.Count; k += 2)
            {
                  right += _enemy.characters[k].stats.Strength > 15 ? 2 : 0;
                _enemy.characters[k].Nav.SetState = BaseNav.state.Battle;

                forward -= 1;
                var move = obj.transform.position + obj.transform.right * right + obj.transform.forward * -forward;

                _enemy.characters[k].Nav.Agent.stoppingDistance = 0.0f;
                _enemy.characters[k].Nav.Agent.SetDestination(move);
                right += 2;
            }

            forward = -1;

            for (var k = 1; k < _enemy.characters.Count; k += 2)
            {
                left += _enemy.characters[k].stats.Strength > 15 ? 2 : adjustleft + 0;

                _enemy.characters[k].Nav.SetState = BaseNav.state.Battle;
                forward -= 1;
                var moveleft = obj.transform.position + -obj.transform.right * left + obj.transform.forward * -forward;
                _enemy.characters[k].Nav.Agent.stoppingDistance = 0;
                _enemy.characters[k].Nav.Agent.SetDestination(moveleft);
                left += 2;
            }

            int p = 0;
            for (var i = 0; i < _enemy.characters.Count; i++){
                _enemy.count++;
                //StaticManager.utility.EnableObstacle( Enemies[ i ].Nav.Agent , true );
                StartCoroutine(TurnEnemy((Enemy)_enemy.characters[i], _enemy, _bool => _enemy.hasCompanionsLinedUp = _bool, _player.leader.transform.position));
            }
        }
        

        private List < Companion > holderCompanions;
        private void LineUpCompanions() {
            holderCompanions = new List < Companion >();
            
            var right = 2;
            var left = 2;
            var adjustright = 2;
            var adjustleft = 2;
            var forward = -1;
            
            for (var k = 0; k < _player.characters.Count; k += 2)
            {
                 right += _player.characters[k].stats.Strength > 15 ?  2 :  0;
                forward -= 1;
                _player.characters[k].Nav.SetState = BaseNav.state.Attacking;
                var move = _player.leader.gameObject.transform.position + _player.leader.gameObject.transform.right * right + _player.leader.transform.forward * -forward;

                _player.characters[k].Nav.Agent.stoppingDistance = 0.0f;
                _player.characters[k].Nav.Agent.SetDestination(move);
                right += 2;
            }

            forward = -1;

            for (var k = 1; k < _player.characters.Count; k += 2)
            {
                left += _player.characters[k].stats.Strength > 15 ?  2 :  0 ;
                forward -= 1;
                _player.characters[k].Nav.SetState = BaseNav.state.Attacking;

                var moveleft = _player.leader.gameObject.transform.position + -_player.leader.gameObject.transform.right * left + _player.leader.transform.forward * -forward;
                _player.characters[k].Nav.Agent.stoppingDistance = 0;
                _player.characters[k].Nav.Agent.SetDestination(moveleft);
                left += 2;
            }

            
            for (var i = 0; i < _player.characters.Count; i++){
                _player.count++;
                StartCoroutine(TurnEnemy((Companion)_player.characters[i], _player, _bool => _player.hasCompanionsLinedUp = _bool, _enemy.leader.transform.position ));
            }

            if ( _player.characters.Count == 0 ){
                _player.hasCompanionsLinedUp = true;
            }
        }
        int p = 0;
        private IEnumerator TurnEnemy(BaseCharacter enemy, checkStruct character, Action <bool> hasTurned, Vector3 lookat) {
            timer = 0;

            if ( character.characters.Count == 0 ){
                character.hasCompanionsLinedUp = true;

                yield break;
            }
            while (StaticManager.utility.NavDistanceCheck(enemy.Nav.Agent) == DistanceCheck.HasNotReachedDestination && timer < navTime)
            {
                yield return new WaitForEndOfFrame();
            }

            timer = 0;

            StaticManager.utility.EnableObstacle(enemy.Nav.Agent);

            enemy.gameObject.GetComponent<Collider>().isTrigger =
                    !enemy.gameObject.GetComponent<Collider>().isTrigger;

            timer = 0;

            var r = Quaternion.LookRotation(lookat - enemy.transform.position);

            while (enemy.transform.rotation != r && timer < turnTime)
            {
                enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, r, 5 * Time.deltaTime);
                timer += Time.deltaTime;

                yield return new WaitForEndOfFrame();
            }

            if (timer >= turnTime)
            {
                enemy.transform.rotation = r;
            }

            timer = 0;

            enemy.gameObject.GetComponent<Collider>().isTrigger =
                    !enemy.gameObject.GetComponent<Collider>().isTrigger;

            
            if (character.count >= character.characters.Count )
            {
                character.count = 0;
                hasTurned( true );
            }
        }
        

        private IEnumerator Turn(NavMeshAgent agent, Vector3 toRotateTopos, Quaternion rotation, Action <bool> hasRotated)
        {
            _player.hasRotated = false;

            if (StaticManager.utility.NavDistanceCheck(agent) == DistanceCheck.HasNotReachedDestination)
            {
                yield return new WaitForEndOfFrame();
            }

            else
            {
                StaticManager.utility.EnableObstacle(agent);

                agent.gameObject.GetComponent<Collider>().isTrigger =
                        !agent.gameObject.GetComponent<Collider>().isTrigger;

                float timer = 0;
                var r = Quaternion.LookRotation(toRotateTopos - agent.transform.position);

                while (agent.transform.rotation != r && timer < turnTime)
                {
                    agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, r, 5 * Time.deltaTime);
                    timer += Time.deltaTime;

                    yield return new WaitForEndOfFrame();
                }

                if (timer >= turnTime)
                {
                    agent.transform.rotation = r;
                }

                timer = 0;

                agent.gameObject.GetComponent<Collider>().isTrigger =
                        !agent.gameObject.GetComponent<Collider>().isTrigger;

                hasRotated( true );
            }
        }
    }
    public class Ref<T>
    {
        private T backing;
        public T Value { get { return backing; } }
        public Ref(T reference)
        {
            backing = reference;
        }
    }
}