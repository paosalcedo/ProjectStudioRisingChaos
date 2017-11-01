using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealthManager : MonoBehaviour {

	
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

	
}
