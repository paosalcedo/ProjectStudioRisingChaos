using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateActionPointsUIText : MonoBehaviour {

	public Text alertText;
	public Text apText;
	// Use this for initialization
	void Start () {
		apText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateAPCount();
		ShowAlertAP();
	}

	public void UpdateAPCount(){
		apText.text = "AP: " + TimeManager.actionPoints.ToString("F0") + "/100";		
	}

	public void ShowAlertAP(){
		alertText.text = TimeManager.apAlertString;
	}
}
