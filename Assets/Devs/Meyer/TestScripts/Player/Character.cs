using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using UnityEngine.AI;
using System.Collections;
using System.Runtime.CompilerServices;

namespace Assets.Meyer.TestScripts.Player
{
    public class Character : MonoBehaviour
    {

        public static Character instance;
        public static GameObject player;
        public CompanionLeader leader;

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
           
        }
        
    
        public GameObject CamHolder {
            get { return camHolder; }

        }
    }
}
