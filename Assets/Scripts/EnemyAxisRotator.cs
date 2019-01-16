using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAxisRotator : MonoBehaviour{
	public bool randomRotationAxis = true;
	public float rotationSpeed = 1;
	
	private Vector3 roationAxis;
	
    // Start is called before the first frame update
    void Start()
    {
        roationAxis = Random.rotation;
		roationAxis.y = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(Time.deltaTime);
    }
}
