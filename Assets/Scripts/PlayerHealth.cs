using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour{
	[Tooltip("If player health is enabled (if player can Die).")]
	public bool enable = true;
	[Tooltip("If player will let projectiles pass through (after taking damage) or remove them.")]
	public bool destroyProjectiles = true;
	[Tooltip("Player starting health.")]
	public float playerStartHealth = 1;
	[Tooltip("Time game is paused when player is hit.")]
	public float timeFrozen = 5;
	[Tooltip("Text object refence for player health.")]
	public Text healthText;

	
	
	private PlayerController playerController;
	private float playerHealth;
	
	void Start(){
		//playerController = gameObject.GetComponent(typeof(PlayerController)) as PlayerController;
		playerController = gameObject.GetComponent<PlayerController>();
		playerHealth = playerStartHealth;
		SetHealthText();
	}
	
	void OnTriggerEnter(Collider other){
		if(enable && other.transform.CompareTag("EnemyProjectile")){
			playerHealth -= other.transform.root.gameObject.GetComponent<Projectile>().damage;
			if(destroyProjectiles){
				Destroy(other.transform.root.gameObject);
			}
			SetHealthText();
		}
	}
	
	void SetHealthText(){
		healthText.text = "Player Health: " + playerHealth.ToString();
	}
	
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
		playerController.enable = false;
		Time.timeScale = 0;
	}
	
	void RestartScene(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
		Time.timeScale = 1;
		playerController.enable = true;
		
	}
}
