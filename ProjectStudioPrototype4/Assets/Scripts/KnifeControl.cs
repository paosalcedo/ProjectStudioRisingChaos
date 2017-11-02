﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class KnifeControl : LaserControl {

	private Vector3 startPos;
	private Vector3 startRot; 
	public GameObject knife;
	// Use this for initialization
	protected override void Start () {
		knife.SetActive(true);
		thisPlayerTimeManager = GetComponentInParent<PlayerTimeManager>();		
		myAPcost = Services.WeaponDefinitions.weapons[WeaponDefinitions.WeaponType.Knife].ap_cost;
		damage = Services.WeaponDefinitions.weapons[WeaponDefinitions.WeaponType.Knife].damage;
		startPos = knife.transform.localPosition;
		startRot = knife.transform.localEulerAngles;
	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update();
	}

	void AnimateKnifeAttack(){
		Debug.Log("Animating knife!");
		knife.transform.DOLocalMove(Vector3.left, 0.3f, false);
		knife.transform.DOLocalRotate(new Vector3 (0, 0, 90), 0.3f, RotateMode.FastBeyond360);
		StartCoroutine(AnimateKnifeReturn(0.4f));
	}

	IEnumerator AnimateKnifeReturn(float delay){
		yield return new WaitForSeconds(delay);
		knife.transform.DOLocalMove(startPos, 0.3f, false);
		knife.transform.DOLocalRotate(startRot, 0.3f, RotateMode.FastBeyond360);
	}

	public override void Attack(KeyCode key){
		if(Input.GetKeyDown(key)){
			AnimateKnifeAttack();
			thisPlayerTimeManager.myActionPoints -= myAPcost;  
		}
  	}


}
