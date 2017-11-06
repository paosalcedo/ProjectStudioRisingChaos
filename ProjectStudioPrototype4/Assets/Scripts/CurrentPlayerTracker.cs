using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentPlayerTracker : MonoBehaviour {

	public GameObject[] players;
	public static GameObject currentPlayer;

	public static GameObject otherPlayer;
	public static void SetCurrentPlayer(GameObject playerToMakeCurrent){
		currentPlayer = playerToMakeCurrent;
		// Debug.Log("Current player set!");
	}

	public void AssignPlayers(){
 		players = GameObject.FindGameObjectsWithTag("Player");
		currentPlayer = players[0];
		otherPlayer = players[1];
		// currentPlayer.GetComponent<PlayerHealthManager>().enabled = true;
		// otherPlayer.GetComponent<PlayerHealthManager>().enabled = true;
	}

}
