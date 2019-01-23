using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health{
	[Tooltip("ScoreKeeper script for keeping score(atached to GameManager).")]
	[SerializeField] public ScoreKeeper scoreKeeper;

	protected override void OnDeath(){
		//base.OnDeath();
		if(scoreKeeper){
			scoreKeeper.Killed(gameObject);
		}else{
			Debug.Log("ScoreKeeper missing from EnemyHealth", gameObject);
		}
		Destroy(gameObject);
	}
	protected void OnEnable(){
		EventManager.StartListening("KillAllEnemies", OnDeath);
	}
	protected void OnDisable(){
		EventManager.StopListening("KillAllEnemies", OnDeath);
	}
}
