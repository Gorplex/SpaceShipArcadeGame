using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon;
using Photon.Pun;

public class PlayerHealth : Health{
	
    public delegate void Respawn(float time);
    public event Respawn RespawnMe;
    
    #region PrivateSerializeFields
	#pragma warning disable 0649
    
	[Tooltip("Text object refence for player health.")]
    [SerializeField]
	private Text healthText;
	[Tooltip("Slider object refence for player health.")]
    [SerializeField]
	private Slider healthSlider;
    [Tooltip("playerRespawnTime.")]
    [SerializeField]
	private float playerRespawnTime = 5f;
    
    #pragma warning restore 0649
	#endregion
	
	private PlayerController playerController;
	
	void Start(){
		playerController = gameObject.GetComponent<PlayerController>();
		SetHealthText(getHealth());
        SetHealthSlider(getHealth()); 
	}
	
	protected override void OnDamage(){
		SetHealthText(getHealth());
        SetHealthSlider(getHealth());
	}
	void SetHealthText(float h){
		healthText.text = h.ToString() + "%";
	}

	void SetHealthSlider(float h){
		healthSlider.value = h*.8f; // Scaled for slider
	}
    
    protected override void OnDeath(){
        if(RespawnMe != null)
                RespawnMe(playerRespawnTime);
        PhotonNetwork.Destroy(gameObject);
    }
}
