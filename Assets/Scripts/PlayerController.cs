using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Photon.MonoBehaviour {

	public int _playerHP = 5;

	public ShotController shotController;

	public PhotonView view;

	void Start()
	{
		view  = this.gameObject.GetPhotonView();
		shotController = this.gameObject.GetComponentInChildren<ShotController>();
	}

	[PunRPC]
	public void HitPlayer()
	{
		print(this.gameObject.GetPhotonView().owner.NickName + "がうたれた");
		_playerHP--;
		print(this.gameObject.GetPhotonView().owner.NickName + "の残りHP" + _playerHP);
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