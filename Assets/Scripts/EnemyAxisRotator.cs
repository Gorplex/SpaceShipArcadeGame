using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAxisRotator : MonoBehaviour{
	public bool randomRotationAxis = true;
	public float rotationSpeed = 1;
	
	private Vector3 roationAxis;

    void Start()
    {
        roationAxis = Random.rotation.eulerAngles;
		roationAxis.y = 0;
    }

    void Update()
    {
        transform.RotateAround(transform.position, roationAxis, rotationSpeed * Time.deltaTime);
    }
}
