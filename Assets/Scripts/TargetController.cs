using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TargetController : MonoBehaviour
{
    public static int _targetHP = 5;
    public static float _targetScore;
    private Animator targetMotion;
    private const int TARGET_HP_EMPTY = 0;
    private const int TARGET_HP_FULL = 5;
    private const int REVIVE_MOTION_INTERVAL = 10;
    private const int DEFAULT_MOTION_INTERVAL = 1;
    private bool _isCrushTarget;
    private bool _isReviveTarget;
    private bool _isDefaultTarget;

    // Use this for initialization
    void Start()
    {
        targetMotion = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_targetHP == TARGET_HP_EMPTY)
        {
            TargetCrashMotion(ref _isCrushTarget, ref _isReviveTarget);
            StartCoroutine(TargetReviveMotion(_isReviveTarget));
            print(_targetHP);
        }
        else
        {
            StopTargetMotion();
        }
    }

    private void TargetCrashMotion(ref bool _isCrushTarget, ref bool _isReviveTarget)
    {
        _isCrushTarget = true;
        _isReviveTarget = true;
        targetMotion.SetBool("IsCrushTarget", _isCrushTarget);
    }
    private IEnumerator TargetReviveMotion(bool _isReviveTarget)
    {
        if (_isReviveTarget)
        {
            _targetHP = TARGET_HP_FULL;
            yield return new WaitForSeconds(REVIVE_MOTION_INTERVAL);
            targetMotion.SetBool("IsReviveTarget", _isReviveTarget);
            yield return new WaitForSeconds(DEFAULT_MOTION_INTERVAL);
            _isDefaultTarget = true;
            targetMotion.SetBool("IsDefaultTarget", _isDefaultTarget);

            yield break;

        }
    }
    private void StopTargetMotion()
    {
        _isCrushTarget = false;
        targetMotion.SetBool("IsCrushTarget", _isCrushTarget);
        _isReviveTarget = false;
        targetMotion.SetBool("IsReviveTarget", _isReviveTarget);
        _isDefaultTarget = false;
        targetMotion.SetBool("IsDefaultTarget", _isDefaultTarget);
        
    }
}