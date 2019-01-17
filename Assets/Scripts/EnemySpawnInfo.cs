using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnInfo : MonoBehaviour{
	[Tooltip("Spwan with at random orentation or same as template.")]
	public bool randomRotation = true;
	[Tooltip("First Spawn time (s) from start of game.")]
	public  float firstSpawnTime = 3;
	[Tooltip("Next Spawn times (s) from prevous spawn times.")]
	public  float spawnTime = 3;
	[Tooltip("Spawn radius in XZ (forms a square).")]
	public float radiusXZ = 5;
	[Tooltip("Spawn radius in Y (forms a square prisim with XZ).")]
	public float radiusY = 10;
	
	private Vector3 spawnCenter;
	
	public void StartSpawning(Vector3 spawnCenterLocation, GameObject PlayerShip){
		spawnCenter = spawnCenterLocation;
		InvokeRepeating("Spawn", firstSpawnTime, spawnTime);
	}
	private void Spawn(){
		Vector3 offset = new Vector3(Random.Range(-radiusXZ,radiusXZ),Random.Range(-radiusY,radiusY),Random.Range(-radiusXZ,radiusXZ));
		Quaternion rot = Quaternion.identity;
		if(randomRotation){
			rot = Random.rotation;
		}
		Instantiate(gameObject, spawnCenter +  offset, rot);
	}
}
