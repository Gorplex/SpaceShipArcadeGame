using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileHoming : Projectile{
	
	#region PrivateSerializedFields
	#pragma warning disable 649
	
	[Tooltip("max turn speed.")]
	[SerializeField]
    private float turnSpeed;
	
	#pragma warning restore 0649
	#endregion
	
	private GameObject player;
	
    protected override void Start(){
        base.Start();
		foreach(GameObject obj in GameObject.FindGameObjectsWithTag(this.getTargetTag()))
			player = obj;
		LookAtPlayer();
    }
    protected override void Update(){
		base.Update();
		if(player){
			transform.LookAt(player.transform);
		}else{
			Debug.Log("'player' not found in EnemyProjectileHoming.cs atached to EnemyProjectileHoming GameObject");
		}
    }
	void LookAtPlayer(){
		if(player){
			transform.LookAt(player.transform);
		}else{
			Debug.Log("'player' not found in EnemyProjectileHoming.cs atached to EnemyProjectileHoming GameObject");
		}
	}
}
