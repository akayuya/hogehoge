using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] ScoreController scoreController;
    [SerializeField] TargetController targetController;
    [SerializeField] SpawnController spawnController;
    [SerializeField] PhotonController photonController;

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
        StartSpawnPlayer();
        
        _timeLimit = TIME_LIMIT - Time.time;
        uiManager.UpdateText(_timeLimit, scoreController._score, spawnController.shotController._bulletBox, spawnController.shotController._bullet, BULLET_STOCK_FIRST,spawnController.playerController._playerHP);

        if (targetController._hitHeadMarker)
        {
            scoreController.CalcScore(headMarkerCenter, targetController.hitPosition);
            targetController._hitHeadMarker = false;
        }
    }

    private void StartSpawnPlayer()
    {
        if(photonController._startSpawn) 
        {
            spawnController._spawn = true;
            photonController._startSpawn = false;
        }

        if(spawnController.playerController._dead)
        {
            spawnController._spawn = true;
            spawnController.playerController._dead = false;
        } 
    }
}