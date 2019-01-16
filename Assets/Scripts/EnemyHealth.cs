using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour{
	public float startingHealth = 4;
	
	private float health;
	private ScoreKeeper scoreKeeper;
	
    void Start(){
        health = startingHealth;
		scoreKeeper = GameObject.Find("WorldManager").GetComponent<ScoreKeeper>();
    }

    void Update(){
		
    }
	
	void CheckHealth(){
		if(health <= 0){
			//Play Animation
			if(scoreKeeper){
				scoreKeeper.Killed(gameObject);
			}else{
				Debug.Log("ScoreKeeper missing from EnemyHealth", gameObject);
			}
			Destroy(gameObject);
		}
	}
	
	void OnTriggerEnter(Collider other){
		//if(other.gameObject.CompareTag("PlayerProjectile")){
		if(other.CompareTag("PlayerProjectile")){
		//get other components top level transfrom (root) and destroy it
			health -= other.transform.root.gameObject.GetComponent<Projectile>().damage;
			Destroy(other.transform.root.gameObject);
			CheckHealth();
		}
	}
}
