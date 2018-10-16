using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Assets.Meyer.TestScripts;
using Assets.Meyer.TestScripts.Player;

using Kristal;

using TMPro;

using UnityEngine;
using UnityEngine.AI;

namespace Assets.Meyer.TestScripts
{
    public class TurnBasedController : MonoBehaviour {

        public List < Enemy > Enemies;

        public List<Companion> Companions;

        public Companion PlayerSelectedCompanion;

        public Enemy PlayerSelectedEnemy;

        public CompanionLeader CompanionLeader;

        public Enemy EnemyLeader;

        public bool AttackMode;

        public static TurnBasedController instance;

        public int CompanionCount;

        private float timer = 0;

        [SerializeField] float turnTime = 1;

        //private float coroutineTimer;

        public void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(instance);
            }

            DontDestroyOnLoad(gameObject);
        }

        private bool addedEnemyLeader = false;
        private void Update() {
            timer += Time.deltaTime;
           

            if (AttackMode)
            {
                //If enemies & companions aren't lined up
                if (!initalized)
                {
                    Debug.Log("Enemycount: " + Enemies.Count + "           Initalize- line: 66");

                    Initalize();
                    switchCompanionSelected = true;
                }

                if ( initalized && !addedEnemyLeader ){
                    isPlayerTurn = true;
                    addedEnemyLeader = true;
                    Enemies.Add(EnemyLeader);

                }

                if ( addedEnemyLeader && isPlayerTurn && ( !hasSelectedCompanion ) ){
                    SelectCompanion();
                }
                //if initalized & it's the players turn & either the player hasn't selected a companion OR they havent selected an enemy to attack
                if (addedEnemyLeader && isPlayerTurn && (hasSelectedCompanion || !isEnemySelected))
                {

                    //Select enemy
                    SelectEnemy();
                }

                //if it's the players turn and they have selected an enemy & have selected a companion
                if (isPlayerTurn && isEnemySelected)
                {
                    Debug.Log("Enemycount: " + Enemies.Count + "           Update- line: 87");

                    //Take players turn
                    PlayersTurn();
                }

                //if it's the enemys turn
                else if (isEnemyTurn)
                {

                    //take enemys turn
                    EnemysTurn();
                }
            }
        }
        private bool initalized;

        private bool setUpEnimies;

        private bool hasCompanionsLinedUp;

        private void Initalize()
        {
            if (hasCompanionsLinedUp)
            {
                foreach ( var VARIABLE in Companions ){
                    VARIABLE.material.color = VARIABLE.BaseColor;
                }

                foreach ( var VARIABLE in Enemies ){
                    VARIABLE.material.color = VARIABLE.BaseColor;
                }
                hasCompanionsLinedUp = false;
                Companions.Insert(0, CompanionLeader);
                initalized = true;
            }
        }

        public void HasCollided(Enemy enemy)
        {
            if (!AttackMode){
                CompanionLeader = Character.player.GetComponent < CompanionLeader >( );
                Enemies = enemy.Leader.characters.Cast<Enemy>().ToList();
                EnemyLeader = enemy.Leader.Leader;
                Companions = CompanionLeader.characters.Cast<Companion>().ToList();
                AttackMode = true;

                if ( CompanionLeader.agent.enabled == true ){
                CompanionLeader.agent.isStopped = true;
                }

                foreach ( var VARIABLE in Companions ){
                    StaticManager.utility.EnableObstacle(VARIABLE.agent, true);
                }

                foreach ( var VARIABLE in Enemies ){
                    VARIABLE.Nav.SetState = EnemyState.Battle;
                }

                CompanionLeader.agent.stoppingDistance = 0.1f;
                StaticManager.character.controller.SetControlled(false);
                CompanionCount = Companions.Count;
                EnemyLeader.Nav.SetState = EnemyState.Battle;
                Debug.Log("Enemycount: " + Enemies.Count + "           HasCollided- line: 141");

                StartBattle();
            }
        }

        public void StartBattle()
        {
            CameraController.controller.SwitchMode(CameraMode.Battle, CompanionLeader);

            StartCoroutine(LineUpLeader());
        }

        private GameObject obj;
        private IEnumerator LineUpLeader() {
            obj = new GameObject("basePos");
            obj.transform.position = Character.player.transform.position + Character.player.transform.forward * 15.0f;
            obj.transform.LookAt(Character.player.transform);
            StaticManager.utility.EnableObstacle(EnemyLeader.Nav.Agent, true);
            EnemyLeader.Nav.Agent.stoppingDistance = 0;
            EnemyLeader.Nav.SetDestination(Character.player.transform.position + Character.player.transform.forward * 15.0f);
            Debug.Log("Enemycount: " + Enemies.Count + "           LineUpLeader- line: 162");

            LineUpCompanions();
            LineUpEnemies();

            while (StaticManager.utility.NavDistanceCheck(EnemyLeader.Nav.Agent) == DistanceCheck.HasNotReachedDestination)
            {
                yield return new WaitForEndOfFrame();
            }
            EnemyLeader.gameObject.transform.LookAt(Character.player.transform);
        }


        [SerializeField] private int index = 0;

        private bool hasSelectedCompanion;

        public bool switchCompanionSelected;

        private void SelectCompanion()
        {
            if (isPlayerTurn && AttackMode || switchCompanionSelected)
            {
                switchCompanionSelected = false;
               StaticManager.uiInventory.CompanionStatShowWindow(true);

                hasSelectedCompanion = true;

                if (index >= Companions.Count || Companions[index].gameObject == null)
                {
                    index = 0;
                }

                PlayerSelectedCompanion = Companions[index];
                PlayerSelectedCompanion.material.color = PlayerSelectedCompanion.IsChosenBySelf;
                if (index == 0)
                {
                    Companions[Companions.Count - 1].material.color =  Companions[Companions.Count - 1].BaseColor;
                }
                else
                {
                    Companions[index - 1].material.color = Companions[index - 1].BaseColor;
                }

                StaticManager.uiInventory.UpdateCompanionStats(PlayerSelectedCompanion.gameObject.GetComponent<Stat>());

                CameraController.controller.SwitchMode(CameraMode.ToOtherPlayer, PlayerSelectedCompanion);
                index += 1;
            }
        }

        private bool isPlayerTurn;

        private bool switchToPlayerTurn;

        private bool isEnemySelected;

        private bool isReached;

        private bool isReturned;

        private bool hasRotated;

        private void PlayersTurn()
        {
            // if player reached enemy
            if (isReached)
            {
                isReached = false;

                StartCoroutine(damage(PlayerSelectedEnemy));

            }

            if (hasDamaged)
            {
                hasDamaged = false;
                PlayerSelectedEnemy.material.color = PlayerSelectedEnemy.BaseColor;
                StartCoroutine(returnToPoint(PlayerStartPos, PlayerSelectedCompanion.agent));

            }
            // if player returned to point and hasn't turned yet
            if (isReturned && !hasRotated){
                isReturned = false;
                
                //turn to original rotation
                StartCoroutine(Turn(PlayerSelectedCompanion.agent, EnemyLeader.transform.position, PlayerStartRotation));
            }

            //if it turned
            if (hasRotated)
            {
                hasRotated = false;
                isEnemyTurn = true;
                isPlayerTurn = false;
            }
        }

        private IEnumerator damage(Enemy enemy)
        {
            
            PlayerSelectedCompanion.AnimationClass.Play(AnimationClass.states.Attacking);

            yield return new WaitForSeconds(2);

            PlayerSelectedCompanion.AnimationClass.Stop(AnimationClass.states.Attacking);

            Debug.Log("Enemycount: " + Enemies.Count + "           Damage- line: 271");
            
            enemy.Damage(PlayerSelectedCompanion);
            hasDamaged = true;
        }

        private IEnumerator returnToPoint(Vector3 point, NavMeshAgent enemy)
        {
            enemy.SetDestination(point);

            while (StaticManager.utility.NavDistanceCheck(enemy) == DistanceCheck.HasNotReachedDestination)
            {
                yield return new WaitForEndOfFrame();
            }

            isReturned = true;
        }

        private Vector3 PlayerStartPos;

        private Quaternion PlayerStartRotation;

        public static int stoppingDistance = 0;

        public IEnumerator NavDistanceCheck(NavMeshAgent agent) {
            timer = 0;
            while (StaticManager.utility.NavDistanceCheck(agent) == DistanceCheck.HasNotReachedDestination && timer < 5.0f)
            {
                yield return new WaitForEndOfFrame();
            }

            isReached = true;
        }

        private void SelectEnemy()
        {
            if (Input.GetMouseButtonDown(0) && hasSelectedCompanion)
            {
                var l_hitInfo = new RaycastHit();
                var hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out l_hitInfo);

                if (hit)
                {
                    Debug.Log("Hit " + l_hitInfo.transform.gameObject.name);

                    if (l_hitInfo.transform.gameObject.tag == "Enemy" && PlayerSelectedCompanion.gameObject != null){
                        l_hitInfo.transform.gameObject.GetComponent < BaseCharacter >( ).material.color = l_hitInfo.transform.gameObject.GetComponent < BaseCharacter >( ).IsChosenByEnemy;
                        CameraController.controller.SwitchMode(CameraMode.Freeze);
                        PlayerStartPos = PlayerSelectedCompanion.transform.position;
                        PlayerStartRotation = PlayerSelectedCompanion.transform.localRotation;

                        StaticManager.utility.EnableObstacle(PlayerSelectedCompanion.agent, true);
                        PlayerSelectedCompanion.agent.stoppingDistance = stoppingDistance;

                        PlayerSelectedCompanion.agent.SetDestination(l_hitInfo.transform.gameObject.transform.position + l_hitInfo.transform.gameObject.transform.forward * 2);
                        PlayerSelectedEnemy = l_hitInfo.transform.gameObject.GetComponent<Enemy>(); StartCoroutine(NavDistanceCheck(PlayerSelectedCompanion.agent));

                        isEnemySelected = true;
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

        private bool isEnemyTurn;

        private bool switchToEnemiesTurn;

        private bool isRandomPlayerSelected;

        private bool hasDamaged = false;

        private void EnemysTurn()
        {
            //if enemy hasn't selected a random player
            if (!isRandomPlayerSelected)
            {
                isRandomPlayerSelected = true;

                //select random player
                SelectRandomPlayer();
            }

            //if enemy has reached random player
            if (isReached)
            {
                isReached = false;
                StartCoroutine(damage(enemySelectedCompanion));
            }
            //if enemy has done their damage routine
            if (hasDamaged){
                enemySelectedEnemy.material.color = enemySelectedEnemy.BaseColor;
                enemySelectedCompanion.material.color = enemySelectedCompanion.BaseColor;
                hasDamaged = false;
                //return enemy to starting point
                StartCoroutine(returnToPoint(EnemyStartPos, enemySelectedEnemy.Nav.Agent));

            }
            //if enemy has returned to starting point
            if (isReturned)
            {
                isReturned = false;

                // turn to face original direction 
                StartCoroutine(Turn(enemySelectedEnemy.Nav.Agent, Character.player.transform.position, EnemyStartRotation));
            }

            // if enemy has turn to face original direction
            if (hasRotated)
            {
                hasRotated = false;

                //take players turn
                isPlayerTurn = true;
                isEnemyTurn = false;
                isRandomPlayerSelected = false;
                hasSelectedCompanion = false;
                isEnemySelected = false;
            }
        }

        private Companion enemySelectedCompanion;

        private Enemy enemySelectedEnemy;

        private Vector3 EnemyStartPos;

        private Quaternion EnemyStartRotation;

        private int selected = 0;

        private void SelectRandomPlayer()
        {
            if ( Enemies.Count == 1 ){
                enemySelectedEnemy = EnemyLeader;
            }
            else{
                enemySelectedEnemy = Enemies[selected];
            }

            if ( Companions.Count == 1 ){
                enemySelectedCompanion = Companions[ 0 ];
            }
            else{
                var randomPlayer = Random.Range(1, Companions.Count - 1);
                enemySelectedCompanion = Companions[randomPlayer];
            }
            

            EnemyStartPos = enemySelectedEnemy.transform.position;
            EnemyStartRotation = enemySelectedEnemy.transform.localRotation;

            StaticManager.utility.EnableObstacle(enemySelectedEnemy.Nav.Agent, true);
            enemySelectedEnemy.material.color = enemySelectedEnemy.IsChosenBySelf;
            enemySelectedCompanion.material.color = enemySelectedCompanion.IsChosenByEnemy;

            enemySelectedEnemy.GetComponent<NavMeshAgent>().SetDestination(enemySelectedCompanion.gameObject.transform.position + enemySelectedCompanion.gameObject.transform.forward * 2);

            StartCoroutine(NavDistanceCheck(enemySelectedEnemy.Nav.Agent));
            selected++;
        }

        IEnumerator damage(Companion companion)
        {
            enemySelectedEnemy.AnimationClass.Play(AnimationClass.states.Attacking);

            yield return new WaitForSeconds(1);

            enemySelectedEnemy.AnimationClass.Stop(AnimationClass.states.Attacking);

            companion.Damage(enemySelectedEnemy);

            hasDamaged = true;
        }
        

        public void BattleWon( ) {
            
            StaticManager.character.controller.SetControlled( true );
            CameraController.controller.SwitchMode(CameraMode.Player);
            for ( int i= 0; i < Companions.Count; i++ ){
                if ( Companions[i] != CompanionLeader ){
                        StaticManager.utility.EnableObstacle( Companions[ i ].Nav.Agent , true );
                        Companions[ i ].Nav.Switch( CompanionNav.CompanionState.Follow );
                    }
                else{
                    Companions[ i ].material.color = Companions[ i ].LeaderColor;
                    Companions.RemoveAt(i);
                    i--;
                }
                   
            }
            StaticManager.uiInventory.CompanionStatShowWindow(false);
            Destroy(this);
        }


        private void LineUpEnemies()
        {
            if ( Enemies.Count == 0 ){
                setUpEnimies = true;
                return;
            }
            var right = 2;
            var left = 2;

            var forward = -1;

            for (var k = 0; k < Enemies.Count; k += 2)
            {
                Enemies[k].Nav.SetState = EnemyState.Battle;

                forward -= 1;
                var move = obj.transform.position + obj.transform.right * right + obj.transform.forward * -forward;

                Enemies[k].Nav.Agent.stoppingDistance = 0.0f;
                Enemies[k].Nav.Agent.SetDestination(move);
                right += 2;
            }

            forward = -1;

            for (var k = 1; k < Enemies.Count; k += 2)
            {
                Enemies[k].Nav.SetState = EnemyState.Battle;
                forward -= 1;
                var moveleft = obj.transform.position + -obj.transform.right * left + obj.transform.forward * -forward;
                Enemies[k].Nav.Agent.stoppingDistance = 0;
                Enemies[k].Nav.Agent.SetDestination(moveleft);

                left += 2;
            }

            for (var i = 0; i < Enemies.Count; i++)
            {
                //StaticManager.utility.EnableObstacle( Enemies[ i ].Nav.Agent , true );
                StartCoroutine(TurnEnemy(Enemies[i], i));
            }
        }


        private int p;

        private List < Companion > holderCompanions;
        private void LineUpCompanions() {
            holderCompanions = new List < Companion >();
            var right = 2;
            var left = 2;
            var forward = -1;
            
            for (var k = 0; k < CompanionCount; k += 2)
            {
                forward -= 1;
                Companions[k].Nav.State = CompanionNav.CompanionState.Attacking;
                var move = CompanionLeader.gameObject.transform.position + CompanionLeader.gameObject.transform.right * right + CompanionLeader.transform.forward * -forward;

                Companions[k].Nav.Agent.stoppingDistance = 0.0f;
                Companions[k].Nav.Agent.SetDestination(move);
                right += 2;
                holderCompanions.Add(Companions[k]);
            }

            forward = -1;

            for (var k = 1; k < CompanionCount; k += 2)
            {
                forward -= 1;
                Companions[k].Nav.State = CompanionNav.CompanionState.Attacking;

                var moveleft = CompanionLeader.gameObject.transform.position + -CompanionLeader.gameObject.transform.right * left + CompanionLeader.transform.forward * -forward;
                Companions[k].Nav.Agent.stoppingDistance = 0;
                Companions[k].Nav.Agent.SetDestination(moveleft);

                holderCompanions.Add(Companions[k]);

                left += 2;
            }

            for (var i = 0; i < CompanionCount; i++)
            {
                StartCoroutine(TurnCompanion(Companions[i], i));
            }

            if ( Companions.Count == 0 ){
                hasCompanionsLinedUp = true;
            }
            Companions = holderCompanions;
        }

        private IEnumerator TurnEnemy(Enemy enemy, int i) {
            timer = 0;
            while (StaticManager.utility.NavDistanceCheck(enemy.Nav.Agent) == DistanceCheck.HasNotReachedDestination && timer < 5.0f)
            {
                yield return new WaitForEndOfFrame();
            }

            timer = 0;

            StaticManager.utility.EnableObstacle(enemy.Nav.Agent);

            enemy.gameObject.GetComponent<Collider>().isTrigger =
                    !enemy.gameObject.GetComponent<Collider>().isTrigger;

            timer = 0;

            var r = Quaternion.LookRotation(Character.player.transform.position - enemy.transform.position);

            while (enemy.transform.rotation != r && timer < turnTime)
            {
                enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, r, 5 * Time.deltaTime);
                timer += Time.deltaTime;

                yield return new WaitForEndOfFrame();
            }

            if (timer > turnTime)
            {
                enemy.transform.rotation = r;
            }

            timer = 0;

            enemy.gameObject.GetComponent<Collider>().isTrigger =
                    !enemy.gameObject.GetComponent<Collider>().isTrigger;

            p++;

            if (p >= Enemies.Count)
            {
                p = 0;
                setUpEnimies = true;
            }
            Debug.Log("Enemycount: " + Enemies.Count + "           Turn- line: 623");

        }

        private IEnumerator TurnCompanion(Companion companion, int i) {
            timer = 0;
            while (StaticManager.utility.NavDistanceCheck(companion.Nav.Agent) == DistanceCheck.HasNotReachedDestination && timer < 5.0f)
            {
                yield return new WaitForEndOfFrame();
            }

            timer = 0;
            StaticManager.utility.EnableObstacle(companion.Nav.Agent);

            companion.gameObject.GetComponent<Collider>().isTrigger =
                    !companion.gameObject.GetComponent<Collider>().isTrigger;

            var r = Quaternion.LookRotation(obj.transform.position - companion.transform.position);

            timer = 0;
            while (companion.transform.rotation != r && timer < turnTime)
            {
                companion.transform.rotation = Quaternion.Slerp(companion.transform.rotation, r, 5 * Time.deltaTime);

                yield return new WaitForEndOfFrame();
            }
            if (timer >= turnTime)
            {
                companion.transform.rotation = r;
            }
            timer = 0;
            companion.gameObject.GetComponent<Collider>().isTrigger =
                    !companion.gameObject.GetComponent<Collider>().isTrigger;

            p++;

            if (p >= Companions.Count - 1)
            {
                p = 0;
                hasCompanionsLinedUp = true;
            }
        }

        private IEnumerator Turn(NavMeshAgent agent, Vector3 toRotateTopos, Quaternion rotation)
        {
            hasRotated = false;

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

                while (agent.transform.rotation != r && timer < 3)
                {
                    agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, r, 5 * Time.deltaTime);
                    timer += Time.deltaTime;

                    yield return new WaitForEndOfFrame();
                }

                if (timer > 3)
                {
                    agent.transform.rotation = r;
                }

                timer = 0;

                agent.gameObject.GetComponent<Collider>().isTrigger =
                        !agent.gameObject.GetComponent<Collider>().isTrigger;

                hasRotated = true;
            }
        }
    }
}