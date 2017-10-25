using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour {

	public string myName;

	public int myIndex;
	public int currentHealth;

	StealthPlayerSwitcher playerSwitcher;
	public int maxHealth = 100;
	// Use this for initialization
	void Start () {
		playerSwitcher = GetComponent<StealthPlayerSwitcher>();
		myIndex = playerSwitcher.myIndex;
		if(myIndex == 0){
			//you are player 2
			myName = PlayerNames.playerTwoName;
		} 
		if(myIndex == 1){
			//you are player 1
			myName = PlayerNames.playerOneName;
		}
		currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	public void DepleteHealth(int damage){
		currentHealth -= damage;
		Debug.Log("Damaging " + this.gameObject);
		HealthManager.CheckPlayerHealth(this.gameObject, currentHealth);
	}

	
}
