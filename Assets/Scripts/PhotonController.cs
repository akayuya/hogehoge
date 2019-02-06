using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonController : MonoBehaviour
{
	[SerializeField] SpawnController spawnController;
    private const string ROOM_NAME = "RoomA";

    // Use this for initialization
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(null);
    }

    void OnJoinedLobby()
    {
        // PhotonNetwork.CreateRoom(null);
        PhotonNetwork.JoinOrCreateRoom(ROOM_NAME, new RoomOptions(), TypedLobby.Default);
        print("JoinedLobby");
    }

    void OnJoinedLobbyFailed()
    {
        PhotonNetwork.CreateRoom(null);
        print("CreateRoom");

    }

    void OnJoinedRoom()
    {
        Debug.Log("JoinedRoom");
        spawnController.SpawnPlayer();
        print("hoeg");
    }
}
