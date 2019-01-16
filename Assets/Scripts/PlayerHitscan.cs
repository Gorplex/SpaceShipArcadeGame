using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitscan : MonoBehaviour{
	[Tooltip("Weapon fire frequency(Hz).")]
    public float fireFreq = 4f;
	[Tooltip("Damage per shot of player weapon.")]
	public float damagePerShot = 1f;
	[Tooltip("max range of player weapon.")]
	public float maxRange = 100f;
	[Tooltip("Layer number of targets to be hit by player weapon. (9 is Enemy layer")]
	public int targetLayer = 9;	
	
	[Tooltip("Shows debug raycast.")]
	public bool showRaycast = true;
	[Tooltip("enables rendering of Targeting Cylinder.")]
	public bool showTargetingCyl = false;
	
	private float nextShot;
	private int layerMask;
	private GameObject targetingCyl;
	private Renderer targetingCylRend;
	/*
	void createCylinder(){
		GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        cylinder.transform.position = new Vector3(0f, 0f, 26f);
		cylinder.transform.Rotate(90f,0f,0f);
		cylinder.transform.localScale += new Vector3(.25f,20f,.25f);
		cylinder.transform.SetParent(transform);
		
	}*/
    void Start(){
		targetingCyl = transform.Find("TargetingCylinder").gameObject;
		targetingCylRend = targetingCyl.GetComponent<Renderer>();
		targetingCylRend.material.SetColor("_Color",Color.white);
		targetingCyl.SetActive(showTargetingCyl);
		nextShot = Time.time;
		layerMask = 1<<targetLayer;
		
    }
	void RaycastTest(){
		RaycastHit hit;
		if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.magenta);
            //Debug.Log("Did Hit");
			targetingCylRend.material.SetColor("_Color",Color.magenta);
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            //Debug.Log("Did not Hit");
			targetingCylRend.material.SetColor("_Color",Color.white);
        }
	}
    void Update(){
		targetingCyl.SetActive(showTargetingCyl);
        /*if(enable && Input.GetAxis ("Fire1")>0){
			Fireing();
		}*/
		RaycastTest();
    }
	void Fireing(){
		RaycastHit hit;
		if(Time.time >= nextShot){
			if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, maxRange, layerMask)){
				if(showRaycast){
					Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
				}
				//Shoot();
				nextShot = Time.time + 1/fireFreq;
			}
		}
	}
	void Shoot(GameObject target){
		playAnimation();
		
	}
	void playAnimation(){
		
	}
	
	
}
