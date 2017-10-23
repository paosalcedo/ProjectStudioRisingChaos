using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentPlayerTracker : MonoBehaviour {

	public static GameObject currentPlayer;

	public static void SetCurrentPlayer(GameObject playerToMakeCurrent){
		currentPlayer = playerToMakeCurrent;
		// Debug.Log("Current player set!");
	}
}
