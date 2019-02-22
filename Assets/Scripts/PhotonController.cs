using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonController : MonoBehaviour
{
    private const string ROOM_NAME = "RoomA";

    public bool _joined {get; private set;}
    public bool Joined {get {return _joined;}}

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

        _joined = true;
    }
}