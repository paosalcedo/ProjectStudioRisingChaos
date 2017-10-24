using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpdateHitAlertText : MonoBehaviour {

	public enum HitStatus{
		HIT,
		NO_HIT
	}

	private static string targetHitName;
	private HitStatus hitStatus;
	private Text hitAlert; 
	// Use this for initialization
	void Start () {
		hitStatus = HitStatus.NO_HIT;
		hitAlert = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		switch(hitStatus){
			case HitStatus.HIT:
			hitAlert.text = "You hit " + targetHitName;
			break;
			case HitStatus.NO_HIT:
			hitAlert.text = "";
			break;
			default:
			break;
		}
	}

	public void ShowHitAlert(){
		if(CurrentPlayerTracker.currentPlayer.GetComponent<StealthPlayerSwitcher>().myIndex == 0){
			targetHitName = PlayerNames.playerOneName;
		}
		else if(CurrentPlayerTracker.currentPlayer.GetComponent<StealthPlayerSwitcher>().myIndex == 1){
			targetHitName = PlayerNames.playerTwoName;
		}
		hitStatus = HitStatus.HIT;
		StartCoroutine(HideHitAlert(3f));
	}

	IEnumerator HideHitAlert(float delay){
		yield return new WaitForSeconds(delay);
		hitStatus = HitStatus.NO_HIT;
	}

	public static void GetPlayerNamesForHitAlert(string targetHit){
		if(CurrentPlayerTracker.currentPlayer.GetComponent<StealthPlayerSwitcher>().myIndex == 0){
			targetHitName = PlayerNames.playerOneName;
		}
		else if(CurrentPlayerTracker.currentPlayer.GetComponent<StealthPlayerSwitcher>().myIndex == 1){
			targetHitName = PlayerNames.playerTwoName;
		}
	}

}
