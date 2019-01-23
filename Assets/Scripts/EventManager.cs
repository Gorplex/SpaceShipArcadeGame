using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public sealed class EventManager : MonoBehaviour{
    
	private Dictionary<string, UnityEvent> eventDictionary;
	
	private static EventManager eventManager;
	
	public static EventManager instance{
		get{
			if(!eventManager){
				eventManager = FindObjectOfType<EventManager>();
				if(!eventManager){
					Debug.Log("<Color=Red><b>Missing</b></Color> EventManager in scene.");
				}else{
					eventManager.Init(); 
				}
			}
			//Debug.Log(eventManager);
			//Debug.Log(instance);
			return eventManager;
		}
	}
	
	private void Init(){
		if(null == eventDictionary){
			eventDictionary = new Dictionary<string, UnityEvent>();
		}
	}
	
	public static void StartListening(string eventName, UnityAction listener){
		UnityEvent thisEvent = null;
		if(instance.eventDictionary.TryGetValue(eventName, out thisEvent)){
			thisEvent.AddListener(listener);
		}else{
			thisEvent = new UnityEvent();
			thisEvent.AddListener(listener);
			instance.eventDictionary.Add(eventName, thisEvent);
		}
	}
	
	public static void StopListening(string eventName, UnityAction listener){
		if(!eventManager)
			return;
		
		UnityEvent thisEvent = null;
		if(instance.eventDictionary.TryGetValue(eventName, out thisEvent)){
			thisEvent.RemoveListener(listener);
		}
	}
	public static void TriggerEvent(string eventName){
		UnityEvent thisEvent = null;
		if(instance.eventDictionary.TryGetValue(eventName, out thisEvent)){
			thisEvent.Invoke();
		}
	}
	//for debug
	public static List<string> GetTriggerNames(){
		return new List<string>(instance.eventDictionary.Keys);
	}
	public static string GetTriggerNamesString(){
		return string.Join(", ", GetTriggerNames());
	}
}
