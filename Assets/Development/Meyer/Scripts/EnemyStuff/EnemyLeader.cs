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
                characters.Add(l_gameObject.GetComponent<Enemy>());
                l_gameObject.GetComponent<Enemy>().leader = this;
            }
        }
    }

    public void AssignNewLeader( ) {
        GameObject NewLeader = TurnBasedController.instance._enemy.characters[ 0 ].gameObject;

        NewLeader.gameObject.AddComponent < EnemyLeader >( );
        NewLeader.gameObject.GetComponent<EnemyLeader>().characterObjs = new List < GameObject >();
        foreach ( var VARIABLE in TurnBasedController.instance._enemy.characters ){
            NewLeader.GetComponent < EnemyLeader >( ).characterObjs.Add(VARIABLE.obj);
        }
        TurnBasedController.instance._enemy.leader = NewLeader.GetComponent < Enemy >( );

        Destroy(this.gameObject);
        this.enabled = false;
    }
    void FillOutInfo(List <GameObject> objects ) {

    }

    public void Remove(BaseCharacter _obj)
    {
        Debug.Log("Enemycount: " + TurnBasedController.instance._enemy.characters.Count + "           Remove-EnemyLeader- line: 64");

        if (TurnBasedController.instance._enemy.characters.Count == 1 && _obj == this.Leader)
        {
            characterObjs.Clear();
            characters.Clear();
            TurnBasedController.instance.BattleWon();
            Destroy(_obj.gameObject);
            Debug.Log("Enemycount: " + TurnBasedController.instance._enemy.characters.Count + "           Remove-EnemyLeader line: 73");

        }
        else if (_obj == this.Leader)
        {
            for (int i = 0; i < TurnBasedController.instance._enemy.characters.Count; i++)
            {
                if (TurnBasedController.instance._enemy.characters[i] == null)
                {
                    TurnBasedController.instance._enemy.characters.RemoveAt(i);
                }
            }
            Debug.Log("Enemycount: " + TurnBasedController.instance._enemy.characters.Count + "           Remove-EnemyLeader- line: 85");
            TurnBasedController.instance._enemy.characters.Remove(( Enemy )_obj);
            AssignNewLeader();
        }
        else
        {
            TurnBasedController.instance._enemy.characters.Remove((Enemy)_obj);

            Debug.Log("Enemycount: " + TurnBasedController.instance._enemy.characters.Count + "           Remove-EnemyLeader- line: 101");

            Destroy(_obj.gameObject);
        }
    }

}
