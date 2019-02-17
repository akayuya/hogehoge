using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] ScoreController scoreController;
    [SerializeField] TargetController targetController;
    [SerializeField] SpawnController spawnController;
    [SerializeField] PhotonController photonController;
    private ShotController shotController;
    private PlayerController playerController;


    [SerializeField] UIManager uiManager;
    [SerializeField] BoxCollider headMarkerBoxCollider;
    private Vector3 headMarkerCenter;


    private float _timeLimit;
    private const int TIME_LIMIT = 90;
    private const int BULLET_STOCK_FIRST = 30;


    void Start()
    {
        headMarkerCenter = headMarkerBoxCollider.bounds.center;
    }

    void Update()
    {
        if(photonController._startSpawn) StartSpawn();

        if(playerController._dead) StartRespawn();

        _timeLimit = TIME_LIMIT - Time.time;
        uiManager.UpdateText(_timeLimit, scoreController._score, shotController._bulletBox, shotController._bullet, BULLET_STOCK_FIRST,playerController._playerHP);

        if (targetController._hitHeadMarker)
        {
            scoreController.CalcScore(headMarkerCenter, targetController.hitPosition);
            targetController._hitHeadMarker = false;
        }
    }

    private PlayerController GetPlayerController()
    {
        foreach(GameObject player in spawnController.players)
        {
            print(player.GetPhotonView().owner.NickName);
            if(player.GetPhotonView().isMine)
            {
                playerController = player.GetComponent<PlayerController>();
            }
        }
        return playerController;
    }

    private void GetShotController()
    {
        shotController = GetPlayerController().transform.GetComponentInChildren<ShotController>();
    }

    private void  StartSpawn()
    {
        spawnController.SpawnPlayer();
        photonController._startSpawn = false;
        GetPlayerController();
        GetShotController();
    }

    private void StartRespawn()
    {
        playerController.DeadPlayer();
        spawnController.players.Remove(playerController.gameObject);
        spawnController.SpawnPlayer();
        GetPlayerController();
        GetShotController();
    }
}