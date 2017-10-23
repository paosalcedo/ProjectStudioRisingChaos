using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

 	public class TimeManager : MonoBehaviour {

		// Use this for initialization
		public static string apAlertString;
		public static float turnTime;

		public static float actionPoints = 100;
		
		public static void DepleteAP(float amountToDepletePerSecond){
			if(amountToDepletePerSecond <= actionPoints){
				actionPoints -= amountToDepletePerSecond;
			} 

			// Debug.Log("Depleting AP!");
			// Debug.Log("AP is now " + actionPoints);
		}

		public static void ResetAP(){
			actionPoints = 100f;
		}

		public static void ClearAlertString(){
			apAlertString = "";
		}
	}

