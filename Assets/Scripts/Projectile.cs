using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float velocity = 10;
    public int maxTicks = 200;
    
    private int ticks = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ticks++;
        if(ticks >= maxTicks){
			Destroy (gameObject);
		}
        transform.Translate(Vector3.forward * velocity * Time.deltaTime);
    }
}
