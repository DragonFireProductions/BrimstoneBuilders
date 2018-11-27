using Boo.Lang;

using Kristal;

using UnityEngine;

namespace Assets.Meyer.TestScripts.Player {

    public class Character : Companion {

        public static GameObject Player;
        public companionSpawner spawner;

        [ SerializeField ] private GameObject camHolder;

        public bool buttonIsClicked = false;
        //CharacterController controller;
        // Use this for initialization
        private void Awake() {
            base.Awake();
            Player = GameObject.FindWithTag("Player");
            spawner = gameObject.GetComponent<companionSpawner>();
            Nav = gameObject.GetComponent<LeaderNav>();

        }

    }

}