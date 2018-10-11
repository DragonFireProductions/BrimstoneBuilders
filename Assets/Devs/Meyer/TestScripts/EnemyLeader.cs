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

    public Color selectedColor = Color.yellow;

    public Color baseColor = Color.grey;

    public Color leaderColor = Color.green;

    

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
        GameObject NewLeader = characters[ 0 ].gameObject;

        NewLeader.gameObject.AddComponent < EnemyLeader >( );
        NewLeader.GetComponent < EnemyLeader >( ).characterObjs = characterObjs;
        TurnBasedController.instance.EnemyLeader = NewLeader.GetComponent < Enemy >( );
        NewLeader.GetComponent < EnemyLeader >( ).baseColor = baseColor;
        NewLeader.GetComponent < EnemyLeader >( ).selectedColor = selectedColor;
        
        Destroy(this.gameObject);
        this.enabled = false;
    }
    void FillOutInfo(List <GameObject> objects ) {
       
    }

    public override void Remove(BaseCharacter _obj)
    {
        Debug.Log("Enemycount: " + TurnBasedController.instance.Enemies.Count + "           Remove-EnemyLeader- line: 64");

        if (characters.Count == 0 && _obj == this.Leader)
        {
            characterObjs.Clear();
            characters.Clear();
            TurnBasedController.instance.BattleWon();
            Destroy(_obj.gameObject);
            Debug.Log("Enemycount: " + TurnBasedController.instance.Enemies.Count + "           Remove-EnemyLeader line: 73");

        }
        else if (_obj == this.Leader)
        {
            for (int i = 0; i < characterObjs.Count; i++)
            {
                if (characterObjs[i] == null)
                {
                    characterObjs.RemoveAt(i);
                }
            }
            Debug.Log("Enemycount: " + TurnBasedController.instance.Enemies.Count + "           Remove-EnemyLeader- line: 85");

            characterObjs.Remove(_obj.gameObject);
            characters.Remove(_obj);
            TurnBasedController.instance.Enemies.Remove(( Enemy )_obj);
            AssignNewLeader();
        }
        else
        {
            for (int l_i = 0; l_i < characters.Count; l_i++)
            {
                if ( characters[ l_i ].gameObject == _obj.gameObject ){
                    characters.RemoveAt( l_i );
                    TurnBasedController.instance.Enemies.RemoveAt( l_i );
                }
            }
            Debug.Log("Enemycount: " + TurnBasedController.instance.Enemies.Count + "           Remove-EnemyLeader- line: 101");

            Destroy(_obj.gameObject);
        }
    }

}
