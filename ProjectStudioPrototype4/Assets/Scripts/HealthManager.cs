using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour {

	// Use this for initialization
	public static void CheckPlayerHealth(GameObject playerHit_, int health_){
		// Debug.Log();
		if(health_ <= 0){
			Debug.Log(playerHit_.GetComponent<PlayerHealthManager>().myName + " is dead!");
		}		
	}
}
