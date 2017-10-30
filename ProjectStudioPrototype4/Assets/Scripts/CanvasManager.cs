using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour {

	public GameObject[] players;
	public GameObject[] canvases;
	
// Use this for initialization
	void Awake(){
		// players = GameObject.FindGameObjectsWithTag("Player");
	}
	void Start () {
		IdentifyPlayersAndCanvases();
		AssignCanvasToPlayers();
		AssignPlayerNumbers();
		// StartCoroutine(IdentifyPlayersAndCanvases(0.001f));
		// StartCoroutine(AssignPlayerNumbers(0.002f));
		// StartCoroutine(AssignCanvasToPlayers(0.003f));
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void IdentifyPlayersAndCanvases(){
 		players = GameObject.FindGameObjectsWithTag("Player");
		canvases = GameObject.FindGameObjectsWithTag("Canvas");
		GameObject tempCanvas1 = GameObject.Find("CanvasP1");
		GameObject tempCanvas2 = GameObject.Find("CanvasP2");
		canvases[0] = tempCanvas1;
		canvases[1] = tempCanvas2;
	}
	public void AssignPlayerNumbers(){
		for (int i = 0; i < players.Length-1; i++)
		{
			players[i].GetComponent<PlayerIdentifier>().myPlayerNum = i;
			if(players[i].GetComponent<PlayerIdentifier>().myPlayerNum == players[i+1].GetComponent<PlayerIdentifier>().myPlayerNum){
				players[i+1].GetComponent<PlayerIdentifier>().myPlayerNum = i+1;
			}	
		}
	}
	public void AssignCanvasToPlayers(){
		players[0].GetComponent<PlayerTimeManager>().myCanvas = canvases[0];
		Debug.Log("Canvas assigned to players!");
		players[1].GetComponent<PlayerTimeManager>().myCanvas = canvases[1];
		// players[0].GetComponentInChildren<LaserControl>().myCanvas = canvases[0];
		// players[1].GetComponentInChildren<LaserControl>().myCanvas = canvases[1];
	}

	// IEnumerator IdentifyPlayersAndCanvases(float delay){
	// 	yield return new WaitForSeconds(delay);

	// 	players = GameObject.FindGameObjectsWithTag("Player");
	// 	canvases = GameObject.FindGameObjectsWithTag("Canvas");
	// 	GameObject tempCanvas1 = GameObject.Find("CanvasP1");
	// 	GameObject tempCanvas2 = GameObject.Find("CanvasP2");
	// 	canvases[0] = tempCanvas1;
	// 	canvases[1] = tempCanvas2;
	// }



	// IEnumerator AssignPlayerNumbers(float delay){
	// 	yield return new WaitForSeconds(delay);
	// 	for (int i = 0; i < players.Length-1; i++)
	// 	{
	// 		players[i].GetComponent<PlayerIdentifier>().myPlayerNum = i;
	// 		if(players[i].GetComponent<PlayerIdentifier>().myPlayerNum == players[i+1].GetComponent<PlayerIdentifier>().myPlayerNum){
	// 			players[i+1].GetComponent<PlayerIdentifier>().myPlayerNum = i+1;
	// 		}	
	// 	}
	// }

	// IEnumerator AssignCanvasToPlayers(float delay){
	// 	yield return new WaitForSeconds(delay);
	// 	players[0].GetComponent<PlayerTimeManager>().myCanvas = canvases[0];
	// 	players[1].GetComponent<PlayerTimeManager>().myCanvas = canvases[1];
	// 	// players[0].GetComponentInChildren<LaserControl>().myCanvas = canvases[0];
	// 	// players[1].GetComponentInChildren<LaserControl>().myCanvas = canvases[1];
	// }
}
