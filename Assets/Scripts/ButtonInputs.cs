using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ButtonInputs : MonoBehaviour{
	
	private bool mouseLocked = false;
	private bool paused = false;
	
	//Button Triggers
	public void KillAllEnemies(){
		EventManager.TriggerEvent("KillAllEnemies");
	}
	public void TogglePause(){
		if(paused){
			EventManager.TriggerEvent("Unpause");
		}else{
			EventManager.TriggerEvent("Pause");
		}
	}
	public void Restart(){
		EventManager.TriggerEvent("Restart");
	}
	
	//keyboard inputs
	private void Update(){
		if(Input.GetKeyDown("f")){
            EventManager.TriggerEvent("KillAllEnemies");
        }
		if(Input.GetKeyDown("p")){
            TogglePause();
        }
		if(Input.GetKeyDown("m")){
            if(mouseLocked){
				EventManager.TriggerEvent("UnlockMouse");
			}else{
				EventManager.TriggerEvent("LockMouse");
			}
        }
		if(Input.GetKeyDown("escape")){
			EventManager.TriggerEvent("UnlockMouse");
		}
	}
	
		
	//setup listeners
	private void OnEnable(){
		EventManager.StartListening("UnlockMouse", UnlockMouse);
		EventManager.StartListening("LockMouse", LockMouse);
		EventManager.StartListening("Pause", Pause);
		EventManager.StartListening("Unpause", Unpause);
	}
	private void OnDisable(){
		EventManager.StopListening("UnlockMouse", UnlockMouse);
		EventManager.StopListening("LockMouse", LockMouse);
		EventManager.StopListening("Pause", Pause);
		EventManager.StopListening("Unpause", Unpause);
	}
	//Pause Listeners
	private void Pause(){
		if(!paused){
			paused = true;
			Time.timeScale = 0.0f;
			EventManager.TriggerEvent("UnlockMouse");
			EventManager.TriggerEvent(PlayerController.disable);
		}
	}
	private void Unpause(){
		if(paused){
			paused = false;
			Time.timeScale = 1.0f;
			EventManager.TriggerEvent("LockMouse");
			EventManager.TriggerEvent(PlayerController.enable);
		}
	}
	//MouseLocking listeners
	private void UnlockMouse(){
		if(mouseLocked){
			//Debug.Log("Mouse Unlocked");
			mouseLocked = false;
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}
	private void LockMouse(){
		if(!mouseLocked){
			//Debug.Log("Mouse Locked");
			mouseLocked = true;
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
	}
	
	
}
