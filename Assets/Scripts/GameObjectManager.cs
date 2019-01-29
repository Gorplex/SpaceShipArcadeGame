using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectManager : MonoBehaviour {
    
    #region PrivateSerializedFields
	#pragma warning disable 0649
    
    [Tooltip("Refrence to GameObject prefab.")]
    [SerializeField]
    private GameObject prefab;
    [Tooltip("Number of Objects to manage.")]
    [SerializeField]
    private int numGameObjects = 10;
    [Tooltip("Name of each object created (will be followed by its number).")]
    [SerializeField]
    private string objectName = "Object";
    
    #pragma warning restore 0649
	#endregion
	
    #region PrivateFields
    
	private GameObject[] objects;
    private int objectsIndex = 0;
    
    #endregion
    
    void Awake(){
        Init();
    }

    protected virtual void Init(){
        this.objects = new GameObject[numGameObjects];
        for(int i=0;i<numGameObjects;i++){
            if(prefab){
                this.objects[i] = Instantiate(prefab);
            }else{
                this.objects[i] = new GameObject(objectName+i.ToString());
            }
            this.objects[i].transform.parent = gameObject.transform;
            this.objects[i].SetActive(false);
        }
    }
    protected GameObject GetNext(){
        GameObject g = this.objects[this.objectsIndex++];
        this.objectsIndex %= this.numGameObjects;
        return g;
    }
    public GameObject Spawn(Vector3 pos = new Vector3(), Quaternion rot = new Quaternion()){
        GameObject gameObject = GetNext();
        gameObject.transform.position = pos;
        gameObject.transform.rotation = rot;
        gameObject.SetActive(true);
        return gameObject;
    }
    public void Despawn(GameObject projectile){
        projectile.SetActive(false);
    }
}
