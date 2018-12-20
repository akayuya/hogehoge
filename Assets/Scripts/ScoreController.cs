using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    private float _targetScore;
    private const int HIT_HEADMARKER_SCORE_CENTER = 50;
    private const int SCORE_MAGNIFICATION = 50;
    private Vector2 CenterCoordinate;
    private float distanceFromCenterCoordinate;
    // ShotController shotController; 
    // GameObject shotControl;

    // Use this for initialization
    void Start()
    {
        // shotControl = GameObject.Find("ShotControl");

        // shotController = shotControl.GetComponent<ShotController>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void HitTargetScoreHp()
    {
        ShotController shotController;
        GameObject shotControl;
        shotControl = GameObject.Find("ShotControl");

        shotController = shotControl.GetComponent<ShotController>();
        if (shotController.gunShotHit.collider.gameObject.tag == "Target")
        {
            return;
        }
        if (shotController.gunShotHit.collider.gameObject.tag == "HeadMarker")
        {
            CenterCoordinate = shotController.gunShotHit.collider.GetComponent<BoxCollider>().bounds.center;
            distanceFromCenterCoordinate = (Vector2.Distance(CenterCoordinate, shotController.hitObjPosition));
            _targetScore += (HIT_HEADMARKER_SCORE_CENTER - (distanceFromCenterCoordinate * SCORE_MAGNIFICATION));
            print(_targetScore);

        }
    }
}
