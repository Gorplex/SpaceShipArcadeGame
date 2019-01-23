using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour{
	
	#region PrivateSerializedFields
	#pragma warning disable 649
	
	[Tooltip("Refence to EnemyProjectile GameObject.")]
	[SerializeField]
	private GameObject projectile;
	[Tooltip("Offset to Shift projectile spanw location out of Enemy.")]
	[SerializeField]
	private float launchOffset = 2f;
	[Tooltip("Frequency(Hz) projectiles are launched.")]
	[SerializeField]
	private float launchFreq = .5f;
	[Tooltip("Delay(s) for first projectile.")]
	[SerializeField]
	private float firstLaunch = 2f;
	[Tooltip("playerTag.")]
	[SerializeField]
	private string playerTag = "Player";
	
	#pragma warning restore 0649
	#endregion
	
    #region PrivateFields
	private Animator recoilAnim;
	private float nextLaunch;
	private GameObject player;
	#endregion
	
    // Start is called before the first frame update
    void Start(){
		foreach(GameObject obj in GameObject.FindGameObjectsWithTag(playerTag))
			player = obj;
		LookAtPlayer();
        nextLaunch = firstLaunch + Time.time;
        recoilAnim = gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update(){
		LaunchProjectile();
    }
	void LookAtPlayer(){
		if(player){
			transform.LookAt(player.transform);
		}else{
			Debug.Log("'player' not found in EnemyProjectileHoming.cs atached to EnemyProjectileHoming GameObject");
		}
	}
	void LaunchProjectile(){
		LookAtPlayer();
		if(Time.time >= nextLaunch){
			recoilAnim.Play("Recoil");
            Instantiate (projectile, transform.position + transform.rotation * new Vector3(0,0,launchOffset) , transform.rotation);
			nextLaunch = Time.time + 1/launchFreq;
		}
	}
}
