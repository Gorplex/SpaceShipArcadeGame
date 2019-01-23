using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour{
	
	#region PrivateSerializeFields
	#pragma warning disable 649
	
	[Tooltip("Max health.")]
	[SerializeField]
	private float maxHealth = 1; 
	[Tooltip("starting health.")]
	[SerializeField] 
	private float health = 1; 	
	
	#pragma warning restore 0649
	#endregion
	
	private bool dead = false;
	
	protected virtual void OnDeath(){
		
	}
	protected virtual void OnDamage(){
		
	}
	public void TakeDamage(float damage){
		health-=damage;
		OnDamage();
		if(health <= 0){
			Kill();
		}
	}
	public void Kill(){
		if(!dead){
			dead = true;
			OnDeath();
		}
	}
	public bool isDead(){
		return dead;
	}
	public void SetHealthMax(){
		health = maxHealth;
	}
	public float getHealth(){
		return health;
	}
}
