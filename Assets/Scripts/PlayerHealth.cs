using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : Health{
	[Tooltip("Time game is paused when player is hit.")]
	public float timeFrozen = 5;
	[Tooltip("Text object refence for player health.")]
	public Text healthText;
	[Tooltip("Slider object refence for player health.")]
	public Slider healthSlider;
	
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
	
	
	//Broken stuff
	void EndGame(){
		StopScene();
		//StartCoroutine(Example());
		//Invoke("RestartScene", timeFrozen);
	}
	
	IEnumerator Example(){
        StopScene();
        yield return new WaitForSecondsRealtime(5);
        RestartScene();
    }
	
	void StopScene(){
		playerController.enabled = false;
		Time.timeScale = 0;
	}
	
	void RestartScene(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
		Time.timeScale = 1;
		playerController.enabled = true;
		
	}
}
