using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpdateHealthUIText : MonoBehaviour {

	Text hpText;
	// Use this for initialization
	void Start () {
		hpText = GetComponent<Text>();	
	}
	
	// Update is called once per frame
	void Update () {
		UpdateHP();
	}

	public void UpdateHP(){
		hpText.text = "HP: " + CurrentPlayerTracker.currentPlayer.GetComponent<PlayerHealthManager>().currentHealth.ToString() + "/100";
	}
}
