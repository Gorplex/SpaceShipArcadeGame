using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;

public class EnemyHealth : Health{
    
    #region PrivateSerializedFields
	#pragma warning disable 0649
    
	[Tooltip("ScoreKeeper script for keeping score(atached to GameManager).")]
	[SerializeField]
    private ScoreKeeper scoreKeeper;
    [Tooltip("Is this enemy a boss.")]
	[SerializeField]
    private bool isBoss = false;

    #pragma warning restore 0649
	#endregion
    
	protected override void OnDeath(){
		if(scoreKeeper){
			scoreKeeper.Killed(gameObject);
		}else{
			Debug.Log("ScoreKeeper missing from EnemyHealth", gameObject);
		}
		base.OnDeath();
        if(PhotonNetwork.IsMasterClient){
            PhotonNetwork.Destroy(gameObject);
        }
	}
	protected void OnEnable(){
        if(!isBoss)
            EventManager.StartListening("KillAllEnemies", OnDeath);
	}
	protected void OnDisable(){
        if(!isBoss)
            EventManager.StopListening("KillAllEnemies", OnDeath);
	}
}
