using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;

public class Health : MonoBehaviour, IPunObservable{
	
	#region PrivateSerializeFields
	#pragma warning disable 0649
	
	[Tooltip("Max health.")]
	[SerializeField]
	private float maxHealth = 1; 
	[Tooltip("starting health.")]
	[SerializeField] 
	private float health = 1; 	
	
	#pragma warning restore 0649
	#endregion
	
	private bool dead = false;
	
	protected virtual void OnDeath(){

	}
	protected virtual void OnDamage(){
		
	}
    protected virtual void Update(){
        if(health <= 0){
			Kill();
		}
    }
    
	public void TakeDamage(float damage){
		health-=damage;
		OnDamage();
	}
	public void Kill(){
		if(!dead){
			dead = true;
			OnDeath();
		}
	}
	public bool isDead(){
		return dead;
	}
	public void SetHealthMax(){
		health = maxHealth;
	}
	public float getHealth(){
		return health;
	}
    #region IPunObservable implementation

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // We own this player: send the others our data
            //stream.SendNext(this.IsFiring);
            stream.SendNext(this.health);
        }
        else
        {
            // Network player, receive data
            //this.IsFiring = (bool)stream.ReceiveNext();
            this.health = (float)stream.ReceiveNext();
        }
    }

    #endregion
    
}
