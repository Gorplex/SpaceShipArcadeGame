using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour{
	[Tooltip("Speed of projectile.")]
    public float velocity = 10;
	[Tooltip("Max lifetime of projectile in seconds.")]
    public float maxTime = 10;
	[Tooltip("damage projectile will deal to a target.")]
    public float damage = 1;
    
	void Start(){
		Destroy(gameObject, maxTime);
    }
    void Update()
    {
        transform.Translate(Vector3.forward * velocity * Time.deltaTime);
    }
}
