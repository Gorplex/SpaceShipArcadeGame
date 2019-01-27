using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour{
	
	#region PrivateSerializedFields
	#pragma warning disable 0649
	
	[Tooltip("Refence to EnemyProjectile GameObject.")]
	[SerializeField]
	private GameObject projectile;
	[Tooltip("Offset to Shift projectile spanw location out of Enemy.")]
	[SerializeField]
	protected float launchOffset = 2f;
	[Tooltip("Frequency(Hz) projectiles are launched.")]
	[SerializeField]
	private float launchFreq = .5f;
	[Tooltip("Delay(s) for first projectile.")]
	[SerializeField]
	private float firstLaunch = 2f;
	[Tooltip("Player tag string.")]
	[SerializeField]
	private string playerTag = "Player";
	
	#pragma warning restore 0649
	#endregion
	
    #region PrivateFields
	private Animator recoilAnim;
	private float nextLaunch;
	private GameObject player;
	#endregion
	
    protected GameObject GetProjectile(){
        return projectile;
    }
    
    protected void InitLaunchTimer(){
        nextLaunch = firstLaunch + Time.time;
    }
    
    protected virtual void Start(){
		LookAtPlayer();
        InitLaunchTimer();
        recoilAnim = gameObject.GetComponentInChildren<Animator>();
    }
    
    protected virtual void Update(){
		LaunchProjectile();
    }
	void LookAtPlayer(){
		if(player){
			transform.LookAt(player.transform);
		}else{
            if(!(player = GameObject.FindWithTag(playerTag))){
                Debug.Log("'player' not found in EnemyShooter.cs atached to many enemy GameObjects");
            }else{
                transform.LookAt(player.transform);
            }
        }
	}
	void LaunchProjectile(){
		LookAtPlayer();
		if(Time.time >= nextLaunch){
			if(recoilAnim)
                recoilAnim.Play("Recoil");
            Instantiate (projectile, transform.position + transform.rotation * new Vector3(0,0,launchOffset) , transform.rotation);
			nextLaunch = Time.time + 1/launchFreq;
		}
	}
}
