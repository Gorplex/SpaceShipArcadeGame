using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAxisRotator : MonoBehaviour{
	
	#region PrivateSerializedFields
	#pragma warning disable 0649
	
	[Tooltip("On spawn Enemy pickes a random rotation axis in XZ plane. Otherwise always rotates on local X(still may have random spawn orentation).")]
	[SerializeField]
	public bool randomRotationAxis = true;
	[Tooltip("Speed of Rotation.")]
	[SerializeField]
	public float rotationSpeed = 20;
	
	#pragma warning restore 0649
	#endregion
	
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
