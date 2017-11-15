using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Characters.FirstPerson;
public class DestroyThis : MonoBehaviour {

	public InputField p1NameInputField;
	public InputField p2NameInputField;
	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Return)){
			if(p1NameInputField.text != p2NameInputField.text && p1NameInputField.text != "" && p2NameInputField.text != ""){
				// CurrentPlayerTracker.currentPlayer.GetComponent<PlayerTimeManager>().UnFreezeMe();
				PlayerNames.AssignNames(p1NameInputField.text, p2NameInputField.text);
							
				// GameObject.Find("CanvasP1").GetComponent<Canvas>().enabled = true;
				// GameObject.Find("CanvasP2").GetComponent<Canvas>().enabled = true;
				Services.MapManager.AddSpawnPointsToArray();
				Services.MapManager.AddPlayersToMap();
				Services.CanvasManager.IdentifyPlayersAndCanvases();
				Services.CanvasManager.AssignCanvasToPlayers();
				Services.CanvasManager.AssignPlayerNumbers();
				Services.CanvasManager.HideCursor();
				Services.CurrentPlayerTracker.AssignPlayers();
				Services.CurrentPlayerTracker.players[0].GetComponent<PlayerHealthManager>().enabled = true;
				Services.CurrentPlayerTracker.players[1].GetComponent<PlayerHealthManager>().enabled = true;
				StartCoroutine(LateDestroy(0.2f));
			}
		}
	}

	IEnumerator LateDestroy(float delay){
		yield return new WaitForSeconds(delay);
 		Destroy(gameObject);
		Destroy(GameObject.Find("P2IntroCanvas"));
	}

	void HideCursor(){
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
}
