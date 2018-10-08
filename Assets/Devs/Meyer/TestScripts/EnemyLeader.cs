using System.Collections;
using System.Collections.Generic;

using Kristal;

using UnityEngine;

public class EnemyLeader : MonoBehaviour {
    

    [SerializeField] List<GameObject> Enemies;

    public List<Enemy> EnemyGroup;

    public Enemy Leader;

    // Use this for initialization
    void Start () {
        
        Leader = this.gameObject.GetComponent < Enemy >( );

        gameObject.GetComponent < Enemy >( ).leader = this;

        foreach ( var l_gameObject in Enemies ){
            EnemyGroup.Add(l_gameObject.GetComponent<Enemy>());
            l_gameObject.GetComponent < Enemy >( ).leader = this;
        }
    }

    public void AssignNewLeader( ) {

        //TODO: This might cause errors
        if ( EnemyGroup.Count > 0 ){
            GameObject NewLeader = EnemyGroup[ 0 ].gameObject;
            EnemyGroup.RemoveAt(0);
            NewLeader.gameObject.AddComponent < EnemyLeader >( );
            NewLeader.GetComponent < EnemyLeader >( ).Enemies = Enemies;
            this.enabled = false;
        }
    }
    void FillOutInfo(List <GameObject> objects ) {
       
    }

    public void Remove(GameObject _obj)
    {
        for (int l_i = 0; l_i < EnemyGroup.Count; l_i++)
        {
            if (EnemyGroup[l_i].gameObject == _obj)
                EnemyGroup.RemoveAt(l_i);
        }
        Destroy(_obj);
    }
    public void Remove(Enemy _obj)
    {
        for (int l_i = 0; l_i < EnemyGroup.Count; l_i++)
        {
            if (EnemyGroup[l_i].gameObject == _obj.gameObject)
                EnemyGroup.RemoveAt(l_i);
        }
        Destroy(_obj.gameObject);
    }
}
