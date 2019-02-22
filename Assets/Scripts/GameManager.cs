﻿using System.Collections;
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
        if(!photonController.Joined) return;

        if(spawnController.GetPlayerController == null) 
        {
            spawnController.SpawnPlayer();
            return;
        }

        _timeLimit = TIME_LIMIT - Time.time;
        uiManager.UpdateText(_timeLimit, scoreController._score, spawnController.GetPlayerController.GetShotController.GetBulletBox, spawnController.GetPlayerController.GetShotController.GetBullet, BULLET_STOCK_FIRST,spawnController.GetPlayerController.GetPlayerHP);

        if (targetController.HasHitHeadMarker)
        {
            scoreController.CalcScore(headMarkerCenter, targetController.GetHitPosition);
            targetController.HasHitHeadMarker = false;
        }
    }
}