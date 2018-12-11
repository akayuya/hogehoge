using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPController : MonoBehaviour
{

    public int _targetHP = 5;
    public const int TARGET_HP_FULL = 5;
    public const int TARGET_HP_EMPTY = 0;
    ShotController shotController;
    GameObject shotControl;

    // Use this for initialization
    void Start()
    {
        shotControl = GameObject.Find("ShotControl");

        shotController = shotControl.GetComponent<ShotController>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void TargetHitHP()
    {
        if (shotController.gunShotHit.collider.gameObject.tag == "Target")
        {
            _targetHP--;
            print(_targetHP);
        }
        else if (shotController.gunShotHit.collider.gameObject.tag == "HeadMarker")
        {
            _targetHP--;
            print(_targetHP);
        }

    }
    public void TargetHPFull()
    {

        if (_targetHP != TARGET_HP_EMPTY)
        {
            return;
        }

        _targetHP = TARGET_HP_FULL;
        print(_targetHP);

    }
}
