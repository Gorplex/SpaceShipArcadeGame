using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour{
	
	//[Tooltip("Animator refrence.")]
	//[SerializeField] Animator anim;
	[Tooltip("ZSmoothTime.")]
	[SerializeField]private float ZSmoothTime = .5f;
	[Tooltip("XSmoothTime.")]
	[SerializeField]private float XSmoothTime = .5f;
	
	private Quaternion startingRotation;
	private Vector3 oldPos;
	private float[] ZAngles;
	private int ZIndex;
	private float ZAng;
	private float ZAngVel;
	private float[] XAngles;
	private int XIndex;
	private float XAng;
	private float XAngVel;
	
	
	void Start(){
		ZAng = 0f;
		XAng = 0f;
		ZIndex = 1;
		ZAngles = new float[3] {25f,0f,-25f};
		//ZAngles = new float[3] {0f,0f,0f};
		ZAngVel = 0f;
		XIndex = 1;
		XAngles = new float[3] {7f,0f,-7f};
		//XAngles = new float[3] {0f,0f,0f};
		XAngVel = 0f;
		oldPos = transform.parent.position;
	}
    void Update(){
		startingRotation = transform.parent.rotation;
		CheckDif();
		MoveTowardTargets();
		//AnimUpdate();
	}
	int CompareVals(float newA, float oldA){
		if(newA < oldA){
			//roll/pitch left/down
			return 0;
		}else if(newA > oldA){
			//roll/pitch right/up
			return 2;
		}else{
			//roll/pitch center
			return 1;
		}
	}
	void CheckDif(){
		ZIndex = CompareVals(transform.parent.position.x, oldPos.x);
		XIndex = CompareVals(transform.parent.position.y, oldPos.y);
		oldPos = gameObject.transform.position;
	}
	void MoveTowardTargets(){
		ZAng = Mathf.SmoothDamp(ZAng, ZAngles[ZIndex], ref ZAngVel, ZSmoothTime);
		XAng = Mathf.SmoothDamp(XAng, XAngles[XIndex], ref XAngVel, XSmoothTime);
		gameObject.transform.rotation = startingRotation * Quaternion.Euler(XAng, 0, ZAng);
	} 
}
