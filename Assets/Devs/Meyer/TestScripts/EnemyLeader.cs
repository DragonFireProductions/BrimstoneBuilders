using System.Collections;
using System.Collections.Generic;

using Assets.Meyer.TestScripts;

using Kristal;

using UnityEngine;

public class EnemyLeader : MonoBehaviour {
    

    [SerializeField] List<GameObject> Enemies;

    public List<Enemy> EnemyGroup;

    public Enemy Leader;

    // Use this for initialization
    void Start () {
        
        Leader = this.gameObject.GetComponent < Enemy >( );

        EnemyGroup = new List < Enemy >();

        gameObject.GetComponent < Enemy >( ).leader = this;

        if ( Enemies.Count != 0 ){
            foreach (var l_gameObject in Enemies)
            {
                EnemyGroup.Add(l_gameObject.GetComponent<Enemy>());
                l_gameObject.GetComponent<Enemy>().leader = this;
            }
        }
        
    }

    public void AssignNewLeader( ) {

        //TODO: This might cause errors
        if ( EnemyGroup.Count > 0 ){
            GameObject NewLeader = EnemyGroup[ 0 ].gameObject;

            for ( int i = 0 ; i < EnemyGroup.Count ; i++ ){
                if ( EnemyGroup[i] == Leader ){
                    Enemies.RemoveAt(i);
                }

                if ( EnemyGroup[i] == NewLeader ){
                    Enemies.RemoveAt(i);
                }
            }
            Enemies.Add(NewLeader);
            NewLeader.gameObject.AddComponent < EnemyLeader >( );
            NewLeader.GetComponent < EnemyLeader >( ).Enemies = Enemies;
            TurnBasedController.instance.EnemyLeader = NewLeader.GetComponent<Enemy>();
            
            Destroy(this.gameObject);
            this.enabled = false;
        }
    }
    void FillOutInfo(List <GameObject> objects ) {
       
    }
    
    public void Remove(Enemy _obj)
    {
        if (EnemyGroup.Count == 0 &&  _obj == this.Leader )
        {
            TurnBasedController.instance.BattleWon();
            for (int l_i = 0; l_i < EnemyGroup.Count; l_i++)
            {
                if (EnemyGroup[l_i].gameObject == _obj.gameObject)
                    EnemyGroup.RemoveAt(l_i);
            }

            Destroy(_obj.gameObject);
        }
        else if ( _obj == this.Leader ){
            AssignNewLeader();
        }
        else{
            for (int l_i = 0; l_i < EnemyGroup.Count; l_i++)
            {
                if (EnemyGroup[l_i].gameObject == _obj.gameObject)
                    EnemyGroup.RemoveAt(l_i);
            }
            
            Destroy(_obj.gameObject);
        }
        
    }

}
