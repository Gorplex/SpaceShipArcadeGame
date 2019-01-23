using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class NetworkManager : MonoBehaviourPunCallbacks {
public class NetworkManager : MonoBehaviour {
    
	#region PrivateSerializedFields
	#pragma warning disable 649
	
	[Tooltip("The prefab to use for representing the player")]
	[SerializeField]
	private GameObject playerPrefab;
	
	#pragma warning restore 0649
	#endregion
	
	void Start(){
		if(playerPrefab){
			Instantiate(this.playerPrefab, new Vector3(0f,0f,0f), Quaternion.identity);
			//PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f,0f,0f), Quaternion.identity);
		}else{
			Debug.Log("NetworkManager.cs atached to GameManager GameObject missing playerPrefab");
		}
    }
    void Update(){
        
    }
}
