using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ButtonInputs : MonoBehaviour{
	
	private bool mouseLocked = false;
	
	public void KillAllEnemies(){
		EventManager.TriggerEvent("KillAllEnemies");
	}
	public void Pause(){
		EventManager.TriggerEvent("Pause");
	}
	public void Restart(){
		EventManager.TriggerEvent("Restart");
	}
	//Mouse Locking Triggers
	public void Update(){
		if(Input.GetKeyDown("f")){
            EventManager.TriggerEvent("KillAllEnemies");
        }
		if(Input.GetKeyDown("m") || Input.GetKeyDown("escape")){
            if(mouseLocked){
				EventManager.TriggerEvent("UnlockMouse");
			}else{
				EventManager.TriggerEvent("LockMouse");
			}
        }
	}
	
	
	//MouseLocking listeners
	public void OnEnable(){
		EventManager.StartListening("UnlockMouse", UnlockMouse);
		EventManager.StartListening("LockMouse", LockMouse);
	}
	public void OnDisable(){
		EventManager.StopListening("UnlockMouse", UnlockMouse);
		EventManager.StopListening("LockMouse", LockMouse);
	}
	private void UnlockMouse(){
		if(mouseLocked){
			Debug.Log("Mouse Unlocked");
			mouseLocked = false;
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}
	private void LockMouse(){
		if(!mouseLocked){
			Debug.Log("Mouse Locked");
			mouseLocked = true;
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
	}
	
	
}
