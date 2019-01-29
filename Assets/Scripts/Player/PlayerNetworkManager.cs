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
	[Tooltip("Refence to player camera for enabling.")]
    [SerializeField] 
	private GameObject playerCamera;
	[Tooltip("Refence to player HUDCanvas for enabling.")]
    [SerializeField] 
	private GameObject HUDCanvas;
	/*[Tooltip("Refence to crosshairs for enabling.")]
    [SerializeField] 
	private GameObject crosshairs;*/
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
			//broken??
			//crosshairs = GameObject.Find("OverlayCanvas/CrossHairs");
			//crosshairs = GameObject.FindWithTag("Crosshairs");
			//CheckedSetActive(crosshairs, true, "crosshairs");
			CheckedSetActive(playerCamera, true, "playerCamera");
			CheckedSetActive(HUDCanvas, true, "HUDCanvas");
			gameObject.GetComponent<PlayerHealth>().enabled = true;
			gameObject.GetComponent<PlayerController>().enabled = true;
			gameObject.GetComponent<PlayerHitscanWeapon>().enabled = playerHitscanEnabled;
			gameObject.GetComponent<PlayerProjectileWeapon>().enabled = playerProjectileEnabled;
		}	
	}
	public static void CheckedSetActive(GameObject obj, bool active, string name){
		if(obj){
			obj.SetActive(active);
		}else{
			Debug.Log("<Color=Red><b>Missing</b></Color> " + name + " refrence in PlayerNetworkManager.cs atached to Player GameObject");
		}
	}
	public void Start(){

    }
    public void Update(){
        
    }
}
