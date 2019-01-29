using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileHoming : Projectile{
	
	#region PrivateSerializedFields
	#pragma warning disable 0649
	
	[Tooltip("max turn speed.")]
	[SerializeField]
    private float turnSpeed;
    [Tooltip("Player tag string.")]
	[SerializeField]
	private string playerTag = "Player";
    [Tooltip("Disable Player not found error.")]
	[SerializeField]
	private bool ignorePlayerMissing = true;
	
	#pragma warning restore 0649
	#endregion
	
	private GameObject player;
	
    protected override void Start(){
        base.Start();
		LookAtPlayer();
    }
    protected override void Update(){
		base.Update();
        LookAtPlayer();
    }
	void LookAtPlayer(){
		if(player){
			transform.LookAt(player.transform);
		}else{
            if(!(player = GameObject.FindWithTag(playerTag))){
                if(!ignorePlayerMissing)
                    Debug.Log("'player' not found in EnemyProjectileHoming.cs atached to EnemyProjectileHoming GameObject");
            }else{
                transform.LookAt(player.transform);
            }
        }
	}
}
