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
        
    }
	
	void OnTriggerEnter(Collider other){
		//if(other)
	}
}
