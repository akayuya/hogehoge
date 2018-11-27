using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour {


	private Ray shotRay;
	private RaycastHit gunShotHit;
	private GameObject shotEffect;
	private GameObject shotReachEffect;
	private Vector3 hitObjPosition;
	private float shotInterval;
	private AudioSource shotSound;

	

	// Use this for initialization
	void Start () {
		shotEffect = Resources.Load<GameObject>("Effects/ShotEffect");
		shotReachEffect = Resources.Load<GameObject>("Effects/ShotReachEffect");
		shotInterval = 0;
		shotSound = gameObject.GetComponent<AudioSource>();

	}
	
	// Update is called once per frame
	void Update () { 

		shotInterval += Time.deltaTime;

		if(Input.GetMouseButtonDown(0)　&& shotInterval >=0.5f){
			shotSound.Play();
			shotRay = new Ray (Camera.main.transform.position,Camera.main.transform.forward);
			if(Physics.Raycast(shotRay, out gunShotHit)){
				hitObjPosition = gunShotHit.point;
			
			
			}
			
			Instantiate(shotEffect,this.transform.position,Quaternion.identity);
			Instantiate(shotReachEffect,hitObjPosition,Quaternion.identity);
			shotInterval = 0;
	
		}
	}
}
