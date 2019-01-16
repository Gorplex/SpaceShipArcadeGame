using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorLock : MonoBehaviour{
	CursorLockMode wantedMode;

    void Update(){
		Toggle();
	}
	
	// Apply requested cursor state
    void SetCursorState(){
        Cursor.lockState = wantedMode;
        // Hide cursor when locking
        Cursor.visible = (CursorLockMode.Locked != wantedMode);
    }

    //void OnGUI(){
	void Toggle(){
    /*GUILayout.BeginVertical();
        // Release cursor on escape keypress
        if (Input.GetKeyDown(KeyCode.Escape))
            Cursor.lockState = wantedMode = CursorLockMode.None;
		*/
		//Debug.Log("Checking");
        switch (Cursor.lockState){
            case CursorLockMode.None:
                /*
				//GUILayout.Label("Cursor is normal");
                if (GUILayout.Button("Lock cursor"))
                    wantedMode = CursorLockMode.Locked;
                if (GUILayout.Button("Confine cursor"))
                    wantedMode = CursorLockMode.Confined;
                */
				//Debug.Log("None");
				if (Input.GetKeyDown(KeyCode.Escape))
					wantedMode = CursorLockMode.Confined;
				break;
            case CursorLockMode.Confined:
				/*
                GUILayout.Label("Cursor is confined");
                if (GUILayout.Button("Lock cursor"))
                    wantedMode = CursorLockMode.Locked;
                if (GUILayout.Button("Release cursor"))
                    wantedMode = CursorLockMode.None;
                */
				//Debug.Log("Confined");
				if (Input.GetKeyDown(KeyCode.Escape))
					wantedMode = CursorLockMode.Locked;
				break;
            case CursorLockMode.Locked:
                /*
				GUILayout.Label("Cursor is locked");
                if (GUILayout.Button("Unlock cursor"))
                    wantedMode = CursorLockMode.None;
                if (GUILayout.Button("Confine cursor"))
                    wantedMode = CursorLockMode.Confined;
                */
				//Debug.Log("Locked");
				if (Input.GetKeyDown(KeyCode.Escape))
					Cursor.lockState = wantedMode = CursorLockMode.None;
				break;
        }

        //GUILayout.EndVertical();

        SetCursorState();
    }
}
