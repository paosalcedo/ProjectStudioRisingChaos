using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvasUpdater : MonoBehaviour {

	public Text apText;
	public Text hpText;
	public Text hitAlertText;
	public Text alertText;

	public Text roundScoreText;	
	public Text enemyRoundScoreText;

	public GameObject thisPlayer;

	// Use this for initialization
	void Start(){
		roundScoreText.text = "Your score: " + 0;
		//if this is player 1.
		if(thisPlayer.GetComponent<PlayerIdentifier>().myPlayerNum == 0){
			enemyRoundScoreText.text = PlayerNames.playerOneName + ": " + 0;
		} else { 
			//if this is player 2
			enemyRoundScoreText.text = PlayerNames.playerTwoName + ": " + 0;
		}
	}
	
	void Update(){
		UpdateRoundScore();
	}
	// Update is called once per frame
	public void UpdateHitAlert(string nameOfPlayerHit, int damageDealt){
		hitAlertText.text = "You hit " + nameOfPlayerHit + " for " + damageDealt;
		Debug.Log("You hit " + nameOfPlayerHit + " for " + damageDealt);
		StartCoroutine(ClearText(3f, hitAlertText));
	}

	public void UpdateRoundScore(){
		if(this.gameObject.name == "CanvasP1"){
			// Debug.Log("I  AM P1");
			roundScoreText.text = "Your score: " + Services.ScoreKeeper.p1RoundScore;
			enemyRoundScoreText.text = PlayerNames.playerTwoName + ": " + Services.ScoreKeeper.p2RoundScore;
		}
		if(this.gameObject.name == "CanvasP2"){
			// Debug.Log("I AM P2");
			roundScoreText.text = "Your score: " + Services.ScoreKeeper.p2RoundScore;
			enemyRoundScoreText.text = PlayerNames.playerOneName + ": " + Services.ScoreKeeper.p1RoundScore;
		}
	}

	public void UpdateAlertTextWithDeath(string winningPlayer){
		alertText.text = winningPlayer + " killed you!";
	}
	IEnumerator ClearText(float delay, Text textToClear){
		yield return new WaitForSeconds(delay);
		textToClear.text = "";
	}
}
