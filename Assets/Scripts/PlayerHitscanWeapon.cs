using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitscanWeapon : MonoBehaviour{
    #region PrivateSerializedFields
	#pragma warning disable 0649
    
	[Tooltip("Weapon auto fire.")]
    [SerializeField]
    private bool autoFire = false;
	[Tooltip("Weapon fire frequency(Hz).")]
    [SerializeField]
    private float fireFreq = 4f;
	[Tooltip("Damage per shot of player weapon.")]
    [SerializeField]
	private float damagePerShot = 1f;
	[Tooltip("max range of player weapon.")]
    [SerializeField]
	private float maxRange = 100f;
	[Tooltip("Duration of each laser pulse.")]
    [SerializeField]
	private float laserDuration = .1f;
	[Tooltip("Color of Lasor pulses.")]
    [SerializeField]
	private Color laserColor = Color.green;
	[Tooltip("LayerMask of targets (enemies) to atack.")]
	[SerializeField]
    private LayerMask layerMask;
    [Tooltip("Number of Lasers to create (Cylinder GameObjects).")]
    [SerializeField]
	private int numLasers = 6;
    [Tooltip("Refences to laserGunLocations Transfroms.")]
    [SerializeField]
	private Transform[] laserGunLocations;
    [Tooltip("Width of laser beams.")]
    [SerializeField]
	private float beamWidth = .1f;
	[Tooltip("Laser sound effect reference.")]
    [SerializeField]
	private AudioClip[] laserSounds;
    [Tooltip("Explosion sound volume.")]
    [SerializeField]
	private float laserVol = .2f;
    [Tooltip("PlayerCamera GameObject refrence.")]
    [SerializeField]
	private GameObject playerCamera;
    [Tooltip("Player Aiming happens from the player camer not the player ship.")]
	[SerializeField]
    private bool PlayerCameraShooting = true;
	
    #pragma warning restore 0649
	#endregion
    
	
	private float nextShotTime;
	private int laserIndex = 0;
    private int gunIndex = 0;
	private GameObject curLaser;
	private Vector3 lastPos;
	private Quaternion lastRot;
	private Vector3 lastScale;
    private AudioSource audioSource;
	private int soundIndex = 0;
    private Transform raycastStartLocation;
    private GameObject[] lasers;
	private GameObject laserParent;
    
	void SetupLasers(){
        lasers = new GameObject[numLasers];
        laserParent = new GameObject("[Lasers]");
        for(int i=0;i<numLasers;i++){
            lasers[i] = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            lasers[i].transform.parent = laserParent.transform;
            lasers[i].SetActive(false);
            lasers[i].GetComponent<Renderer>().material.SetColor("_Color",laserColor);
        }
	}
    void OnDisable(){
        Destroy(laserParent);
    }
	
    void Awake(){
		SetupLasers();
		nextShotTime = Time.time;
        audioSource = gameObject.GetComponent<AudioSource>();
        if(PlayerCameraShooting){
            this.raycastStartLocation = playerCamera.transform;
        }else{
            this.raycastStartLocation = this.transform;
        }
        if(!this.raycastStartLocation){
            Debug.Log("<Color=Red><b>Missing</b></Color> PlayerCamera GameObject in PlayerHitscanWeapon on Player GameObject");
        }
    }
    void Update(){
        if(Input.GetAxis("Fire1")>0 || autoFire){
			Fireing();
		}
    }
	void Fireing(){
		RaycastHit hit;
		if(Time.time >= nextShotTime){
			if(Physics.Raycast(this.raycastStartLocation.position, this.raycastStartLocation.TransformDirection(Vector3.forward), out hit, maxRange, layerMask)){
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
        PlaySound();
	}
    void PlaySound(){
        if(audioSource && laserSounds.Length > 0){
			audioSource.PlayOneShot(laserSounds[soundIndex], laserVol);
			soundIndex++;
			if (soundIndex >= laserSounds.Length) {
				soundIndex = 0;
			}
		}
    }
	void PlayAnimation(Vector3 target){
		SetLaser(laserGunLocations[gunIndex++], lasers[laserIndex++], target);
        gunIndex %= laserGunLocations.Length;
        laserIndex %= lasers.Length;
	}
	void SetLaser(Transform gun, GameObject laser, Vector3 target){
		laser.transform.localScale = new Vector3(beamWidth, ((target-gun.position)/2).magnitude, beamWidth);
		laser.transform.position = (gun.position + target)/2;
		laser.transform.rotation = Quaternion.LookRotation(target-gun.position);
		laser.transform.Rotate(90f,0f,0f);
		laser.SetActive(true);
        StartCoroutine(ResetLastLaser(laserDuration, laser));
    }
	private IEnumerator ResetLastLaser(float delayTime, GameObject laser){
        yield return new WaitForSeconds(delayTime);
        laser.SetActive(false);
	}
	
	
}
