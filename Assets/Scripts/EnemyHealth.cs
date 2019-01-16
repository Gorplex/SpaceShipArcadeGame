using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour{
	public float startingHealth = 5;
	
	private float health;
	
    void Start(){
        health = startingHealth;
    }

    void Update(){
        CheckHealth();
    }
	
	private void CheckHealth(){
		if(health <= 0){
			//Play Animation
			Destroy(gameObject);
		}
	}
	
	void DealDamage(float damage){
		health += damage;
		CheckHealth();
	}
	
	void OnTriggerEnter(Collider other){
		//if(other.gameObject.CompareTag("PlayerProjectile")){
		if(other.transform.root.gameObject.CompareTag("PlayerProjectile")){
		//get other components top level transfrom (root) and destroy it
			health -= other.transform.root.gameObject.GetComponent<Projectile>().damage;
			Destroy(other.transform.root.gameObject);
		}
	}
}
