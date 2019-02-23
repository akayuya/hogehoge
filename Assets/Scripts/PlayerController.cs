using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Photon.MonoBehaviour {

	public int PlayerHP {get; private set;}

	private ShotController shotController;
	public ShotController GetShotController {get {return shotController;}}

	public PhotonView View { get; private set;}


	void Start()
	{
		PlayerHP = 5;  //Playerの初期HPを設定。
		View  = this.gameObject.GetPhotonView();
		shotController = this.gameObject.GetComponentInChildren<ShotController>();
	}

	[PunRPC]
	public void HitPlayer()
	{
		print(this.gameObject.GetPhotonView().owner.NickName + "がうたれた");
		PlayerHP--;
		print(this.gameObject.GetPhotonView().owner.NickName + "の残りHP" + PlayerHP);
	}
	
	void OnPhotonSerializeView(PhotonStream stream,PhotonMessageInfo info)
	{
		if(stream.isWriting)
		{
			stream.SendNext(PlayerHP);
		}
		else
		{
			PlayerHP = (int)stream.ReceiveNext();
		}
	}
}