using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAxisRotator : MonoBehaviour{
	public bool randomRotationAxis = true;
	public float rotationSpeed = 20;
	
	private Vector3 roationAxis;

    void Start(){
		roationAxis = Vector3.forward;
		if(randomRotationAxis){
			roationAxis = Random.rotation.eulerAngles;
			roationAxis.y = 0;
		}
    }

    void Update(){
        transform.RotateAround(transform.position, roationAxis, rotationSpeed * Time.deltaTime);
    }
}
