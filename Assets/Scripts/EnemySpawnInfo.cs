using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnInfo : MonoBehaviour{
	public GameObject enemy;
	public bool randomRotation = true;
	public  float firstSpawnTime = 3;
	public  float spawnTime = 3;
	public float radiusXZ = 5;
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
		Instantiate(enemy, spawnCenter +  offset, rot);
	}
}
