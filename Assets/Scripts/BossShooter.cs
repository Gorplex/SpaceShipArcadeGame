using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShooter : EnemyShooter{
    
    #region PrivateSerializedFields
	#pragma warning disable 0649
	
	[Tooltip("Time between large waves of projectiles.")]
	[SerializeField]
	private float timeBetweenWaves = 5;
    [Tooltip("NUmber of projectiles per wave.")]
	[SerializeField]
	private int waveSize = 200;
    
    
    #pragma warning restore 0649
	#endregion
    
    private float nextWave;
    
    protected override void Start(){
        EventManager.TriggerEvent("KillAllEnemies");
        nextWave = Time.time + timeBetweenWaves;
        base.Start();
    }

    protected override void Update(){
        if(Time.time >= nextWave){
            InitLaunchTimer();
            LaunchCylinder();
            Invoke("LaunchSphereWave",.5f);
            Invoke("LaunchCylinder",1f);
            
            nextWave = Time.time + timeBetweenWaves;
        }
        base.Update();
    }
    void LaunchSphereWave(){
        for(int i=0;i<waveSize;i++){
            Quaternion launchAngle = Quaternion.LookRotation(Random.onUnitSphere, Vector3.up);
            Instantiate (GetProjectile(), transform.position + launchAngle * new Vector3(0,0,launchOffset) , launchAngle);
        }
    }
    void LaunchCylinder(){
        for(int i=0;i<waveSize/5;i++){
            Quaternion randWalk = Quaternion.LookRotation(Random.onUnitSphere, Vector3.up);
            float walkDist = 5;
            Vector3 launchVector = randWalk * transform.rotation * new Vector3(0,0,walkDist) 
                + transform.rotation * new Vector3(0,0,launchOffset);
            Quaternion rot = Quaternion.LookRotation(launchVector, Vector3.up);
            Instantiate (GetProjectile(), transform.position + launchVector, rot);
        }
    }
    
}
