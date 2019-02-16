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

        if(playerController == null)
        {
            GetPlayerController();
            print(playerController);
        }
        
        if(shotController == null)
        {
            shotController = GetPlayerController().transform.GetComponentInChildren<ShotController>();
            print(shotController);
        }

        if(playerController._startRespawn) StartRespawn(); 

        _timeLimit = TIME_LIMIT - Time.time;
        uiManager.UpdateText(_timeLimit, scoreController._score, shotController._bulletBox, shotController._bullet, BULLET_STOCK_FIRST,playerController._playerHP);

        if (targetController._hitHeadMarker)
        {
            scoreController.CalcScore(headMarkerCenter, targetController.hitPosition);
            targetController._hitHeadMarker = false;
        }

        print("ここまではきてる");
        print(playerController.gameObject.GetPhotonView().ownerId);
    }

    private PlayerController GetPlayerController()
    {
        foreach(GameObject player in spawnController.players)
        {
            if(player.GetPhotonView().isMine)
            {
                playerController = player.GetComponent<PlayerController>();
            }
        }
        return playerController;
    }

    private void  StartSpawn()
    {
        spawnController._spawn = true;
        photonController._startSpawn = false;
    }
    private void StartRespawn()
    {
        print("リスポーン呼ばれた");
        spawnController._spawn = true;
        playerController._startRespawn = false;
    }
}