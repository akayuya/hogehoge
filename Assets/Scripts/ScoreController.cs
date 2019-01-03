using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [System.NonSerialized] public float _score;
    private const int SCORE_MAGNIFICATION = 50;

    public void CalcScore(Vector3 center, Vector3 hitPosition)
    {
        _score += ((Vector3.Distance(center, hitPosition)) * SCORE_MAGNIFICATION);
        print(_score);
    }
}
