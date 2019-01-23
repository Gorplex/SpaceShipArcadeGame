using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnInfo : MonoBehaviour{
	
	//ADD
	//!Physics.CheckSphere(spawnPoint,2);
	
	
	#region PrivateSerializedFields
	#pragma warning disable 0649
	
	[Tooltip("Spwan with at random orentation or same as template.")]
	[SerializeField]
	private bool randomRotation = true;
	[Tooltip("First Spawn time (s) from start of game.")]
	[Range(1,1000)]
	[SerializeField]
	private  float firstSpawnTime = 3;
	[Tooltip("Next Spawn times (s) from prevous spawn times.")]
	[Range(1,1000)]
	[SerializeField]
	private  float spawnTime = 3;
	[Tooltip("Spawn radius in XZ (forms a square).")]
	[Range(1,1000)]
	[SerializeField]
	private float radiusXZ = 5;
	[Tooltip("Spawn radius in Y (forms a square prisim with XZ).")]
	[Range(1,1000)]
	[SerializeField]
	private float radiusY = 10;
	
	#pragma warning restore 0649
	#endregion
	
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
