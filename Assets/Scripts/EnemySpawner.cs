using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    
    #region RegionName
    #pragma warning disable 0649
    
    [Tooltip("Otherwise centerd on EnemySpawner transform.")]
    [SerializeField]
    private bool centeredOnPlayer = false;
    [Tooltip("Enables EnemySpawner to spawn any Enemies.")]
    [SerializeField]
    private bool enableAny = true;
    [Tooltip("Enables each Enemy individualy. Must be as big as enemyTypes.")]
    [SerializeField]
    private bool[] enable;
    [Tooltip("Refrences to each EnemySpawnInfo script(atached to each Enemy type template). Must be as big as enable.")]
    [SerializeField]
    private EnemySpawnInfo[] enemyTypes;
        
    #pragma warning restore 0649
    #endregion
    
    private GameObject player;
    private bool startedSpawns = false;
    
    void Update(){
        if(!startedSpawns && enableAny){
            //BAD SOLUTION TO CHECK FOR A PLAYER
            if((player = GameObject.Find("Player(Clone)"))){
                //Debug.Log("Spawning Started");
                IterateSpawnList();
                startedSpawns = true;
            }
        }
        
    }
    
    void IterateSpawnList(){
        for(int i=0;i<enable.Length && i<enemyTypes.Length;i++){
            if(enable[i] && enemyTypes[i]){
                if(centeredOnPlayer){
                    enemyTypes[i].StartSpawning(player.transform.position, player);
                }else{
                    enemyTypes[i].StartSpawning(transform.position, player);
                }
            }
        }
    }
}
