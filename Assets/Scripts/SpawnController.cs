using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class SpawnController : MonoBehaviour
{
    [SerializeField] GameObject player;
    private GameObject myPlayer;

    private PlayerController playerController;
    public PlayerController GetPlayerController {get {return playerController;}}

    void Update()
    {
        if(playerController == null) return;

        if(playerController.GetPlayerHP == 0) DeadPlayer();
    }

    public void SpawnPlayer()
    {
        Vector3 playerStartPos = new Vector3(3f, 1f, 3f);

        myPlayer = PhotonNetwork.Instantiate(player.name, playerStartPos, Quaternion.identity, 0);

        myPlayer.transform.Find("FirstPersonCharacter").gameObject.SetActive(true);
        ((MonoBehaviour)myPlayer.GetComponent<FirstPersonController>()).enabled = true;

        myPlayer.GetPhotonView().owner.NickName = "Player" + myPlayer.GetPhotonView().ownerId;

        playerController = myPlayer.gameObject.GetComponent<PlayerController>();
    }

    private void DeadPlayer()
    {
        if(!playerController.GetView.isMine) return;

		playerController.gameObject.GetComponentInChildren<ShotController>().SwitchScopeImage();
		PhotonNetwork.Destroy(playerController.gameObject);
    }
}