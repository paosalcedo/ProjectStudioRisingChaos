using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour {

	public string myName;

	public int myIndex;
	public int maxHealth = 100;
	public int currentHealth;

	PlayerIdentifier playerIdentifier;
	StealthPlayerSwitcher playerSwitcher;
	// Use this for initialization
	void Start () {
		currentHealth = maxHealth;
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
	public void DepleteHealth(int damage){
		currentHealth -= damage;
		Debug.Log("Damaging " + this.gameObject);
		HealthManager.CheckPlayerHealth(this.gameObject, currentHealth);
	}

	
}
