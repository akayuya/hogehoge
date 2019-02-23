using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonController : MonoBehaviour
{
    private const string ROOM_NAME = "RoomA";

    public bool Joined {get; private set;}

    void Awake()
    {
        PhotonNetwork.ConnectUsingSettings(null);
    }

    void OnJoinedLobby()
    {
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

        Joined = true;
    }
}