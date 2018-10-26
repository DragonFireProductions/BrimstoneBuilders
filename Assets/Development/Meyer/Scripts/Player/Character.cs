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
            Player = GameObject.FindWithTag("Player");
            Cube = Player.transform.Find("Cube").gameObject;
            Nav = gameObject.GetComponent<LeaderNav>();
        }


        // Update is called once per framed
        private void Update( ) { }

    }

}