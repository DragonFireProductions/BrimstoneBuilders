using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using UnityEngine.AI;
using System.Collections;
using System.Runtime.CompilerServices;
using Kristal;

namespace Assets.Meyer.TestScripts.Player
{
    public class Character : MonoBehaviour
    {

        public static Character instance;
        public static GameObject player;
        public CompanionLeader leader;
        public static Enemy enemy;
        public static Stat stat;

        private Animator animator;

        private GameObject UI;

        [SerializeField] GameObject camHolder;


        //CharacterController controller;

        private void Start()
        {
            leader = this.gameObject.GetComponent < CompanionLeader >( );

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
            player = this.gameObject;

        }

        // Update is called once per framed
        void Update()
        {
            float eHealth = enemy.getHealth;
            if (eHealth <= 0.0f)
            {
                ++stat.XP;
            }
        }


        public GameObject CamHolder {
            get { return camHolder; }

        }
    }
}
