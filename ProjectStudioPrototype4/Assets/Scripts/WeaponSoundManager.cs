using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSoundManager : MonoBehaviour {

	double delay = 0.0001f;
	public AudioClip laserClip;
	public AudioClip grenadeThrowClip;
	public AudioClip grenadePullClip;
	public AudioClip knifeSlashClip;
 	private AudioSource audioSource;
	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();                      
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlayLaserSound(){
		audioSource.clip = laserClip;
		audioSource.volume = 0.25f;
		audioSource.pitch = Random.Range(0.75f, 1);
 		audioSource.PlayScheduled(AudioSettings.dspTime + delay);
	}

	public void PlayGrenadeThrowSound(){
		audioSource.clip = grenadeThrowClip;
		// audioSource.pitch = Random.Range(0.1f, 1);
		audioSource.PlayScheduled(AudioSettings.dspTime + delay + grenadePullClip.length);
	}

	public void PlayGrenadePullPin(){
		audioSource.clip = grenadePullClip;
		audioSource.PlayScheduled(AudioSettings.dspTime + delay);
	}

	public void PlayKnifeSound(){
		audioSource.clip = knifeSlashClip;
		audioSource.pitch = Random.Range(0.75f, 1);
		audioSource.PlayScheduled(AudioSettings.dspTime + delay);
	}

}
