using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

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
        private int health = 200;

        private Animator animator;

        private GameObject UI;
        private Text healthUI;
        private Text enemyUI;

        [SerializeField] public GameObject weaponAttach;
        

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

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                animator.SetBool("Attacking", true);
                PlayerInventory.attachedWeapon.PlayUsing();
                enemy = FindClosestEnemy();
            }
        }

        GameObject FindClosestEnemy()
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

        public void EndAttack()
        {
            if (enemy != null)
            {
                enemy.GetComponent<Kristal.Enemy>().Damage(playerDamage);
                enemyUI.text = "Enemy Health: " + enemy.GetComponent<Kristal.Enemy>().GetHealth().ToString();
            }

            if (health <= 0)
            {
                animator.SetBool("Dying", true);
            }
            animator.SetBool("Attacking", false);
            PlayerInventory.attachedWeapon.StopUsing();

            PlayerInventory.inventory.GetWeapon().Attack();
        }

        public void Damage(int damage)
        {
            health -= damage;
            Debug.Log("Player Health: " + health);
            healthUI.text = "Player Health: " + health.ToString();
        }

        void EndDeath()
        {
            //Destroy(this.gameObject);
        }
    }
}
