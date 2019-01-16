using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	[Tooltip("Otherwise centerd on EnemySpawner transform.")]
	public bool centeredOnPlayer = false;
	[Tooltip("Enables EnemySpawner to spawn any Enemies.")]
	public bool enableAll = true;
	[Tooltip("Enables each Enemy individualy. Must be as big as enemyTypes.")]
	public bool[] enable;
	[Tooltip("Refrences to each EnemySpawnInfo script(atached to each Enemy type template). Must be as big as enable.")]
	public EnemySpawnInfo[] enemyTypes;
	
	private GameObject player;
	
    void Start(){
		player = GameObject.Find("PlayerShip");
		if(enableAll){
			IterateSpawnList();
		}
    }
	
	void IterateSpawnList(){
		for(int i=0;i<enable.Length && i<enemyTypes.Length;i++){
		//foreach(EnemySpawnInfo enemy in enemyTypes){
			if(enable[i] && enemyTypes[i]){
				if(centeredOnPlayer){
					enemyTypes[i].StartSpawning(player.transform.position, player);
				}else{
					enemyTypes[i].StartSpawning(transform.position, player);
				}
			}
		}
	}
}
