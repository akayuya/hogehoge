using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    [SerializeField] ScoreController scoreController;
    [SerializeField] TargetController targetController;
    [SerializeField] BoxCollider headMarkerBoxCollider;
    [SerializeField] ShotController shotController;
    [SerializeField] UIManager uiManager;
    [System.NonSerialized] public Vector3 headMarkerCenter;
    private float _timeLimit;
    private const float TIME_LIMIT = 90;

    private const int BULLET_STOCK_FIRST = 30;
    // Use this for initialization
    void Start()
    {
        headMarkerCenter = headMarkerBoxCollider.bounds.center;
    }
    // Update is called once per frame
    void Update()
    {
        _timeLimit = TIME_LIMIT - Time.time;

        uiManager.UpdateText(_timeLimit,scoreController._targetScore,shotController._bulletBox,shotController._bullet,BULLET_STOCK_FIRST);

        if (targetController._hitHeadMarker)
        {
            scoreController.CalcScore(headMarkerCenter, targetController.hitPosition);
            targetController._hitHeadMarker = false;
        }
    }
}
