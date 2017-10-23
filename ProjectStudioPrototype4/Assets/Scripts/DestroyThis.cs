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
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Backspace)){
			CurrentPlayerTracker.currentPlayer.GetComponent<PlayerTimeManager>().UnFreezeMe();
			PlayerNames.AssignNames(p1NameInputField.text, p2NameInputField.text);
			HideCursor();
			StartCoroutine(LateDestroy(0.2f));
		}
	}

	IEnumerator LateDestroy(float delay){
		yield return new WaitForSeconds(delay);
		GameObject.Find("CanvasDuringTurn").GetComponent<Canvas>().enabled = true;
		Destroy(gameObject);
	}

	void HideCursor(){
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
}
