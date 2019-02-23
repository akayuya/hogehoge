using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    [SerializeField] Animator targetMotion;

    public bool HasHitHeadMarker {get; set;}

    public Vector3 HitPosition {get; private set;}

    private bool _isDead;

    private const int END_REVIVE_MOTION_INTERVAL = 5;
    private const int TARGET_HP_FULL = 5;
    private int _targetHP = 5;

    private void DeadTarget()
    {
        if (_targetHP != 0) return;

        _isDead = true;
        targetMotion.SetBool("IsCrushTarget", _isDead);

        StartCoroutine(ReviveTarget());
    }
　
    private IEnumerator ReviveTarget()
    {
        if (!_isDead) yield break;

        yield return new WaitForSeconds(END_REVIVE_MOTION_INTERVAL);

        _isDead = false;
        targetMotion.SetBool("IsCrushTarget", _isDead);

        RecoverTargetHP();
    }

    private void RecoverTargetHP()
    {
        if (_targetHP > 0) return;

        _targetHP = TARGET_HP_FULL;
        print(_targetHP + "TargetHP回復");
    }

    public void HitTarget()
    {
        if (_isDead) return;

        _targetHP--;
        print("TargetHP" + _targetHP);
        if (_targetHP == 0)
        {
            DeadTarget();
        }
    }

    public void HitHeadMarker(Vector3 hitPos)
    {
        if (_isDead) return;

        HitPosition = hitPos;
        HasHitHeadMarker = true;
    }
}
