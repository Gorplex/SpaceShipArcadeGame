using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceManager : MonoBehaviour{
    
    #region PrivateSerializedFields
	#pragma warning disable 0649
    
    [Tooltip("Number of AudioSources to play on GameObjects deaths.")]
    [SerializeField]
    private int numAudioSources = 10;
    
    #pragma warning restore 0649
	#endregion
    
    private GameObject[] gameObjects;
	private AudioSource[] audioSources;
    private int audioSourcesIndex;
    
    private static AudioSourceManager audioSourceManager;

	public static AudioSourceManager instance{
		get{
			if(!audioSourceManager){
				audioSourceManager = FindObjectOfType<AudioSourceManager>();
				if(!audioSourceManager){
					Debug.Log("<Color=Red><b>Missing</b></Color> AudioSourceManager in scene.");
				}else{
					audioSourceManager.CreateAudioSources(); 
				}
			}
			return audioSourceManager;
		}
	}
    
    private void CreateAudioSources(){
        audioSources = new AudioSource[numAudioSources];
        gameObjects = new GameObject[numAudioSources];
        
        GameObject sourceParent = new GameObject("[AudioSources]");
        for(int i=0;i<numAudioSources;i++){
            gameObjects[i] = new GameObject("AudioSource"+i.ToString());
            gameObjects[i].transform.parent = sourceParent.transform;
            //gameObjects[i].SetActive(false);
            audioSources[i] = gameObjects[i].AddComponent<AudioSource>(); 
        }
    }
    
    public static void PlayOneShot(Transform transform, AudioClip clip, float volumeScale = 1.0f){
        GameObject curentObject = instance.gameObjects[instance.audioSourcesIndex];
        curentObject.transform.position = transform.position;
        curentObject.transform.rotation = transform.rotation;
        instance.audioSources[instance.audioSourcesIndex].PlayOneShot(clip, volumeScale);
        instance.audioSourcesIndex++;
        instance.audioSourcesIndex %= instance.numAudioSources;
    }
    
}
