using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNames : MonoBehaviour {

	public static string playerOneName;
	public static string playerTwoName;
	public static InputField p1NameInputFieldStatic;
	public static InputField p2NameInputFieldStatic;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
			
	}

	public static void AssignNames(string p1Name, string p2Name){
		playerOneName = p1Name;
		playerTwoName = p2Name;
		Debug.Log(playerOneName);
		Debug.Log(playerTwoName);
	}

}
