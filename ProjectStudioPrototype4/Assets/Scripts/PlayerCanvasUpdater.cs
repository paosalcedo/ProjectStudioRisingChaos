using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvasUpdater : MonoBehaviour {

	public Text apText;
	public Text hpText;
	public Text hitAlertText;
	public Text alertText;

	// Use this for initialization

	
	// Update is called once per frame
	public void UpdateHitAlert(string nameOfPlayerHit, int damageDealt){
		hitAlertText.text = "You hit " + nameOfPlayerHit + " for " + damageDealt;
		Debug.Log("You hit " + nameOfPlayerHit + " for " + damageDealt);
		StartCoroutine(ClearText(3f));
	}

	IEnumerator ClearText(float delay){
		yield return new WaitForSeconds(delay);
		hitAlertText.text = "";
	}
}
