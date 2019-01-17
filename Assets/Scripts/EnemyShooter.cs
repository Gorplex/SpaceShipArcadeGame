using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour{
	[Tooltip("Refence to EnemyProjectile GameObject.")]
	public GameObject projectile;
	[Tooltip("Offset to Shift projectile spanw location out of Enemy.")]
	public float launchOffset = 2f;
	[Tooltip("Frequency(Hz) projectiles are launched.")]
	public float launchFreq = .5f;
	[Tooltip("Delay(s) for first projectile.")]
	public float firstLaunch = 2f;
	
	private float nextLaunch;
	private GameObject player;
	
    // Start is called before the first frame update
    void Start(){
		player = GameObject.Find("PlayerShip");
		transform.LookAt(player.transform);
        nextLaunch = firstLaunch + Time.time;
    }

    // Update is called once per frame
    void Update(){
		LaunchProjectile();
    }
	void LaunchProjectile(){
		transform.LookAt(player.transform);
		if(Time.time >= nextLaunch){
			Instantiate (projectile, transform.position + transform.rotation * new Vector3(0,0,launchOffset) , transform.rotation);
			nextLaunch = Time.time + 1/launchFreq;
		}
	}
}
