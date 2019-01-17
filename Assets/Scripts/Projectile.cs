using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour{
	[Tooltip("tag the projectile will damage.")]
    public string targetTag;
	[Tooltip("Speed of projectile.")]
    public float velocity = 10;
	[Tooltip("Max lifetime of projectile in seconds.")]
    public float maxTime = 10;
	[Tooltip("damage projectile will deal to a target.")]
    [SerializeField] private float damage = 1;
    
	protected virtual void Start(){
		Destroy(gameObject, maxTime);
    }
    protected virtual void Update(){
        transform.Translate(Vector3.forward * velocity * Time.deltaTime);
    }
	//TakeDamage
	protected virtual void OnTriggerEnter(Collider other){
		if(other.CompareTag(targetTag)){
			Health health = other.transform.root.GetComponent<Health>();
			if(health){
				health.TakeDamage(damage);
			}else{
				Debug.Log("Object hit does not have health component", other.transform.root);
			}
			explode();
		}
	}
	 protected virtual void explode(){
		//play animation
		Destroy(gameObject);
	}
}
