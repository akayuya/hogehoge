using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] ScoreController scoreController;
    [SerializeField] TargetController targetController;
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

        if(playerController == null)
        {
            GetPlayerController();
        }
        
        if(shotController == null)
        {
            shotController = GetPlayerController().transform.GetComponentInChildren<ShotController>();
        }

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
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach(GameObject player in players)
        {
            if(player.GetPhotonView().ownerId == players.Length)
            {
                playerController = player.GetComponent<PlayerController>();
            }
        }
        return playerController;
    }       
}