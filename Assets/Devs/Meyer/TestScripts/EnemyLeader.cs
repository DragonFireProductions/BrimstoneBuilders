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
    void Start () {
        
        characters = new List < BaseCharacter >();
        Leader = this.gameObject.GetComponent < Enemy >( );
        
        gameObject.GetComponent < Enemy >( ).leader = this;

        if ( characterObjs.Count != 0 ){
            foreach (var l_gameObject in characterObjs)
            {
                characters.Add(l_gameObject.GetComponent<Enemy>());
                l_gameObject.GetComponent<Enemy>().leader = this;
            }
        }
    }

    public void AssignNewLeader( ) {
        GameObject NewLeader = TurnBasedController.instance.Enemies[ 0 ].gameObject;

        NewLeader.gameObject.AddComponent < EnemyLeader >( );
        NewLeader.gameObject.GetComponent<EnemyLeader>().characterObjs = new List < GameObject >();
        foreach ( var VARIABLE in TurnBasedController.instance.Enemies ){
            NewLeader.GetComponent < EnemyLeader >( ).characterObjs.Add(VARIABLE.obj);
        }
        TurnBasedController.instance.EnemyLeader = NewLeader.GetComponent < Enemy >( );
        
        Destroy(this.gameObject);
        this.enabled = false;
    }
    void FillOutInfo(List <GameObject> objects ) {
       
    }

    public void Remove(BaseCharacter _obj)
    {
        Debug.Log("Enemycount: " + TurnBasedController.instance.Enemies.Count + "           Remove-EnemyLeader- line: 64");

        if (TurnBasedController.instance.Enemies.Count == 1 && _obj == this.Leader)
        {
            characterObjs.Clear();
            characters.Clear();
            TurnBasedController.instance.BattleWon();
            Destroy(_obj.gameObject);
            Debug.Log("Enemycount: " + TurnBasedController.instance.Enemies.Count + "           Remove-EnemyLeader line: 73");

        }
        else if (_obj == this.Leader)
        {
            for (int i = 0; i < TurnBasedController.instance.Enemies.Count; i++)
            {
                if (TurnBasedController.instance.Enemies[i] == null)
                {
                    TurnBasedController.instance.Enemies.RemoveAt(i);
                }
            }
            Debug.Log("Enemycount: " + TurnBasedController.instance.Enemies.Count + "           Remove-EnemyLeader- line: 85");
            TurnBasedController.instance.Enemies.Remove(( Enemy )_obj);
            AssignNewLeader();
        }
        else
        {
            TurnBasedController.instance.Enemies.Remove((Enemy)_obj);

            Debug.Log("Enemycount: " + TurnBasedController.instance.Enemies.Count + "           Remove-EnemyLeader- line: 101");

            Destroy(_obj.gameObject);
        }
    }

}
