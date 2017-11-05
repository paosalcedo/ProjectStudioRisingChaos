using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealthManager : MonoBehaviour {

	bool scoreHasBeenIncreased;
	public string myName;
	private GameObject myCanvas;
	public int myIndex;
	public int maxHealth = 100;
	public int currentHealth;

	private PlayerTimeManager timeManager;
	PlayerIdentifier playerIdentifier;
	StealthPlayerSwitcher playerSwitcher;
	// Use this for initialization
	void Start () {
		scoreHasBeenIncreased = false;
		timeManager = GetComponent<PlayerTimeManager>();
		myCanvas = timeManager.myCanvas;
		currentHealth = maxHealth;
		Debug.Log(currentHealth);
		playerIdentifier = GetComponent<PlayerIdentifier>();
 		// myIndex = playerSwitcher.myIndex;
		currentHealth = maxHealth;
		if(playerIdentifier.myPlayerNum == 0){
			//you are player 1
			myName = PlayerNames.playerOneName;
		} 
		if(playerIdentifier.myPlayerNum == 1){
			//you are player 2
			myName = PlayerNames.playerTwoName;
		}
	}
	
	// Update is called once per frame
	void Update(){
		if(currentHealth <= 0){
			AddToEnemyScore();
			myCanvas.GetComponent<PlayerCanvasUpdater>().UpdateRoundScore();
		}
	}
	public void DepleteHealth(int damage){
		currentHealth -= damage;
		Debug.Log("Damaging " + this.gameObject);
		HealthManager.CheckPlayerHealth(this.gameObject, currentHealth);
		UpdateCanvasHealth(currentHealth);
	}

	public void UpdateCanvasHealth(int _health){
		Text hpText = myCanvas.GetComponent<PlayerCanvasUpdater>().hpText;
		hpText.text = "HP: " + _health.ToString("F0") + "/" + maxHealth;
	}



	public void AddToEnemyScore(){
		//if this player is Player 1, then add to the score of Player 2 (otherplayer)
		if(!scoreHasBeenIncreased){
			if(playerIdentifier.myPlayerNum == 0){
				Services.ScoreKeeper.AddToRoundScore(CurrentPlayerTracker.otherPlayer.GetComponent<PlayerIdentifier>().myPlayerNum);
				scoreHasBeenIncreased = true;
			} else {
			//if this player is Player 2, then add to the score of Player 1(currentPlayer)
				Services.ScoreKeeper.AddToRoundScore(CurrentPlayerTracker.currentPlayer.GetComponent<PlayerIdentifier>().myPlayerNum);	
				scoreHasBeenIncreased = true;
			}
		}
	} 



	
}
