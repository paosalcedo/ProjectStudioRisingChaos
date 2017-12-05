using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour {

	public int myPlayerNum;
	Vector2 mouseLook;	
	Vector2 smoothV;

	public float sensitivity = 5.0f;
	public float smoothing = 2.0f;
 
	private Quaternion lastRot;

	GameObject character;

	void Start () {
		character = transform.parent.gameObject;
 	}
	
	// Update is called once per frame
	void Update () {
		if(myPlayerNum == 0){
			Vector2 mousePos = new Vector2 (Input.GetAxisRaw ("Mouse X"), Input.GetAxisRaw ("Mouse Y"));
		
			mousePos = Vector2.Scale (mousePos, new Vector2 (sensitivity * smoothing, sensitivity * smoothing));
			smoothV.x = Mathf.Lerp (smoothV.x, mousePos.x, 1f / smoothing);
			smoothV.y = Mathf.Lerp (smoothV.y, mousePos.y, 1f / smoothing);
			mouseLook += smoothV;
			mouseLook.y = Mathf.Clamp (mouseLook.y, -90f, 90f);

			transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        	character.transform.localRotation = Quaternion.AngleAxis (mouseLook.x, Vector3.up);
		} else {
			Vector2 mousePos = new Vector2 (Input.GetAxisRaw ("P2_Mouse X"), Input.GetAxisRaw ("P2_Mouse Y"));

			mousePos = Vector2.Scale (mousePos, new Vector2 (sensitivity * smoothing, sensitivity * smoothing));
			smoothV.x = Mathf.Lerp (smoothV.x, mousePos.x, 1f / smoothing);
			smoothV.y = Mathf.Lerp (smoothV.y, mousePos.y, 1f / smoothing);
			mouseLook += smoothV;
			mouseLook.y = Mathf.Clamp (mouseLook.y, -90f, 90f);

			// transform.localRotation = Quaternion.AngleAxis(mouseLook.x, Vector3.up);
			// character.transform.localRotation = Quaternion.AngleAxis (mouseLook.y, Vector3.right);	
			transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        	character.transform.localRotation = Quaternion.AngleAxis (mouseLook.x, Vector3.up);
		
		}

		

		

	}


}
