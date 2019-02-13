using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Photon.MonoBehaviour {

	public int _playerHP = 5;
	private SpawnController spawnController;

	private PhotonView view;

	private bool _dead;

	void Start()
	{
		spawnController = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnController>();
		view  = this.gameObject.GetPhotonView();
	}

	void Update()
	{
		if(_dead) 
		{
			DeadPlayer();
			print("やられた" + view.ownerId);
		}
	}

	[PunRPC]
	public void HitPlayer()
	{
		print("HitPlayerがよばれた");
		_playerHP--;
		print("HitPlayerHP" + _playerHP);

		if(_playerHP == 0) _dead = true;
	}
	
	private void DeadPlayer()
	{
		if(!view.isMine) return;

		PhotonNetwork.Destroy(this.gameObject);

		spawnController.respawn = true;
		_dead = false;
	}

	void OnPhotonSerializeView(PhotonStream stream,PhotonMessageInfo info)
	{
		if(stream.isWriting)
		{
			stream.SendNext(_playerHP);
		}
		else
		{
			_playerHP = (int)stream.ReceiveNext();
		}
	}
}

