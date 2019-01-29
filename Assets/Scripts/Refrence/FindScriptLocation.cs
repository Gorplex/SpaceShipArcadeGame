using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindScriptLocation : MonoBehaviour{
    
    /* broken
    void Start(){
        GameObject[] found = FindObjectsOfType(typeof(EventManager)) as GameObject[];
        if(found.Length > 0){
            foreach(GameObject g in found){
                Debug.Log("Atached to" + g.ToString());
            }
        }else{
            Debug.Log("None Found");
        }
    }
    */
    void Start(){
        EventManager[] found = FindObjectsOfType<EventManager>();
        foreach(EventManager g in found){
            Debug.Log("Atached to" + g.ToString());
        }
        if(found.Length == 0){
            Debug.Log("None Found");
        }
    }
}
