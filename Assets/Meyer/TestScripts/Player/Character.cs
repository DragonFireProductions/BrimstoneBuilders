using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using UnityEngine.AI;
using System.Collections;

namespace Assets.Meyer.TestScripts.Player
{
    public class Character : MonoBehaviour
    {

        public static Character instance;
        public static GameObject player;
        GameObject enemy;

        [SerializeField] private int playerDamage = 10;

        [SerializeField] private float attackDistance = 5;

        [SerializeField]
        private int maxHealth = 200;
        [SerializeField]
        private int maxStamina;
        [SerializeField]
        private int strength;
        [SerializeField]
        private int Speed;
        [SerializeField]
        private int stamina;

        private Animator animator;

        private GameObject UI;
        private Text healthUI;
        private Text enemyUI;

        [SerializeField] GameObject camHolder;

       

        [SerializeField] public GameObject weaponAttach;

        Vector3 new_position;
        //CharacterController controller;
        NavMeshAgent agent;
        public float speed;

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            //controller = GetComponent<CharacterController>();
        }

        // Use this for initialization
        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
            player = GameObject.FindWithTag("Player");

            animator = gameObject.GetComponent<Animator>();
            Assert.IsNotNull(animator, "Animator is null");

            UI = GameObject.Find("HealthUI");
            Assert.IsNotNull(UI, "No player UI attached!");

            Transform canvas = UI.transform.Find("Canvas");

            healthUI = canvas.transform.Find("PlayerHealth").GetComponent<Text>();
            enemyUI = canvas.transform.Find("EnemyHealth").GetComponent<Text>();
            Assert.IsNotNull(healthUI, "Missing playerUI");
            Assert.IsNotNull(enemyUI, "Missing player UI");
        }

        // Update is called once per framed
        void Update()
        {
            if (Input.GetMouseButtonDown(0) && !CharacterUtility.instance.turnbased.enabled)
            {
                RaycastHit hit;

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    if ( !gameObject.GetComponent < NavMeshAgent >( ).enabled ){
                        gameObject.GetComponent < NavMeshAgent >( ).enabled = true;
                    }
                    float step = speed;
                    float distance = Vector3.Distance(transform.position, hit.point);

                        Vector3 pos;
                        pos.x = hit.point.x;
                        pos.y = 0.0f;
                        pos.z = hit.point.z;
                    gameObject.GetComponent < NavMeshAgent >( ).SetDestination( pos );

                }
            }
        }
        

        /// <summary>
        /// Finds the closest enemy to player
        /// </summary>
        /// <returns>Closest enemy game object</returns>
        public GameObject FindClosestEnemy()
        {
            GameObject[] gos;
            gos = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject closest = null;
            float distance = Mathf.Infinity;
            Vector3 position = transform.position;
            foreach (GameObject go in gos)
            {
                Vector3 diff = go.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance && curDistance < attackDistance)
                {
                    closest = go;
                    distance = curDistance;
                }
            }
            return closest;
        }

        public void StartAttackMode( ) {
            TurnBased.Instance.Switch();
        }

        void SelectEnemy( ) {
            
        }
        public void EndAttack()
        {
            if (enemy != null)
            {
                enemy.GetComponent<Kristal.Enemy>().Damage(playerDamage);
            }
            
            animator.SetBool("Attacking", false);
            if (PlayerInventory.attachedWeapon)
            {
                PlayerInventory.attachedWeapon.StopUsing();
            }    
        }

        

        void EndDeath()
        {
            //Destroy(this.gameObject);
        }

        public int GetMaxHealth()
        {
            return maxHealth;
        }

        public int GetMaxStamina()
        {
            return maxStamina;
        }

        public int GetStamina()
        {
            return stamina;
        }

        public int GetStrength()
        {
            return strength;
        }

        public int GetSpeed()
        {
            return Speed;
        }

        public GameObject CamHolder {
            get { return camHolder; }

        }
    }
}
