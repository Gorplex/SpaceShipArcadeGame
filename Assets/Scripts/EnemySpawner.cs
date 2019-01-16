using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	public bool[] enable;
	public EnemySpawnInfo[] enemyTypes;
	
	private GameObject player;
	
    void Start(){
		player = GameObject.Find("PlayerShip");
		for(int i=0;i < enable.Length;i++){
		//foreach(EnemySpawnInfo enemy in enemyTypes){
			if(enable[i] && enemyTypes[i]){
				enemyTypes[i].StartSpawning(transform.position, player);
			}
		}
    }
}
