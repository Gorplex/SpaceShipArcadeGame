using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAxisRotator : MonoBehaviour{
	[Tooltip("On spawn Enemy pickes a random rotation axis in XZ plane. Otherwise always rotates on local X(still may have random spawn orentation).")]
	public bool randomRotationAxis = true;
	[Tooltip("Speed of Rotation.")]
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
