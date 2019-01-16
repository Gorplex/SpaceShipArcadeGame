using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour{
	[Tooltip("Speed of projectile.")]
    public float velocity = 10;
	[Tooltip("Max lifetime of projectile in seconds.")]
    public float maxTime = 20;
    
    private float dieTime;
    
	void Start(){
        dieTime = Time.time + maxTime;
    }
    void Update()
    {
        if(Time.time >= dieTime){
			Destroy(gameObject);
		}
        transform.Translate(Vector3.forward * velocity * Time.deltaTime);
    }
}
