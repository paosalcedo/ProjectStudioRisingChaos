using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpdateUINameText : MonoBehaviour {

	private GameObject currentPlayer;
	public Text nameText;
	// Use this for initialization
	void Start () {
		currentPlayer = CurrentPlayerTracker.currentPlayer;
	}
	
	// Update is called once per frame
	void Update () {
		currentPlayer = CurrentPlayerTracker.currentPlayer;
		if(currentPlayer.GetComponent<PlayerIdentifier>().myPlayerNum == 0){
			nameText.text = "Your turn is over! Switching to " + currentPlayer.GetComponent<PlayerIdentifier>().myName;
		}

		if(currentPlayer.GetComponent<PlayerIdentifier>().myPlayerNum == 1){
			nameText.text = "Your turn is over! Switching to " + currentPlayer.GetComponent<PlayerIdentifier>().myName;
		}

	}
}
