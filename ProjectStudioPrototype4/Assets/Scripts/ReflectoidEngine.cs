using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ReflectoidEngine : MonoBehaviour {

	GameObject parent;

	public enum ReflectionState{
		None,
		One,
		Two,
	}

	ReflectionState reflectionState;
	float speed = 10f;
	
	private Vector3 initialDir;
	List<Vector3> reflectionPoints = new List<Vector3>();
	
	public Vector3 firstPoint;
	public Vector3 secondPoint;
	public Vector3 thirdPoint;
	public void GetInitialDirection(Vector3 direction){
		initialDir = direction;
		Debug.Log("Direction taken!");
	}
	public bool isInFirstTween;
	public bool isInSecondTween;
	public bool isInThirdTween;
	public Vector3 firstReflection;
	public Vector3 secondReflection;
	private Vector3 firstRayDir;
	private Vector3 secondRayDir;
	private Vector3 thirdRayDir;

	private Rigidbody rb;

 	public void Start () {
		rb = GetComponent<Rigidbody>();
		// parent = transform.parent.gameObject;
		isInFirstTween = false;
		isInSecondTween = false;
		isInThirdTween = false;
		reflectionState = ReflectionState.None;
		rb.AddForce(transform.forward * 25f, ForceMode.Impulse);
	}
	
	
	// Update is called once per frame
	void Update () {

		// rb.AddForce()
		// if(!isInFirstTween && !isInSecondTween && !isInThirdTween){
		// 	if(firstPoint != Vector3.zero){
		// 		transform.DOMove(firstPoint, 0.5f, false);
		// 		if(Vector3.Distance(transform.position, firstPoint) <= 0.1f){
 		// 			isInFirstTween = true;
		// 		}
		// 	} else if (firstPoint == Vector3.zero){
		// 		Debug.Log("First reflection end!");
		// 		transform.Translate(firstReflection * 10f * Time.deltaTime);
		// 	} 
		// }  
		
		// if (isInFirstTween && !isInSecondTween && !isInThirdTween){
		// 	if(secondPoint != Vector3.zero){
		// 		Debug.Log("Starting second path!");
		// 		transform.DOMove(secondPoint, 0.5f, false);
		// 		if(Vector3.Distance(transform.position, secondPoint) <= 0.1f){
		// 			isInSecondTween = true;
		// 		}
		// 	}
		// 	else if (secondPoint == Vector3.zero){
		// 		Debug.Log("Second reflection end!");
		// 		transform.Translate(firstReflection * 10f * Time.deltaTime);
		// 		// Destroy(gameObject);
		// 	}
		// } 

		// if (isInFirstTween && isInSecondTween && !isInThirdTween){
		// 	if(thirdPoint != Vector3.zero){
		// 		Debug.Log("Starting third path!");
		// 		transform.DOMove(thirdPoint, 0.5f, false);
		// 		if(Vector3.Distance(transform.position, thirdPoint) <= 0.1f){
		// 			isInThirdTween = true;
		// 			Destroy(gameObject, 1f);
		// 		}
		// 	}
		// }

		// switch(reflectionState){
		// 	case ReflectionState.None:			
		// 	if(!isTweening){
		// 		transform.DOMove(firstPoint, 0.5f, false);
		// 		if(transform.position == firstPoint){
 		// 			isTweening = true;	
		// 			Debug.Log("Tween complete!");
		// 		}
		// 	}
		// 	break;
		// 	case ReflectionState.One:
			
		// 	break;
		// 	case ReflectionState.Two:
		// 	break;
		// 	default:
		// 	break;
		// }
	}

	public void ShootRay(){
		Ray ray = new Ray(transform.position, transform.forward);
		RaycastHit rayHit = new RaycastHit();

		Debug.DrawRay(transform.position, transform.forward * 100f, Color.red, 3f);
 		if(Physics.Raycast(ray, out rayHit, Mathf.Infinity)){
			firstReflection = Vector3.Reflect(ray.direction, rayHit.normal);
			reflectionPoints.Add(rayHit.point);
			Debug.DrawRay (rayHit.point, firstReflection * Mathf.Infinity, Color.blue);
			if(rayHit.transform.tag == "Player"){
				firstPoint = rayHit.point;
				reflectionState = ReflectionState.None;					 
 			} else if (rayHit.transform.tag != "Player"){
				//first ricochet.
				firstPoint = rayHit.point;
				reflectionState = ReflectionState.One;					 
 				Ray secondRay = new Ray(rayHit.point, firstReflection);
				RaycastHit secondRayHit = new RaycastHit();
				Debug.DrawRay(rayHit.point, firstReflection * 100f, Color.blue, 3f);		
				firstRayDir = firstReflection;
				// reflectoid.transform.DOMove(secondRayHit.point, 0.5f, false);
				if(Physics.Raycast(secondRay, out secondRayHit, Mathf.Infinity)){
					if(secondRayHit.transform.tag != "Player"){
						secondPoint = secondRayHit.point;
						reflectionPoints.Add(secondRayHit.point);
						secondReflection = Vector3.Reflect(secondRay.direction, secondRayHit.normal);
						Ray thirdRay = new Ray(secondRayHit.point, secondReflection);
						RaycastHit thirdRayHit = new RaycastHit();	
						Debug.DrawRay(secondRayHit.point, secondReflection * 100f, Color.green, 3f);
						if(Physics.Raycast(thirdRay, out thirdRayHit, Mathf.Infinity)){
							thirdPoint = thirdRayHit.point; 
							if(thirdRayHit.transform.tag != "Player"){
								Debug.Log("Hit something with third ray!");
								
							} else {
								Debug.Log("Hit player with third ray!");
							}
						}
					} 
					//if reflection hit the player
				} 
			}
		} 
	}

	void OnTriggerEnter(Collider coll){
		//use ontriggerenter here.
	}
}
