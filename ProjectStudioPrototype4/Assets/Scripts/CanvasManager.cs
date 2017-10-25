using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour {

	public GameObject[] players;
	
// Use this for initialization
	void Awake(){
		// players = GameObject.FindGameObjectsWithTag("Player");
	}
	void Start () {
		StartCoroutine(IdentifyPlayers(0.001f));
		StartCoroutine(AssignPlayerNumbers(0.002f));
	}
	
	// Update is called once per frame
	void Update () {
	}

	IEnumerator IdentifyPlayers(float delay){
		yield return new WaitForSeconds(delay);
		players = GameObject.FindGameObjectsWithTag("Player");
	}

	IEnumerator AssignPlayerNumbers(float delay){
		yield return new WaitForSeconds(delay);
		for (int i = 0; i < players.Length-1; i++)
		{
			players[i].GetComponent<PlayerIdentifier>().myPlayerNum = i;
			if(players[i].GetComponent<PlayerIdentifier>().myPlayerNum == players[i+1].GetComponent<PlayerIdentifier>().myPlayerNum){
				players[i+1].GetComponent<PlayerIdentifier>().myPlayerNum = i+1;
			}	
		}
	}
}
