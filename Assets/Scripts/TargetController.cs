using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TargetController : MonoBehaviour
{
    // HeadMarkerのTargetController.csが反応した時にも、
    // Targetのアニメーションが再生されるようにTargetのAnimatorを取得。
    [SerializeField] Animator targetMotion;
    private const int TARGET_HP_EMPTY = 0;
    private const int END_REVIVE_MOTION_INTERVAL = 5;
    private const int TARGET_HP_FULL = 5;
    private static int _targetHP = 5;
    private bool _isCrushTarget;
    public bool _hitHeadMarker;
    public Vector3 hitPosition;

    // Use this for initialization

    private void Update()
    {
        if (_targetHP == TARGET_HP_EMPTY)
        {
            CrushTargetMotion();
        }
    }
    private void CrushTargetMotion()
    {
        if (_targetHP != TARGET_HP_EMPTY)
        {
            return;
        }
        _isCrushTarget = true;
        targetMotion.SetBool("IsCrushTarget", _isCrushTarget);

        StartCoroutine(ReviveTargetMotion());
    }
    private IEnumerator ReviveTargetMotion()
    {
        if (!_isCrushTarget)
        {
            yield break;
        }

        yield return new WaitForSeconds(END_REVIVE_MOTION_INTERVAL);

        _isCrushTarget = false;
        targetMotion.SetBool("IsCrushTarget", _isCrushTarget);

        RecoverTargetHP();
    }
    private void RecoverTargetHP()
    {
        if (_targetHP > TARGET_HP_EMPTY)
        {
            return;
        }
        _targetHP = TARGET_HP_FULL;
        print(_targetHP + "HP回復");
    }
    public void HitTarget()
    {
        _targetHP--;
        print(_targetHP);
    }
    public void HitHeadMarker(Vector3 hitPos)
    {
        hitPosition = hitPos;
        _hitHeadMarker = true;
    }
}