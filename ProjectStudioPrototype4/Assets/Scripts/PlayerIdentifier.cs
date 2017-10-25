﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdentifier : MonoBehaviour {

	public string myName;
	public int myPlayerNum;
	// Use this for initialization
	void Start () {

		//this won't scale well. Consider using a for loop. But PlayerNames.cs would have to be more modular also. 
		//Maybe use a list if strings there.
		
	}
	
	// Update is called once per frame
	void Update () {
		if(myPlayerNum == 0){
			myName = PlayerNames.playerOneName;
		} else {
			myName = PlayerNames.playerTwoName;
		}	
	}


}
