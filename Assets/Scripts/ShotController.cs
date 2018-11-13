using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour {


	private Ray gunShot;
	private RaycastHit gunShotHit;
	private GameObject shotEffect;
	private GameObject shotReachEffect;
	private Vector3 hitObj;
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
			gunShot = new Ray (Camera.main.transform.position,Camera.main.transform.forward);
			if(Physics.Raycast(gunShot, out gunShotHit)){
				hitObj = gunShotHit.point;
			}
			Instantiate(shotEffect,this.transform.position,Quaternion.identity);
			Instantiate(shotReachEffect,hitObj,Quaternion.identity);
			print(hitObj);
			shotInterval = 0;
	
		}
	}
}
