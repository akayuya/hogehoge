using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TargetController : MonoBehaviour
{
    private Animator targetMotion;
    private const int TARGET_HP_EMPTY = 0;
    private const int START_REVIVE_MOTION_INTERVAL = 3;
    private const int DEFAULT_MOTION_TRUE_INTERVAL = 1;
    private const float END_REVIVE_MOTION_INTERVAL = 0.5f;
    public const int TARGET_HP_FULL = 5;
    public int _targetHP = 5;
    private bool _isCrushTarget;
    private bool _isReviveTarget;
    private bool _isDefaultTarget;
    // GameObject shotControl;

    // ShotController shotController;

    // Use this for initialization
    void Start()
    {
        // shotControl = GameObject.Find("ShotControl");

        // shotController = shotControl.GetComponent<ShotController>();

        targetMotion = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void TargetCrashMotion()
    {
        if (_targetHP != TARGET_HP_EMPTY)
        {
            return;
        }
        _isCrushTarget = true;
        _isReviveTarget = true;
        targetMotion.SetBool("IsCrushTarget", _isCrushTarget);

        StartCoroutine(TargetReviveMotion());

    }
    public IEnumerator TargetReviveMotion()
    {
        if (_isReviveTarget)
        {

            yield return new WaitForSeconds(START_REVIVE_MOTION_INTERVAL);
            targetMotion.SetBool("IsReviveTarget", _isReviveTarget);
            // IsReviveTargetのアニメが終わるまでのインターバル設定。
            yield return new WaitForSeconds(END_REVIVE_MOTION_INTERVAL);

            StartCoroutine(StopTargetMotion());

            TargetHPFull();

            yield break;
        }
    }
    public IEnumerator StopTargetMotion()
    {
        // StopTargetMotionの処理前に繰り返されないように_isCrushTargetをfalseに。
        _isCrushTarget = false;
        targetMotion.SetBool("IsCrushTarget", _isCrushTarget);
        _isDefaultTarget = true;
        targetMotion.SetBool("IsDefaultTarget", _isDefaultTarget);

        _isReviveTarget = false;
        targetMotion.SetBool("IsReviveTarget", _isReviveTarget);

        // IsDeafaultTargetの判定が出てからfalseにするためインターバルを設定。
        yield return new WaitForSeconds(DEFAULT_MOTION_TRUE_INTERVAL);
        _isDefaultTarget = false;
        targetMotion.SetBool("IsDefaultTarget", _isDefaultTarget);
    }
    public void TargetHitHP()
    {
        GameObject shotControl;

        ShotController shotController;

        shotControl = GameObject.Find("ShotControl");

        shotController = shotControl.GetComponent<ShotController>();
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