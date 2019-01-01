using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    [SerializeField] ScoreController scoreController;
    [SerializeField] TargetController targetController;
    [SerializeField] BoxCollider headMarkerBoxCollider;
    public Vector3 headMarkerCenter;
    public float _timeLimit;
    private const float TIME_LIMIT = 90;


    // Use this for initialization
    void Start()
    {
        headMarkerCenter = headMarkerBoxCollider.bounds.center;
    }
    // Update is called once per frame
    void Update()
    {
        if (targetController._hitHeadMarker)
        {
            scoreController.CalcScore(headMarkerCenter, targetController.hitPosition);
            targetController._hitHeadMarker = false;
        }
        _timeLimit = TIME_LIMIT - Time.time;
    }
}
