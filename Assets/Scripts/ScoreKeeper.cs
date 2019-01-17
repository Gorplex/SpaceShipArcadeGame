using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour{
	[Tooltip("Text UI object to display points.")]
	public Text scoreText;
	[Tooltip("Refrences to each EnemySpawnInfo script(atached to each Enemy type template).")]
	public GameObject[] enemyTypes;
	[Tooltip("Points each enemy type is worth")]
	public int[] points;
	
	
	private int totalKills;
	private int totalPoints;
	private int[] kills;
	
    void Start(){
        totalKills=0;
		totalPoints=0;
		kills = new int[enemyTypes.Length];
		UpdateScore();
    }
    void Update(){
        UpdateScore();
    }
	public void Killed(GameObject enemy){
		int index;
		totalKills++;
		if(0 > (index = GetIndexOfName(enemyTypes, enemy))){
			//Error Case
			Debug.Log("Enemy not found in ScoreKeeper.cs", enemy);
		}else{
			if(index < points.Length){
				totalPoints += points[index];
			}
			//kills[index]++;
			UpdateScore();
		}
	}
	private void UpdateScore(){
		if(scoreText){
			scoreText.text = "";
			scoreText.text += "-Score Info-\nPoints: " + totalPoints.ToString()+"\nKills: " + totalKills.ToString();
			scoreText.text += "\n---KILLS---\n";
			for(int i=0;i<enemyTypes.Length;i++){
				scoreText.text += enemyTypes[i].name;
				scoreText.text += ": ";
				scoreText.text += kills[i].ToString();
				scoreText.text += "\n";
			}
		}
	}
	
	private int GetIndexOfName(GameObject[] list, GameObject target){
		for(int i=0;i<list.Length;i++){
			if(target.name.Contains(list[i].name)){
				return i;
			}
		}
		return -1;
	}
}
