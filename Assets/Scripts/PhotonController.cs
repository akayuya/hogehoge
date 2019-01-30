using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonController : MonoBehaviour {
	[SerializeField] GameObject player;
	private Vector3 playerStartPos;

	private const string ROOM_NAME = "RoomA";

	// Use this for initialization
	void Start () {
		PhotonNetwork.ConnectUsingSettings(null);
	}

	void OnJoinedLobby()
	{
		// PhotonNetwork.CreateRoom(null);
		PhotonNetwork.JoinOrCreateRoom(ROOM_NAME,new RoomOptions(),TypedLobby.Default);
		print("JoinedLobby");
	}
	
	void OnJoinedLobbyFailed()
	{
		PhotonNetwork.CreateRoom(null);
		print("CreateRoom");

	}

	void OnJoinedRoom()
	{
		playerStartPos = new Vector3 (3f,1f,3f);
		Debug.Log("JoinedRoom");
		PhotonNetwork.Instantiate
		(
			player.name,
			playerStartPos,
			Quaternion.identity,
			0
		);
	}
}
