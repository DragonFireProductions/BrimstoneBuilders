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

        private GameObject UI;
        

        [SerializeField] GameObject camHolder;
        public PlayerController controller;
        
        //CharacterController controller;
        void Start() { 
            controller = gameObject.GetComponent < PlayerController >( );
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

            //DontDestroyOnLoad(gameObject);
            player = this.gameObject;

        }

        // Update is called once per framed
        void Update()
        {
            
        }
    }
}
