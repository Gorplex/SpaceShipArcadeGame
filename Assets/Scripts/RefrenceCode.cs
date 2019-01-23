using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefrenceCode : MonoBehaviour{
	#region RegionName
	#pragma warning disable 0649
	
	#pragma warning restore 0649
	#endregion
	//standard error template
	//Debug.Log("<Color=Red><b>Missing</b></Color> playerCamera refrence in PlayerNetworkManager.cs atached to Player GameObject");
	
	/*
	[Tooltip("Discription of var.")]
    [SerializeField] 
	private string var;
	*/
	
	/*void createCylinder(){
		GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        cylinder.transform.position = new Vector3(0f, 0f, 26f);
		cylinder.transform.Rotate(90f,0f,0f);
		cylinder.transform.localScale += new Vector3(.25f,20f,.25f);
		cylinder.transform.SetParent(transform);
	}*/
	//other.transform.root.gameObject.CompareTag("PlayerProjectile")
}
