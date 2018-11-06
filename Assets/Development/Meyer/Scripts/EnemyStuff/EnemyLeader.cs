using System;
using System.Collections;
using System.Collections.Generic;

using Assets.Meyer.TestScripts;

using Kristal;

using UnityEngine;

[Serializable]
public class EnemyLeader : Enemy {


    //[SerializeField] List<GameObject> Enemies;

    //public List<Enemy> EnemyGroup;

    public Enemy Leader;
    // Use this for initialization
    protected void Start () {
        base.Start( );

        characters = new List < BaseCharacter >();
        Leader = this.gameObject.GetComponent < Enemy >( );

        gameObject.GetComponent < Enemy >( ).leader = this;

        if ( characterObjs.Count != 0 ){
            foreach (var l_gameObject in characterObjs)
            {
                //characters.Add(l_gameObject.GetComponent<Enemy>());
                //l_gameObject.GetComponent<Enemy>().leader = this;
            }
        }
    }
    
}
