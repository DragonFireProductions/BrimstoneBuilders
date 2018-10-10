using System.Collections;
using System.Collections.Generic;

using Assets.Meyer.TestScripts;

using Kristal;

using UnityEngine;

public class EnemyLeader : MonoBehaviour {
    

    [SerializeField] List<GameObject> Enemies;

    public List<Enemy> EnemyGroup;

    public Enemy Leader;

    public Color selectedColor = Color.yellow;

    public Color baseColor = Color.grey;

    public Color leaderColor = Color.green;

    

    // Use this for initialization
    void Start () {
        EnemyGroup = new List < Enemy >();
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
            GameObject NewLeader = EnemyGroup[ 0 ].gameObject;

            NewLeader.gameObject.AddComponent < EnemyLeader >( );
            NewLeader.GetComponent < EnemyLeader >( ).Enemies = Enemies;
            TurnBasedController.instance.EnemyLeader = NewLeader.GetComponent < Enemy >( );
        NewLeader.GetComponent < EnemyLeader >( ).baseColor = baseColor;
        NewLeader.GetComponent < EnemyLeader >( ).selectedColor = selectedColor;
            
            Destroy(this.gameObject);
            this.enabled = false;
    }
    void FillOutInfo(List <GameObject> objects ) {
       
    }
    
    public void Remove(Enemy _obj)
    {
        if (EnemyGroup.Count == 0 &&  _obj == this.Leader )
        {
            Enemies.Clear();
            EnemyGroup.Clear();
            TurnBasedController.instance.BattleWon();
            Destroy(_obj.gameObject);
        }
        else if ( _obj == this.Leader ){
            for ( int i = 0 ; i < Enemies.Count ; i++ ){
                if ( Enemies[i] == null){
                    Enemies.RemoveAt(i);
                }
            }
            Enemies.Remove( _obj.gameObject );
            EnemyGroup.Remove( _obj );
            TurnBasedController.instance.Enemies.Remove( _obj );
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
