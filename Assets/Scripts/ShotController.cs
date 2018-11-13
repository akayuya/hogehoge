using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour {

	private Ray gunShot;
	private GameObject gunEffect;


	// Use this for initialization
	void Start () {
		gunEffect = Resources.Load<GameObject>("Effects/GunEffect");

	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			gunShot = Camera.main.ScreenPointToRay(Input.mousePosition);
			Instantiate(gunEffect,this.transform.position,Quaternion.identity);
			Instantiate(gunEffect,Input.mousePosition,Quaternion.identity);
			print("a");
			print(Input.mousePosition);
		}


	}
}
