using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class SpawnController : MonoBehaviour
{
    [SerializeField] GameObject player;
    public List<GameObject> players = new List<GameObject>();

    public bool _spawn;

    void Start()
    {
        players = new List<GameObject>();
    }
    void Update()
    {
        if(_spawn)
        {
            SpawnPlayer();
            _spawn = false;
        }
    }

    public void SpawnPlayer()
    {
        Vector3 playerStartPos = new Vector3(3f, 1f, 3f);

        GameObject myPlayer = PhotonNetwork.Instantiate(player.name, playerStartPos, Quaternion.identity, 0);

        myPlayer.transform.Find("FirstPersonCharacter").gameObject.SetActive(true);
        ((MonoBehaviour)myPlayer.GetComponent<FirstPersonController>()).enabled = true;
        myPlayer.GetPhotonView().owner.NickName = "Player" + myPlayer.GetPhotonView().ownerId;
        players.Add(myPlayer);
    }
}