using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] GameObject player;

    public bool spawn;
    public bool respawn;

    void Update()
    {
        if(spawn)
        {
            SpawnPlayer();
            spawn = false;
        }

        if(respawn) 
        {
            SpawnPlayer();
            respawn = false;
        }
    }
    
    public void SpawnPlayer()
    {
        Vector3 playerStartPos = new Vector3(3f, 1f, 3f);

        GameObject myPlayer = PhotonNetwork.Instantiate(player.name, playerStartPos, Quaternion.identity, 0);

        myPlayer.transform.Find("FirstPersonCharacter").gameObject.SetActive(true);
        ((MonoBehaviour)myPlayer.GetComponent("FirstPersonController")).enabled = true;
        myPlayer.GetPhotonView().owner.NickName = "Player" + myPlayer.GetPhotonView().ownerId;
    }
}