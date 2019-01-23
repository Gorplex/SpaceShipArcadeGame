using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon;
using Photon.Pun;
using Photon.Realtime;

public class PlayerNetworkManager : MonoBehaviourPunCallbacks{
    
	#region PrivateSerializedFields
	#pragma warning disable 0649
	[Tooltip("Refence to player camera.")]
    [SerializeField] 
	private GameObject playerCamera;
	[Tooltip("Tag the projectile will damage.")]
    [SerializeField] 
	private bool playerHitscanEnabled = true;
	[Tooltip("Tag the projectile will damage.")]
    [SerializeField] 
	private bool playerProjectileEnabled = false;
	
	#pragma warning restore 0649
	#endregion
	
	public void Awake(){
		if (photonView.IsMine){
			if(playerCamera){
				playerCamera.SetActive(true);
			}else{
				Debug.Log("<Color=Red><b>Missing</b></Color> playerCamera refrence in PlayerNetworkManager.cs atached to Player GameObject");
			}
			gameObject.GetComponent<PlayerHealth>().enabled = true;
			gameObject.GetComponent<PlayerController>().enabled = true;
			gameObject.GetComponent<PlayerHitscanWeapon>().enabled = playerHitscanEnabled;
			gameObject.GetComponent<PlayerProjectileWeapon>().enabled = playerProjectileEnabled;
		}	
	}
	public void Start(){
        
    }
    public void Update(){
        
    }
}
