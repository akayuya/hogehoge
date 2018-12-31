using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    [SerializeField] ScoreController scoreController;
    [SerializeField] TargetController targetController;
    public Vector3 headMarkerCenter;
    [SerializeField] BoxCollider headMarkerBoxCollider;
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
            scoreController.CalcScore(headMarkerCenter,targetController.hitPosition);
            targetController._hitHeadMarker = false;
        }
    }
}
