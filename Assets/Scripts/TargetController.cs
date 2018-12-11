using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TargetController : MonoBehaviour
{
    private Animator targetMotion;
    private const int TARGET_HP_EMPTY = 0;
    private const int REVIVE_MOTION_INTERVAL = 3;
    private const float DEFAULT_MOTION_INTERVAL = 1;

    private bool _isCrushTarget;
    private bool _isReviveTarget;
    private bool _isDefaultTarget;
    GameObject hpControl;

    HPController hpController;

    // Use this for initialization
    void Start()
    {
        hpControl = GameObject.Find("HPControl");
        hpController = hpControl.GetComponent<HPController>();
        targetMotion = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void TargetCrashMotion()
    {
        if (hpController._targetHP != TARGET_HP_EMPTY)
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

            yield return new WaitForSeconds(REVIVE_MOTION_INTERVAL);
            targetMotion.SetBool("IsReviveTarget", _isReviveTarget);
            // IsReviveTargetのアニメが終わるまでのインターバル設定。
            yield return new WaitForSeconds(REVIVE_MOTION_INTERVAL);

            hpController.TargetHPFull();

            StartCoroutine(StopTargetMotion());

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

        yield return new WaitForSeconds(DEFAULT_MOTION_INTERVAL);

        _isReviveTarget = false;
        targetMotion.SetBool("IsReviveTarget", _isReviveTarget);
        _isDefaultTarget = false;
        targetMotion.SetBool("IsDefaultTarget", _isDefaultTarget);

    }
}