using System.Collections;
using System.Collections.Generic;

using Kristal;

using UnityEngine;

public class RealTime : MonoBehaviour {

	public bool Attacking { get; set; }
	public Enemy EnemyClicked { get; set; }

	public List < Enemy > Enemies;
	public List < Companion > Companions;

	[SerializeField] public Color ClickedColor { get; set; }

	public void Awake( ) {
		Companions = new List < Companion >();
		Enemies = new List < Enemy >();
	}
	

	public void SetAttackCompanions( ) {
		foreach ( var l_companion in Companions ){
			int random = Random.Range( 0 , Enemies.Count );
			l_companion.enemy = Enemies[ random ];
			Enemies[ random ].enemy = l_companion;
			l_companion.Nav.SetDestination(Enemies[random].transform.position);
			l_companion.Nav.SetState = BaseNav.state.ATTACKING;
			Enemies[random].attackers.Add(l_companion);
			l_companion.attackers.Add(Enemies[random]);
        }
    }

	public void SetAttackEnemies( ) {

        foreach (var l_enemy in Enemies)
        {
            if (l_enemy.enemy == null){
	            int random = Random.Range( 0 , Companions.Count );
	            l_enemy.enemy = Companions[random  ];
                l_enemy.Nav.SetDestination(l_enemy.enemy.transform.position);
				Companions[random].attackers.Add(l_enemy);
            }
	        l_enemy.Nav.SetState = BaseNav.state.ATTACKING;

        }

    }
}
