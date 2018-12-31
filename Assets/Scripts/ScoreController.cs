using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    private float _targetScore;
    private const int HIT_HEADMARKER_SCORE_CENTER = 50;
    private const int SCORE_MAGNIFICATION = 50;
    public void CalcScore(Vector3 center, Vector3 hitPosition)
    {
        _targetScore += ((Vector3.Distance(center, hitPosition)) * SCORE_MAGNIFICATION);
        print(_targetScore);
    }
}