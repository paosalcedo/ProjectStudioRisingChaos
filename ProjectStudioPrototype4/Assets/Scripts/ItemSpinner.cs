using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ItemSpinner : MonoBehaviour {

	public Vector3 rotateAxis;
	float rotateSpeed = 50f;
	// Use this for initialization
	void Start () {
		LeanTween.moveY(gameObject, transform.position.y + 0.25f, 1f).setEase(LeanTweenType.easeInOutSine).setLoopPingPong();
		// LeanTween.rotateY(gameObject, )
		// transform.DOLocalMoveY(5f, 1f, false).OnComplete(;
		// transform.DOShakePosition(1f, 3, 10, 0, false).SetLoops(-1);
	}
	
	// Update is called once per frame
	void Update () {
		RotateItem();
	}

	void RotateItem(){
		transform.Rotate(rotateAxis * rotateSpeed * Time.deltaTime);
	}
}
