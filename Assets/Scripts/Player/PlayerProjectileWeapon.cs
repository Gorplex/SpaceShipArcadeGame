using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileWeapon : MonoBehaviour{
    [Tooltip("Player projectile GameObject refrence.")]
	public GameObject projectile;
	[Tooltip("Player projectile fire rate.")]
	[SerializeField] public float projectileFreq = 1;
	[Tooltip("Player projectile fire rate.")]
	[SerializeField] public bool autoFire = false;
	
	private float nextProjectile;
	
	void Start(){
		nextProjectile = Time.time;
    }
    void Update(){
        Shoot();
    }
	void Shoot(){
		if (Time.time >= nextProjectile) {
			if (Input.GetAxis ("Fire1") == 1 || autoFire) {
				Instantiate (projectile, transform.position, transform.rotation);
				nextProjectile = Time.time + 1/projectileFreq;
			}
		}
	}
}
