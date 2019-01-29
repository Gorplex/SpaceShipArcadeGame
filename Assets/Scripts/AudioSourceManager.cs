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
    
	private AudioSource[] audioSources;
    private int audioSourcesIndex = 0;
    
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
        
        GameObject sourceParent = new GameObject("[AudioSources]");
        for(int i=0;i<numAudioSources;i++){
            GameObject obj = new GameObject("AudioSource"+i.ToString());
            obj.transform.parent = sourceParent.transform;
            //obj.SetActive(false);
            audioSources[i] = obj.AddComponent<AudioSource>(); 
        }
    }
    
    public static void PlayOneShot(Transform transform, AudioClip clip, float volumeScale = 1.0f){
        AudioSource curentSource = instance.audioSources[instance.audioSourcesIndex];
        curentSource.gameObject.transform.position = transform.position;
        curentSource.gameObject.transform.rotation = transform.rotation;
        curentSource.PlayOneShot(clip, volumeScale);
        instance.audioSourcesIndex++;
        instance.audioSourcesIndex %= instance.numAudioSources;
    }
    
}
