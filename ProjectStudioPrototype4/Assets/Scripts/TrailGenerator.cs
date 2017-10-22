﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailGenerator : MonoBehaviour {

	public enum DrawingState
	{
		DRAWING,
		NOT_DRAWING
	}

	public DrawingState drawingState;

	List<GameObject> nodes = new List<GameObject>();

	List<Vector3> nodePositions = new List<Vector3>();
 
	public LayerMask targetLayer;
 
	public int maxNodes = 10;

	float distanceBetweenNodes = 0;
	float totalLengthOfLine;
	public float maxLengthOfLine = 0;

	[SerializeField]float lineInterval = 1f;
	[SerializeField]float startingLineInterval = 1f;

	[SerializeField]float l_width;
	[SerializeField]float l_length;

	public KeyCode drawKey;
	LineRenderer lr;

	// Use this for initialization
	void Start () {
		lr = GetComponent<LineRenderer> ();
 		drawingState = DrawingState.NOT_DRAWING;
 	}
	
	// Update is called once per frame
	void Update () {
		if(drawingState == DrawingState.DRAWING){
			DrawLine ();
			DropNodesNoInput ();
			Debug.Log(drawingState);
		} else {
			Debug.Log(drawingState);
		}
 	}

	void DropNodesNoInput(){

		lineInterval -= Time.deltaTime;

		Ray ray = new Ray(transform.position, Vector3.down);

		RaycastHit rayHit = new RaycastHit ();
		if (Physics.Raycast (ray, out rayHit, Mathf.Infinity)) {
			if (rayHit.transform != null) {
				if(lineInterval <= 0){
					// GameObject node_ = Instantiate (Resources.Load ("Node")) as GameObject;
					GameObject node_ = Instantiate (Services.Prefabs.Node) as GameObject;
					node_.transform.position = rayHit.point;
					nodes.Add (node_);
					nodePositions.Add (node_.transform.position);
					lineInterval = startingLineInterval;
					// 					}
				}
			}
		}
	}


	void DropNodes(KeyCode key){

		if (Input.GetKey (key)) {
			lineInterval -= Time.deltaTime;
 		
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			RaycastHit rayHit = new RaycastHit ();
			if (Physics.Raycast (ray, out rayHit, Mathf.Infinity, targetLayer)) {
				if (rayHit.transform != null && rayHit.transform.name != "LineMaker") {
 //					if (lineInterval <= 0) {
					GameObject node_ = Instantiate (Resources.Load ("Node")) as GameObject;
					node_.transform.position = rayHit.point;
					if (nodes.Count > 0) {
//						node_.AddComponent<CharacterJoint> ().connectedBody = nodes [nodes.Count-1].GetComponent<Rigidbody>();
					}
					nodes.Add (node_);
					nodePositions.Add (node_.transform.position);
//						lineInterval = startingLineInterval;
// 					}
				}
			}
		}	
	}


	void DrawLine(){
		Vector3[] points = new Vector3[nodes.Count];
 		for (int i = 0; i < nodes.Count; i++) {
			points [i] = new Vector3 (nodes[i].transform.position.x, nodes[i].transform.position.y, nodes[i].transform.position.z);
		}

		lr.positionCount = points.Length;
		lr.SetPositions (points);

		lr.startWidth = l_width;
		lr.endWidth = l_width;
		lr.startColor = Color.red;
		lr.endColor = Color.red;
	}

	float length = 0;

	void GetLengthOfLine(){
		if (nodes.Count > 1) {
			for (int i = 0; i < nodes.Count; i++) {
//				length = Vector3.Distance (nodes [i].transform.position, nodes [i - 1].transform.position);
			}
		}
		Debug.Log ("Length = " + length);
		totalLengthOfLine += length;
	}

	void OnTriggerEnter(Collider coll){
		drawingState = DrawingState.DRAWING;
	}
	void OnTriggerEnterExit(){
		drawingState = DrawingState.NOT_DRAWING;
	}
}
