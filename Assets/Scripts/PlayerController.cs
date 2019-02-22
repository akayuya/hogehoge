using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Photon.MonoBehaviour {

	private int _playerHP = 5;
	public int GetPlayerHP {get {return _playerHP;}}

	private ShotController shotController;
	public ShotController GetShotController {get {return shotController;}}

	private PhotonView view;
	public PhotonView GetView { get {return view;}}

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