using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class SpawnController : MonoBehaviour
{
    [SerializeField] GameObject player;
    private GameObject myPlayer;

    public PlayerController PlayerController {get ; private set;}

    void Update()
    {
        if(PlayerController == null) return;

        print(PlayerController.PlayerHP);
        print(PlayerController.View.isMine);
        if(PlayerController.PlayerHP == 0) DeadPlayer();
    }

    public void SpawnPlayer()
    {
        Vector3 playerStartPos = new Vector3(3f, 1f, 3f);

        myPlayer = PhotonNetwork.Instantiate(player.name, playerStartPos, Quaternion.identity, 0);

        myPlayer.transform.Find("FirstPersonCharacter").gameObject.SetActive(true);
        ((MonoBehaviour)myPlayer.GetComponent<FirstPersonController>()).enabled = true;

        PlayerController = myPlayer.gameObject.GetComponent<PlayerController>();

        PlayerController.View.owner.NickName = "Player" + PlayerController.View.ownerId;
    }

    private void DeadPlayer()
    {
        if(!PlayerController.View.isMine) return;

		PhotonNetwork.Destroy(PlayerController.gameObject);
    }
}