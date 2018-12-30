using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TargetController : MonoBehaviour
{
    // HeadMarkerのTargetController.csが反応した時にも、
    // Targetのアニメーションが再生されるようにTargetのAnimatorを取得。
    [SerializeField] Animator targetMotion;
    private const int TARGET_HP_EMPTY = 0;
    private const int START_REVIVE_MOTION_INTERVAL = 2;
    private const int DEFAULT_MOTION_TRUE_INTERVAL = 1;
    private const float END_REVIVE_MOTION_INTERVAL = 0.5f;
    public const int TARGET_HP_FULL = 5;
    public static int _targetHP = 5;
    private bool _isCrushTarget;
    private bool _isReviveTarget;
    private bool _isDefaultTarget;
    public bool _hitHeadMarker;
    public Vector3 hitPosition;

    // Use this for initialization
    public void CrushTargetMotion()
    {
        if (_targetHP != TARGET_HP_EMPTY)
        {
            return;
        }
        _isCrushTarget = true;
        _isReviveTarget = true;
        targetMotion.SetBool("IsCrushTarget", _isCrushTarget);

        StartCoroutine(ReviveTargetMotion());
    }
    public IEnumerator ReviveTargetMotion()
    {
        if (_isReviveTarget)
        {
            yield return new WaitForSeconds(START_REVIVE_MOTION_INTERVAL);
            targetMotion.SetBool("IsReviveTarget", _isReviveTarget);
            // IsReviveTargetのアニメが終わるまでのインターバル設定。
            yield return new WaitForSeconds(END_REVIVE_MOTION_INTERVAL);

            StartCoroutine(StopTargetMotion());

            RecoverTargetHP();

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
    public void HitTarget(Collider hitObjCollider)
    {
        _targetHP--;
        print(_targetHP);
    }
    public void RecoverTargetHP()
    {
        if (_targetHP != TARGET_HP_EMPTY)
        {
            return;
        }
        _targetHP = TARGET_HP_FULL;
        print(_targetHP + "HP回復");
    }
    public void HitHeadMarker(Vector3 hitPos)
    {
        hitPosition = hitPos;
        _hitHeadMarker = true;
    }
}