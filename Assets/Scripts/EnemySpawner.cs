using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	public GameObject PlayerShip;
	public EnemySpawnInfo[] enemyTypes;
	
	
    void Start(){
		foreach(EnemySpawnInfo enemy in enemyTypes){
			if(enemy){
				enemy.StartSpawning(transform.position, PlayerShip);
			}
		}
    }
}
