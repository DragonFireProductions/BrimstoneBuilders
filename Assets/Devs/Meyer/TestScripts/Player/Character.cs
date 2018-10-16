using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using UnityEngine.AI;
using System.Collections;
using System.Runtime.CompilerServices;
using Kristal;

using UnityEngine.SceneManagement;

namespace Assets.Meyer.TestScripts.Player
{
    public class Character : MonoBehaviour
    {
        
        public static GameObject player;

        private GameObject UI;
        

        [SerializeField] GameObject camHolder;
        public PlayerController controller;

        //CharacterController controller;
        // Use this for initialization
        void Awake()
        {
            player = GameObject.FindWithTag( "Player" );
            controller = player.GetComponent < PlayerController >( );


    }

       

    // Update is called once per framed
    void Update()
        {
            
        }
    }
}
