using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour {

	public int p1RoundScore;
	public int p2RoundScore;
	public int gameScore;
	public int p1GameScore;
	public int p2GameScore;
	// Use this for initialization
	void Start () {
		p1RoundScore = 0;
		p2RoundScore = 0;	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AddToRoundScore(int playerNumWhoScored){
		if(playerNumWhoScored == 0){
			++p1RoundScore;
		}
		if(playerNumWhoScored == 1){
			++p2RoundScore;
		}
	}

	public void AddToGameScore(){
		if(p1RoundScore >= 3){
			++p1GameScore;
		}
		if(p2RoundScore >= 3){
			++p2GameScore;
		}
	}
}
