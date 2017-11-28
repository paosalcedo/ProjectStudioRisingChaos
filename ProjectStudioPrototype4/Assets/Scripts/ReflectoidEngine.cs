using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ReflectoidEngine : MonoBehaviour {

	double delay = 0.0001f;
	GameObject parent;
	GameObject[] players;
	public GameObject playerWhoFiredMe;
	private AudioSource audioSource;
	private bool damageDealt;
 	float speed = 10f;

	public int bounces;	

	private Rigidbody rb;

 	public void Start () {
		audioSource = GetComponent<AudioSource>();
		damageDealt = false;
		rb = GetComponent<Rigidbody>();
		players = GameObject.FindGameObjectsWithTag("Player");
 		rb.AddForce(transform.forward * 25f, ForceMode.Impulse);
	}
	
	
	// Update is called once per frame
	void Update () {
		DetectPlayers();

		if(bounces > 2){
			Destroy(gameObject);
		}
	}

	public bool bounceHasPlayed = false;
	void OnCollisionEnter(Collision coll){
		bounces += 1;
		if(!bounceHasPlayed){
			Debug.Log("Collision sound should play!");
					audioSource.pitch = Random.Range(0.1f, 1);

			audioSource.PlayScheduled(AudioSettings.dspTime + delay);
			bounceHasPlayed = true;
			StartCoroutine(ResetBoolToFalse(0.01f));
		}
	}

	IEnumerator ResetBoolToFalse(float delay){
		yield return new WaitForSeconds(delay);
		bounceHasPlayed = false;
		// Debug.Log("Bool should be false!");
	}

	void DetectPlayers(){
		foreach (GameObject player in players){
			float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
 
			RaycastHit hit;

			Vector3 rayDirection = player.transform.position - transform.position;

			if (Physics.Raycast (transform.position, rayDirection, out hit)) {
				if (hit.transform.tag == "Player" && player != playerWhoFiredMe && distanceToPlayer <= 3f) {
					if(!damageDealt){
						player.GetComponent<PlayerHealthManager>().DepleteHealth(40);					
						damageDealt = true;
						if(player.GetComponent<PlayerHealthManager>().currentHealth > 0){
						if(player.gameObject == CurrentPlayerTracker.otherPlayer){
						//tell the canvas of currentPlayer to show a hit alert.
							CurrentPlayerTracker.currentPlayer.GetComponent<PlayerTimeManager>().myCanvas.GetComponent<PlayerCanvasUpdater>().UpdateHitAlert(player.GetComponent<PlayerIdentifier>().myName, 40);
						//tell otherPlayer to show UpdateGotHitAlert.
							CurrentPlayerTracker.otherPlayer.GetComponent<PlayerTimeManager>().myCanvas.GetComponent<PlayerCanvasUpdater>().UpdateGotHitAlert(CurrentPlayerTracker.currentPlayer.transform.GetComponent<PlayerIdentifier>().myName, 40);
						} else {
						//tell the canvas of otherPlayer to show a hit alert.
							CurrentPlayerTracker.otherPlayer.GetComponent<PlayerTimeManager>().myCanvas.GetComponent<PlayerCanvasUpdater>().UpdateHitAlert(player.GetComponent<PlayerIdentifier>().myName, 40);
						//tell otherPlayer to show UpdateGotHitAlert.	
							CurrentPlayerTracker.currentPlayer.GetComponent<PlayerTimeManager>().myCanvas.GetComponent<PlayerCanvasUpdater>().UpdateGotHitAlert(CurrentPlayerTracker.otherPlayer.GetComponent<PlayerIdentifier>().myName, 40);
						}
					} else {
						//if player 2 died
						if(player.gameObject == CurrentPlayerTracker.otherPlayer){
							CurrentPlayerTracker.currentPlayer.GetComponentInChildren<PlayerCanvasUpdater>().UpdateAlertTextWithFrag(CurrentPlayerTracker.otherPlayer.GetComponent<PlayerIdentifier>().myName);
						} 
						//if player 1 died
						else {
							CurrentPlayerTracker.otherPlayer.GetComponentInChildren<PlayerCanvasUpdater>()
							.UpdateAlertTextWithFrag(CurrentPlayerTracker.currentPlayer.GetComponent<PlayerIdentifier>().myName);
						}				
					}
					}
				} 
			}
			// if(distanceToPlayer <= 3f && player != playerWhoFiredMe){
 			// } 
			//check if you're the player who fired.
		}
	}

}
