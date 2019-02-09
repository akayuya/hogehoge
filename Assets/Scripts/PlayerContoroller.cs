using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContoroller : Photon.MonoBehaviour {

	public int _playerHP = 5;
	private SpawnController spawnController;

	private PhotonView view;


	void Start()
	{
		spawnController = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnController>();
		view  = this.GetComponent<PhotonView>();
	}

	void update()
	{
		if(_playerHP == 0) 
		{
			view.RPC("DeadPlayer",PhotonTargets.AllBuffered);
		}	
	}

	[PunRPC]
	public void HitPlayer(int _hitPlayerHP)
	{
		print("HitPlayerがよばれた");
		_hitPlayerHP--;
		print("HitPlayerHP" + _hitPlayerHP);
		print(view.ownerId);
	}
	
	[PunRPC]
	private void DeadPlayer()
	{
		Destroy(this.gameObject);
		spawnController.SpawnPlayer();
	}

}
