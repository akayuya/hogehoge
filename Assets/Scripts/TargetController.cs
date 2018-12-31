using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TargetController : MonoBehaviour
{
    [SerializeField] Animator targetMotion;
    private const int END_REVIVE_MOTION_INTERVAL = 5;
    private const int TARGET_HP_FULL = 5;
    private int _targetHP = 5;
    private bool _isCrushTarget;
    public bool _hitHeadMarker;
    public Vector3 hitPosition;

    // Use this for initialization
    private void Dead()
    {
        if (_targetHP != 0)
        {
            return;
        }
        _isCrushTarget = true;
        targetMotion.SetBool("IsCrushTarget", _isCrushTarget);

        StartCoroutine(Revive());
    }
    //TargetがCrushMotionを繰り返さないようにモーション終了後は_isCrushTargetをfalseに。 　
    private IEnumerator Revive()
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
        if (_targetHP > 0)
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
        if (_targetHP == 0)
        {
            Dead();
        }
    }
    public void HitHeadMarker(Vector3 hitPos)
    {
        hitPosition = hitPos;
        _hitHeadMarker = true;
    }
}
