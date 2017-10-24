using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {

	private Vector3 playerOneStartPos;
	private Vector3 playerTwoStartPos;

	// Use this for initialization
	void Start () {
		playerOneStartPos = GameObject.Find("StartOne").transform.position;
		playerTwoStartPos = GameObject.Find("StartTwo").transform.position;
		AddPlayersToMap();			
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AddPlayersToMap(){
		GameObject playerOne = Instantiate(Services.Prefabs.StealthPlayers[0]) as GameObject;
		playerOne.transform.position = playerOneStartPos;
		// playerOne.transform.eulerAngles = new Vector3 (0, 180, 0);
		GameObject playerTwo = Instantiate(Services.Prefabs.StealthPlayers[0]) as GameObject;
		playerTwo.transform.position = playerTwoStartPos;

	}	
}
