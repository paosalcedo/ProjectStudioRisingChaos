using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealthManager : MonoBehaviour {

	bool scoreHasBeenIncreased;
	public string myName;
	private GameObject myCanvas;
	public GameObject myEnemy;
	public int myIndex;
	public int myScore;
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
		AssignPlayerAndEnemy();
		// if(playerIdentifier.myPlayerNum == 0){
		// 	//you are player 1
		// 	myName = PlayerNames.playerOneName;
		// 	myEnemy = CurrentPlayerTracker.otherPlayer;
		// } 
		// if(playerIdentifier.myPlayerNum == 1){
		// 	//you are player 2
		// 	myName = PlayerNames.playerTwoName;
		// 	myEnemy = CurrentPlayerTracker.currentPlayer;
		// }
	}
	
	// Update is called once per frame
	void Update(){
		if(currentHealth <= 0 && !scoreHasBeenIncreased){
			AddToEnemyScore();
			// Debug.Log("My enemy's health is: " + myEnemy.GetComponent<PlayerHealthManager>().currentHealth);			
		}
		Debug.Log("My enemy's score is: " + myEnemy.GetComponent<PlayerHealthManager>().myScore);
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
		myCanvas.GetComponent<PlayerCanvasUpdater>().UpdateRoundScore();
		myEnemy.GetComponent<PlayerHealthManager>().currentHealth = 100;
		myEnemy.GetComponent<PlayerHealthManager>().myScore += 1;
		if(playerIdentifier.myPlayerNum == 0){
			Services.ScoreKeeper.p2RoundScore = myEnemy.GetComponent<PlayerHealthManager>().myScore;
			//update my canvas.
			myCanvas.GetComponent<PlayerCanvasUpdater>().UpdateRoundScore();
			//update enemy's canvas.
			myEnemy.GetComponent<PlayerHealthManager>().myCanvas.GetComponent<PlayerCanvasUpdater>().UpdateRoundScore();
			// myCanvas.GetComponent<PlayerCanvasUpdater>().UpdateRoundScore();
			scoreHasBeenIncreased = true;
			RespawnPlayerAfterDeath();
		} else if(playerIdentifier.myPlayerNum == 1){
			Services.ScoreKeeper.p1RoundScore = myEnemy.GetComponent<PlayerHealthManager>().myScore;
			//update my canvas.
			myCanvas.GetComponent<PlayerCanvasUpdater>().UpdateRoundScore();
			//update enemy's canvas.
			myEnemy.GetComponent<PlayerHealthManager>().myCanvas.GetComponent<PlayerCanvasUpdater>().UpdateRoundScore();
			scoreHasBeenIncreased = false;
			RespawnPlayerAfterDeath();
		}
	} 

	public void AssignPlayerAndEnemy(){
		if(playerIdentifier.myPlayerNum == 0){
			//you are player 1
			myName = PlayerNames.playerOneName;
			myEnemy = CurrentPlayerTracker.otherPlayer;
		} 
		if(playerIdentifier.myPlayerNum == 1){
			//you are player 2
			myName = PlayerNames.playerTwoName;
			myEnemy = CurrentPlayerTracker.currentPlayer;
		}
	}

	public void RespawnPlayerAfterDeath(){
		if(playerIdentifier.myPlayerNum == 0){
			transform.position = Services.MapManager.spawnPoints[Random.Range(0,2)].transform.position;
			currentHealth = maxHealth;
			UpdateCanvasHealth(currentHealth);
		} else if (playerIdentifier.myPlayerNum == 1){
			// transform.position = Services.MapManager.playerTwoStartPos;			
			transform.position = Services.MapManager.spawnPoints[Random.Range(0,2)].transform.position;
			currentHealth = maxHealth;
			UpdateCanvasHealth(currentHealth);
		}
	}



	
}
