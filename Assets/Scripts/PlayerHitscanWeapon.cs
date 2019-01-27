using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitscanWeapon : MonoBehaviour{
	[Tooltip("Weapon auto fire.")]
    public bool autoFire = false;
	[Tooltip("Weapon fire frequency(Hz).")]
    public float fireFreq = 4f;
	[Tooltip("Damage per shot of player weapon.")]
	public float damagePerShot = 1f;
	[Tooltip("max range of player weapon.")]
	public float maxRange = 100f;
	[Tooltip("Duration of each laser pulse.")]
	public float duration = .1f;
	[Tooltip("Color of Lasor pulses.")]
	public Color laserColor = Color.green;
	[Tooltip("LayerMask of enemies to change color on.")]
	[SerializeField] public LayerMask layerMask;
	[Tooltip("Refences to Laser Cyliender GameObjects.")]
	public GameObject[] lasers;
	[Tooltip("Laser sound effect reference.")]
	public AudioClip laserSound0;
	[Tooltip("Sound effect source reference.")]
	public AudioSource audioSource;
	
	private float nextShotTime;
	private int laserIndex = 0;
	private GameObject curLaser;
	private Vector3 lastPos;
	private Quaternion lastRot;
	private Vector3 lastScale;
	
	void SetupLasers(){
		foreach(GameObject laser in lasers){
			laser.SetActive(false);
			laser.GetComponent<Renderer>().material.SetColor("_Color",laserColor);
		}
	}
	
    void Start(){
		SetupLasers();
		nextShotTime = Time.time;
    }
    void Update(){
        if(Input.GetAxis("Fire1")>0 || autoFire){
			Fireing();
		}
    }
	void Fireing(){
		RaycastHit hit;
		if(Time.time >= nextShotTime){
			if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, maxRange, layerMask)){
				if(hit.collider.CompareTag("Enemy")){
					Shoot(hit.collider.transform.root.gameObject);
					nextShotTime = Time.time + 1/fireFreq;
				}
			}
		}
	}
	void Shoot(GameObject target){
		Health health = target.GetComponent<Health>();
		health.TakeDamage(damagePerShot);
		PlayAnimation(target.transform.position);
		audioSource.PlayOneShot(laserSound0, 1f);
	}
	void PlayAnimation(Vector3 target){
		SetLaser(lasers[laserIndex++], target);
		if(laserIndex >= lasers.Length){
			laserIndex = 0;
		}
	}
	void SetLaser(GameObject laser, Vector3 target){
		curLaser = laser;
		lastPos = laser.transform.position;
		lastRot = laser.transform.rotation;
		lastScale = laser.transform.localScale;
		laser.transform.localScale += new Vector3(0f, ((target-lastPos)/2).magnitude, 0f);
		laser.transform.position = (lastPos + target)/2;
		laser.transform.rotation = Quaternion.LookRotation(target-lastPos);
		laser.transform.Rotate(90f,0f,0f);
		laser.SetActive(true);
		Invoke("ResetLastLaser", .1f);
	}
	void ResetLastLaser(){
		curLaser.SetActive(false);
		curLaser.transform.position = lastPos;
		curLaser.transform.rotation = lastRot;
		curLaser.transform.localScale = lastScale;
	}
	
	
}
