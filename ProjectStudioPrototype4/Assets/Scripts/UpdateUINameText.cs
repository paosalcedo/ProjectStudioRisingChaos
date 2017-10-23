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
		nameText.text = "Your turn is over! Switching to " + PlayerNames.playerOneName;
	}
}
