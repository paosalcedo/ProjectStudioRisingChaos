using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpdateUINameText : MonoBehaviour {

	public Text nameText;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(CurrentPlayerTracker.currentPlayer.GetComponent<StealthPlayerSwitcher>().myIndex == 0){
			nameText.text = "Your turn is over! Switching to " + PlayerNames.playerOneName;
		}

		if(CurrentPlayerTracker.currentPlayer.GetComponent<StealthPlayerSwitcher>().myIndex == 1){
			nameText.text = "Your turn is over! Switching to " + PlayerNames.playerTwoName;
		}
	}
}
