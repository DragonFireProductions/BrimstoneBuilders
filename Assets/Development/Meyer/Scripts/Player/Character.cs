using Boo.Lang;

using Kristal;

using UnityEngine;

namespace Assets.Meyer.TestScripts.Player {

    public class Character : Companion {

        public static GameObject Player;

        [ SerializeField ] private GameObject camHolder;

        public GameObject Cube;


        //CharacterController controller;
        // Use this for initialization
        private void Awake()
        {
            base.Awake();
            Player = GameObject.FindWithTag("Player");
            Cube = Player.transform.Find("Cube").gameObject;
            Nav = gameObject.GetComponent<LeaderNav>();
           
        }

        private void OnTriggerEnter( Collider collider ) {
            
        }

        // Update is called once per framed
        private void Update( ) {
            
        }

    }

}