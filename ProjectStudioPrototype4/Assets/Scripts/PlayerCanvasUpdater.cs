using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvasUpdater : MonoBehaviour {

	public Text apText;
	public Text hpText;
	public Text hitAlertText;
	public Text alertText;

	public Text turnText;
	
	public Text roundScoreText;	
	public Text enemyRoundScoreText;

	public GameObject thisPlayer;
	private PlayerIdentifier thisPlayerID;

	// Use this for initialization
	void Start(){
		thisPlayerID = thisPlayer.GetComponent<PlayerIdentifier>();
		roundScoreText.text = "Your score: " + 0;
		//if this is player 1.
		if(thisPlayerID.myPlayerNum == 0){
			enemyRoundScoreText.text = PlayerNames.playerOneName + ": " + 0;
		} 
		//if this is player 2
		else { 
			enemyRoundScoreText.text = PlayerNames.playerTwoName + ": " + 0;
		}
	}
	
	void Update(){
		UpdateRoundScore();
	}
	// Update is called once per frame
	public void UpdateHitAlert(string nameOfPlayerHit, int damageDealt){
		hitAlertText.text = "You hit " + nameOfPlayerHit + " for " + damageDealt;
 		StartCoroutine(ClearText(3f, hitAlertText));
	}

	public void UpdateGotHitAlert(string nameOfPlayerWhoHitYou, int damageReceived){
		hitAlertText.text = nameOfPlayerWhoHitYou + " for " + damageReceived;
 		StartCoroutine(ClearText(3f, hitAlertText));
	}
	public void UpdateRoundScore(){
		if(thisPlayerID.myPlayerNum == 0){
			// Debug.Log("I  AM P1");
			roundScoreText.text = "Your score: " + Services.ScoreKeeper.p1RoundScore;
			enemyRoundScoreText.text = PlayerNames.playerTwoName + ": " + Services.ScoreKeeper.p2RoundScore;
		}
		else {
			// Debug.Log("I AM P2");
			roundScoreText.text = "Your score: " + Services.ScoreKeeper.p2RoundScore;
			enemyRoundScoreText.text = PlayerNames.playerOneName + ": " + Services.ScoreKeeper.p1RoundScore;
		}
	}

	public void UpdateAlertTextWithDeath(string winningPlayer){
		alertText.text = winningPlayer + " killed you!";
		StartCoroutine(ClearText(3f, alertText));
	}

	public void UpdateAlertTextWithFrag(string losingPlayer){
		alertText.text = "You fragged " + losingPlayer;
		StartCoroutine(ClearText(3f, alertText));
	}
	public void UpdateAlertTextForTrapper(GameObject trappedPlayer){
		alertText.text = trappedPlayer.GetComponent<PlayerIdentifier>().myName + " sprung your trap!";
	}
	public IEnumerator ClearText(float delay, Text textToClear){
		yield return new WaitForSeconds(delay);
		textToClear.text = "";
	}
}
