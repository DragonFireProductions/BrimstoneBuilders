using System.Collections;
using System.Collections.Generic;

using Assets.Meyer.TestScripts;
using Assets.Meyer.TestScripts.Player;

using Kristal;

using UnityEngine;
using UnityEngine.AI;

namespace Assets.Meyer.TestScripts
{
    public class TurnBasedController : MonoBehaviour
    {

        public List<Enemy> Enemies;

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

       

        private void Update() {
            timer += Time.deltaTime;
            UIInventory.instance.ViewEnemyStats();

            if (AttackMode)
            {
                //If enemies & companions aren't lined up
                if (!initalized)
                {
                    Initalize();
                    isPlayerTurn = true;
                    switchCompanionSelected = true;
                }

                //if initalized & it's the players turn & either the player hasn't selected a companion OR they havent selected an enemy to attack
                if (initalized && isPlayerTurn && (!hasSelectedCompanion || !isEnemySelected))
                {
                    //then
                    //Select companion
                    SelectCompanion();

                    //Select enemy
                    SelectEnemy();
                }

                //if it's the players turn and they have selected an enemy & have selected a companion
                if (isPlayerTurn && isEnemySelected)
                {
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

        public void HasCollided(Enemy enemy)
        {
            if (!AttackMode)
            {
                Enemies = enemy.Leader.EnemyGroup;
                EnemyLeader = enemy.Leader.Leader;
                Companions = Character.instance.leader.CompanionGroup;
                CompanionLeader = Character.instance.gameObject.GetComponent<CompanionLeader>();
                AttackMode = true;

                if ( CompanionLeader.agent.enabled == true ){
                CompanionLeader.agent.isStopped = true;
                }
                Character.player.GetComponent<PlayerController>().enabled = false;
                CompanionCount = Companions.Count;
                EnemyLeader.Nav.SetState = EnemyState.Battle;
                
                StartBattle();
            }
        }

        public void StartBattle()
        {
            CameraController.controller.SwitchMode(CameraMode.Battle);

            StartCoroutine(LineUpLeader());
        }
        

        private IEnumerator LineUpLeader()
        {
            CharacterUtility.instance.EnableObstacle(EnemyLeader.Nav.Agent, true);
            EnemyLeader.Nav.Agent.stoppingDistance = 0;
            EnemyLeader.Nav.SetDestination(Character.player.transform.position + Character.player.transform.forward * 15.0f);

            while (CharacterUtility.instance.NavDistanceCheck(EnemyLeader.Nav.Agent) == DistanceCheck.HasNotReachedDestination)
            {
                yield return new WaitForEndOfFrame();
            }

            EnemyLeader.gameObject.transform.LookAt(Character.player.transform);
            LineUpEnemies();
        }

        

        private int index;

        private bool hasSelectedCompanion;

        public bool switchCompanionSelected;

        private void SelectCompanion()
        {
            if (Input.GetKeyDown(KeyCode.P) && isPlayerTurn && AttackMode || switchCompanionSelected)
            {
                switchCompanionSelected = false;
                UIInventory.instance.CompanionStatShowWindow(true);

                hasSelectedCompanion = true;

                if (index >= Companions.Count - 1 || Companions[index].gameObject == null)
                {
                    index = 0;
                }

                PlayerSelectedCompanion = Companions[index];
                PlayerSelectedCompanion.gameObject.GetComponent<Renderer>().material.color = Color.red;

                if (index == 0)
                {
                    Companions[Companions.Count - 1].gameObject.GetComponent<Renderer>().material.color = Color.blue;
                }
                else
                {
                    Companions[index - 1].gameObject.GetComponent<Renderer>().material.color = Color.blue;
                }

                UIInventory.instance.UpdateCompanionStats(PlayerSelectedCompanion.gameObject.GetComponent<Stat>());

                CameraController.controller.SwitchMode(CameraMode.ToOtherPlayer, PlayerSelectedCompanion);
                index += 1;
            }

            if (PlayerSelectedCompanion != null)
            {
                hasSelectedCompanion = true;
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
            yield return new WaitForSeconds(2);
            enemy.Damage(PlayerSelectedCompanion);
            hasDamaged = true;
        }

        private IEnumerator returnToPoint(Vector3 point, NavMeshAgent enemy)
        {
            enemy.SetDestination(point);

            while (CharacterUtility.instance.NavDistanceCheck(enemy) == DistanceCheck.HasNotReachedDestination)
            {
                yield return new WaitForEndOfFrame();
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

        private void SelectEnemy()
        {
            if (Input.GetMouseButtonDown(0) && hasSelectedCompanion)
            {
                var l_hitInfo = new RaycastHit();
                var hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out l_hitInfo);

                if (hit)
                {
                    Debug.Log("Hit " + l_hitInfo.transform.gameObject.name);

                    if (l_hitInfo.transform.gameObject.tag == "Enemy" && PlayerSelectedCompanion.gameObject != null)
                    {
                        CameraController.controller.SwitchMode(CameraMode.Freeze);
                        PlayerStartPos = PlayerSelectedCompanion.transform.position;
                        PlayerStartRotation = PlayerSelectedCompanion.transform.localRotation;

                        CharacterUtility.instance.EnableObstacle(PlayerSelectedCompanion.agent, true);
                        PlayerSelectedCompanion.agent.stoppingDistance = stoppingDistance;

                        PlayerSelectedCompanion.agent.SetDestination(l_hitInfo.transform.gameObject.transform.position + l_hitInfo.transform.gameObject.transform.forward * 2);
                        PlayerSelectedEnemy = l_hitInfo.transform.gameObject.GetComponent<Enemy>();
                        StartCoroutine(NavDistanceCheck(PlayerSelectedCompanion.agent));

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
            if (hasDamaged)
            {
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

        private void SelectRandomPlayer()
        {
            if ( Enemies.Count == 0 ){
                enemySelectedEnemy = EnemyLeader;
            }
            else{
                var randomEnemy = Random.Range(0, Enemies.Count - 1);
                enemySelectedEnemy = Enemies[randomEnemy];
            }
            

            var randomPlayer = Random.Range(0, Companions.Count - 1);
            enemySelectedCompanion = Companions[randomPlayer];

            EnemyStartPos = enemySelectedEnemy.transform.position;
            EnemyStartRotation = enemySelectedEnemy.transform.localRotation;

            CharacterUtility.instance.EnableObstacle(enemySelectedEnemy.Nav.Agent, true);
            enemySelectedEnemy.Nav.Agent.stoppingDistance = 3;

            enemySelectedEnemy.GetComponent<NavMeshAgent>().SetDestination(enemySelectedCompanion.gameObject.transform.position + enemySelectedCompanion.gameObject.transform.forward * 2);

            StartCoroutine(NavDistanceCheck(enemySelectedEnemy.Nav.Agent));
        }

        IEnumerator damage(Companion companion)
        {
            yield return new WaitForSeconds(1);
            companion.Damage(enemySelectedEnemy);

            hasDamaged = true;
        }

        public void BattleWon( ) {
            initalized = false;
            AttackMode = false;
            isEnemyTurn = false;
            switchToEnemiesTurn = false;
            isRandomPlayerSelected = false;
            hasDamaged = false;
            isPlayerTurn = false;
            switchToPlayerTurn = false;
            isEnemySelected = false;
            isReached = false;
            isReturned = false;
            hasRotated = false;
            enemySelectedCompanion = null;
            enemySelectedEnemy = null;
            PlayerSelectedCompanion = null;
            PlayerSelectedEnemy = null;
            index = 0;
            hasSelectedCompanion = false;
            switchCompanionSelected = false;


            Character.player.GetComponent<PlayerController>().enabled = true;
            CameraController.controller.SwitchMode(CameraMode.Player);
            for ( int i= 0; i < Companions.Count; i++ ){
                if ( Companions[i] != CompanionLeader ){
                    Companions[i].Nav.Switch( CompanionNav.CompanionState.Follow );
                    Companions[i].gameObject.GetComponent < Renderer >( ).material.color = Color.blue;
                }
                else Companions.RemoveAt(i);
            }

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
                var move = EnemyLeader.gameObject.transform.position + EnemyLeader.gameObject.transform.right * right + EnemyLeader.transform.forward * -forward;

                Enemies[k].Nav.Agent.stoppingDistance = 0.0f;
                Enemies[k].Nav.Agent.SetDestination(move);
                right += 2;
            }

            forward = -1;

            for (var k = 1; k < Enemies.Count; k += 2)
            {
                Enemies[k].Nav.SetState = EnemyState.Battle;
                forward -= 1;
                var moveleft = EnemyLeader.gameObject.transform.position + -EnemyLeader.gameObject.transform.right * left + EnemyLeader.transform.forward * -forward;
                Enemies[k].Nav.Agent.stoppingDistance = 0;
                Enemies[k].Nav.Agent.SetDestination(moveleft);

                left += 2;
            }

            for (var i = 0; i < Enemies.Count; i++)
            {
                //CharacterUtility.instance.EnableObstacle( Enemies[ i ].Nav.Agent , true );
                StartCoroutine(TurnEnemy(Enemies[i], i));
            }
        }


        private int p;

        private void LineUpCompanions(List<Companion> companions, CompanionLeader companion_leader)
        {
            var right = 2;
            var left = 2;
            var forward = -1;

            for (var k = 0; k < CompanionCount; k += 2)
            {
                forward -= 1;
                companions[k].Nav.State = CompanionNav.CompanionState.Attacking;
                var move = companion_leader.gameObject.transform.position + companion_leader.gameObject.transform.right * right + CompanionLeader.transform.forward * -forward;

                companions[k].Nav.Agent.stoppingDistance = 0.0f;
                companions[k].Nav.Agent.SetDestination(move);
                right += 2;
            }

            forward = -1;

            for (var k = 1; k < CompanionCount; k += 2)
            {
                forward -= 1;
                companions[k].Nav.State = CompanionNav.CompanionState.Attacking;

                var moveleft = companion_leader.gameObject.transform.position + -companion_leader.gameObject.transform.right * left + CompanionLeader.transform.forward * -forward;
                companions[k].Nav.Agent.stoppingDistance = 0;
                companions[k].Nav.Agent.SetDestination(moveleft);

                left += 2;
            }

            for (var i = 0; i < CompanionCount; i++)
            {
                StartCoroutine(TurnCompanion(companions[i], i));
            }
        }

        private IEnumerator TurnEnemy(Enemy enemy, int i)
        {
            while (CharacterUtility.instance.NavDistanceCheck(enemy.Nav.Agent) == DistanceCheck.HasNotReachedDestination)
            {
                yield return new WaitForEndOfFrame();
            }

            CharacterUtility.instance.EnableObstacle(enemy.Nav.Agent);

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
        }

        private IEnumerator TurnCompanion(Companion companion, int i)
        {
            while (CharacterUtility.instance.NavDistanceCheck(companion.Nav.Agent) == DistanceCheck.HasNotReachedDestination)
            {
                yield return new WaitForEndOfFrame();
            }

            CharacterUtility.instance.EnableObstacle(companion.Nav.Agent);

            companion.gameObject.GetComponent<Collider>().isTrigger =
                    !companion.gameObject.GetComponent<Collider>().isTrigger;

            var r = Quaternion.LookRotation(EnemyLeader.transform.position - companion.transform.position);

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

            if (CharacterUtility.instance.NavDistanceCheck(agent) == DistanceCheck.HasNotReachedDestination)
            {
                yield return new WaitForEndOfFrame();
            }

            else
            {
                CharacterUtility.instance.EnableObstacle(agent);

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