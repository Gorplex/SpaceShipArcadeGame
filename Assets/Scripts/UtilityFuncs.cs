using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilityFuncs {
	public static void CheckedSetActive(GameObject obj, bool active, string name){
		if(obj){
			obj.SetActive(active);
		}else{
			Debug.Log("<Color=Red><b>Missing</b></Color> " + name + " refrence in Somewhere called by UtilityFuncs");
		}
	}
}
