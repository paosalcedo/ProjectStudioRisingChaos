using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {
	
	[SerializeField]int numPlayers;

	public GameObject[] players;

	public GameObject[] spawnPoints;
	public Vector3 playerOneStartPos;
	public Vector3 playerTwoStartPos;

	// Use this for initialization
	void Awake (){	
		// AddSpawnPointsToMap();
		// playerOneStartPos = GameObject.Find("StartOne").transform.position;
		// playerTwoStartPos = GameObject.Find("StartTwo").transform.position;
		// AddPlayersToMap();
	}
	void Start () {
		// AddSpawnPointsToArray();
  		// AddPlayersToMap();			
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void AddPlayersToMap(){
		// maybe change this to a for loop in the future
		GameObject playerOne = Instantiate(Services.Prefabs.StealthPlayers[0]) as GameObject;
		playerOne.transform.position = spawnPoints[Random.Range(0, 6)].transform.position;
		// playerOne.transform.eulerAngles = new Vector3 (0, 180, 0);
		GameObject playerTwo = Instantiate(Services.Prefabs.StealthPlayers[0]) as GameObject;
		playerTwo.transform.position = spawnPoints[Random.Range(7,spawnPoints.Length)].transform.position;
		
		playerOne.GetComponent<StealthPlayerSwitcher>().otherPlayer = playerTwo;
		playerTwo.GetComponent<StealthPlayerSwitcher>().otherPlayer = playerOne;
		
		while(Vector3.Distance(playerTwo.transform.position, playerOne.transform.position) <= 2f){
			playerOne.transform.position = spawnPoints[Random.Range(0, 6)].transform.position;
			playerTwo.transform.position = spawnPoints[Random.Range(7,spawnPoints.Length)].transform.position;
		}
 		// for (int i = 0; i < numPlayers; i++){
		// 	Debug.Log("adding player!");
		// 	players[i] = Instantiate(Services.Prefabs.StealthPlayers[0]) as GameObject;
		// 	players[i].GetComponent<PlayerIdentifier>().myPlayerNum = i;
		// 	//pick spawns
		// 	players[i].transform.position = spawnPoints[i].transform.position;
		// }
	}	

	public void AddSpawnPointsToArray(){
		spawnPoints = GameObject.FindGameObjectsWithTag("Respawn");
	}
}
