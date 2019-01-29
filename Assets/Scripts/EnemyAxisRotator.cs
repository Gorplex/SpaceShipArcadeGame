using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAxisRotator : MonoBehaviour{
	
    //NOTE DAMAGE TICKS SHARE A COOLDOWN WITH MULITPLE PLAYERS (randomly maybe)    
    
	#region PrivateSerializedFields
	#pragma warning disable 0649
	
	[Tooltip("On spawn Enemy pickes a random rotation axis in XZ plane. Otherwise always rotates on local X(still may have random spawn orentation).")]
	[SerializeField]
	public bool randomRotationAxis = true;
	[Tooltip("Speed of Rotation.")]
	[SerializeField]
	public float rotationSpeed = 20;
    [Tooltip("Tag the projectile will damage.")]
    [SerializeField] 
	private string targetTag = "Player";
    [Tooltip("Damage per second.")]
	[SerializeField]
	public float dps = 20;
    [Tooltip("Frequency of damage ticks.")]
	[SerializeField]
	public float dmgFreq = 10;
	
	#pragma warning restore 0649
	#endregion
	
	private Vector3 roationAxis;
    private float nextDamageTick;

    void Start(){
        nextDamageTick = Time.time;
		roationAxis = Vector3.forward;
		if(randomRotationAxis){
			roationAxis = Random.rotation.eulerAngles;
			roationAxis.y = 0;
		}
    }

    void Update(){
        transform.RotateAround(transform.position, roationAxis, rotationSpeed * Time.deltaTime);
    }
    protected virtual void OnTriggerStay(Collider other){
        if(other.CompareTag(targetTag)){
            if(Time.time >= nextDamageTick){
                Health health = other.transform.root.GetComponent<Health>();
                if(health){
                    health.TakeDamage(dps/dmgFreq);
                }else{
                    Debug.Log("Object hit does not have health component", other.transform.root);
                }
                nextDamageTick = Time.time + 1/dmgFreq;
            }
        }
    }
}
