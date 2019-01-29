using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour{
	
	#region PrivateSerializedFields
	#pragma warning disable 0649
	
	[Tooltip("Tag the projectile will damage.")]
    [SerializeField] 
	private string targetTag;
	[Tooltip("Speed of projectile.")]
    [SerializeField] 
	private float velocity = 10;
	[Tooltip("Max lifetime of projectile in seconds.")]
    [SerializeField] 
	private float maxTime = 10;
	[Tooltip("Damage projectile will deal to a target.")]
    [SerializeField] 
	private float damage = 1;
    
	#pragma warning restore 0649
	#endregion
	
	protected virtual void Start(){
		Invoke("EndOfLife", maxTime);
    }
    protected virtual void Update(){
        transform.Translate(Vector3.forward * velocity * Time.deltaTime);
    }
	public string GetTargetTag(){
		return targetTag;
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
			Explode();
		}
	}
    protected virtual void EndOfLife(){
        gameObject.SetActive(false);
    }
	public virtual void Explode(){
		//play animation
		//old way
        //Destroy(gameObject);
        gameObject.SetActive(false);
	}
}
