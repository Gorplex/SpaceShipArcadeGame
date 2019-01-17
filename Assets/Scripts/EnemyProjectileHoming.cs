using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileHoming : Projectile{
	//[Tooltip("Player targeted by homing projectile.")]
    //public GameObject player;
	[Tooltip("max turn speed.")]
    public float turnSpeed;
	
	private GameObject player;
	
    protected override void Start(){
        base.Start();
		player = GameObject.Find("PlayerShip");
    }
    protected override void Update(){
		base.Update();
		transform.LookAt(player.transform);
    }
}
