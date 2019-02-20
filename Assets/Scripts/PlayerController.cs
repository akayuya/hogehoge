using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Photon.MonoBehaviour {

	public int _playerHP = 5;

	private PhotonView view;

	void Start()
	{
		view  = this.gameObject.GetPhotonView();
	}

	[PunRPC]
	public void HitPlayer()
	{
		print(this.gameObject.GetPhotonView().owner.NickName + "がうたれた");
		_playerHP--;
		print(this.gameObject.GetPhotonView().owner.NickName + "の残りHP" + _playerHP);
	}
	
	public void DeadPlayer()
	{
		if(!view.isMine) return; 
		this.gameObject.GetComponentInChildren<ShotController>().scopeImage.gameObject.SetActive(true);
		PhotonNetwork.Destroy(this.gameObject);
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