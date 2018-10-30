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

	public void AddEnemy(Enemy enemy ) {
		Enemies.Add(enemy);
	}
}
