using Boo.Lang;

using Kristal;

using UnityEngine;

namespace Assets.Meyer.TestScripts.Player {

    public class Character : Companion {

        public static GameObject Player;

        [ SerializeField ] private GameObject camHolder;

        public GameObject[] line;

        //CharacterController controller;
        // Use this for initialization
        private void Awake() {
            base.Awake();

            line = new GameObject[transform.Find("Line").transform.childCount];
            for ( int i = 0 ; i < line.Length ; i++ ){
                line[ i ] = transform.Find( "Line" ).GetChild( i ).gameObject;
            }
            Player = GameObject.FindWithTag("Player");
         
            Nav = gameObject.GetComponent<LeaderNav>();
            
           
        }
        
        private void Start( ) {
            cube = transform.Find("Cube").gameObject;
            inventory = GetComponent < PlayerInventory >( );
            StaticManager.RealTime.Companions.Add(this);

        }
    }

}