using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserLineGenerator : MonoBehaviour {

	public float l_width;

	List<Vector3> nodePositions = new List<Vector3>();

	LineRenderer lr;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AddNodeToList(Vector3 newNode_){
		nodePositions.Add(newNode_);
	}

	public void DrawLine(){
		Vector3[] points = new Vector3[nodePositions.Count];
 		for (int i = 0; i < nodePositions.Count; i++) {
			points [i] = new Vector3 (nodePositions[i].x, nodePositions[i].y, nodePositions[i].z);
		}

		lr.positionCount = points.Length;
		lr.SetPositions (points);

		lr.startWidth = l_width;
		lr.endWidth = l_width;
		lr.startColor = Color.yellow;
		lr.endColor = Color.yellow;
	}
}