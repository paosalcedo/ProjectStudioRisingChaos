using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using DG.Tweening;
public class LadderScript : MonoBehaviour {
	bool soundPlayed = false;
	public GameObject ladderSoundObject; 
	private AudioSource ladderSound;
	private int myPlayerNum;
	PlayerIdentifier playerIdentifier;
	float ladderSpeed = 100f;
	// Use this for initialization
	Rigidbody rb;
	FirstPersonController fpc;
	void Start () {
		playerIdentifier = GetComponent<PlayerIdentifier>();
		ladderSound = ladderSoundObject.GetComponent<AudioSource>();
		fpc = GetComponent<FirstPersonController>();
		rb = GetComponent<Rigidbody>();
		myPlayerNum = playerIdentifier.myPlayerNum;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider coll){

		if(coll.gameObject.tag == "Ladder"){
			if(myPlayerNum == 0){
				if(Input.GetAxisRaw("Vertical") > 0){
					transform.Translate(Vector3.up * 0.05f);
					if(!soundPlayed && !ladderSound.isPlaying){
						//play sound
						ladderSound.PlayScheduled(AudioSettings.dspTime + 0.000001f);
						soundPlayed = true;
					}
 				}
			} else {
				if(Input.GetAxisRaw("P2_Vertical") > 0){
 					transform.Translate(Vector3.up * 0.05f);
					if(!soundPlayed && !ladderSound.isPlaying){
						//play sound
						ladderSound.PlayScheduled(AudioSettings.dspTime + 0.000001f);
						soundPlayed = true;
					}
 				}	
			}
		
		}
	}
	void OnTriggerStay(Collider coll){

		if(coll.gameObject.tag == "Ladder"){
			if(myPlayerNum == 0){
				if(Input.GetAxisRaw("Vertical") > 0){
					transform.Translate(Vector3.up * 0.05f);
					// if(!soundPlayed){
					// 	//play sound
					// 	ladderSound.PlayScheduled(AudioSettings.dspTime + 0.000001f);
					// 	soundPlayed = true;
					// }
 				}
			} else {
				if(Input.GetAxisRaw("P2_Vertical") > 0){
 					transform.Translate(Vector3.up * 0.05f);
					//  if(!soundPlayed){
					// 	//play sound
					// 	ladderSound.PlayScheduled(AudioSettings.dspTime + 0.000001f);
					// 	soundPlayed = true;
					// }
 				}	
			}
		
		}
	}

	void OnTriggerExit(){
		soundPlayed = false;
	}
}
