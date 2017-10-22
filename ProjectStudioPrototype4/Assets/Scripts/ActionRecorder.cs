using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ActionRecorder : MonoBehaviour
{

    List<Vector3> rotations = new List<Vector3>();
    List<Vector3> positions = new List<Vector3>();
    List<Vector3> camRotations = new List<Vector3>();
    
    List<bool> attacks = new List<bool>();

    private int playbackIndex;

    private Transform myCam;

    void Start(){
        myCam = transform.GetChild(0);
    }

    void Update(){

    }

    public void Record(Vector3 rotation_, Vector3 position_, Vector3 camRotation_){
        rotations.Add(rotation_);
        positions.Add(position_);
        camRotations.Add(camRotation_);
    }

    public void PlayRecording(){
        if(playbackIndex < positions.Count-1)
        {
			playbackIndex++;
            transform.position = positions[playbackIndex];
			transform.eulerAngles = rotations[playbackIndex];
            //need reference to camera
			// isAttacking = enemyAttacks[playbackIndex];
        }
        else if(playbackIndex == positions.Count - 1) {
            // recordingState = RecordingState.NOT_RECORDING;
			playbackIndex = 0;
			transform.position = positions[playbackIndex];
			transform.eulerAngles = rotations[playbackIndex];
            //need reference to camera
			// isAttacking = enemyAttacks[playbackIndex];
        }
    }
    
}