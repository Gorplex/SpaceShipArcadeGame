using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour{
	[Tooltip("Time game is paused when player is hit.")]
	public float timeFrozen = 5;
	
	private PlayerController playerController;
	
	void Start(){
		playerController = gameObject.GetComponent(typeof(PlayerController)) as PlayerController;
	}
	
	void OnTriggerEnter(Collider other){
		if(other.gameObject.CompareTag("EnemyProjectile")){
			StopScene();
			//StartCoroutine(Example());
			//Invoke("RestartScene", timeFrozen);
		}
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
