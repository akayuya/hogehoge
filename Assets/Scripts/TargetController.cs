using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TargetController : MonoBehaviour
{
    [SerializeField] Animator targetMotion;
    private const int END_REVIVE_MOTION_INTERVAL = 5;
    private const int TARGET_HP_FULL = 5;
    private int _targetHP = 5;
    private bool _isDead;
    public bool _hitHeadMarker;
    public Vector3 hitPosition;

    // Use this for initialization
    private void Dead()
    {
        if (_targetHP != 0)　return;

        _isDead = true;
        targetMotion.SetBool("IsCrushTarget", _isDead);

        StartCoroutine(Revive());
    }
    //TargetがCrushMotionを繰り返さないようにモーション終了後は_isCrushTargetをfalseに。 　
    private IEnumerator Revive()
    {
        if (!_isDead)　yield break;

        yield return new WaitForSeconds(END_REVIVE_MOTION_INTERVAL);

        _isDead = false;
        targetMotion.SetBool("IsCrushTarget", _isDead);

        RecoverTargetHP();
    }
    private void RecoverTargetHP()
    {
        if (_targetHP > 0)　return;

        _targetHP = TARGET_HP_FULL;
        print(_targetHP + "HP回復");
    }
    public void HitTarget()
    {
        if (_isDead) return;

        _targetHP--;
        print(_targetHP);
        if (_targetHP == 0)
        {
            Dead();
        }
    }
    public void HitHeadMarker(Vector3 hitPos)
    {
        if (_isDead) return;
        
        hitPosition = hitPos;
        _hitHeadMarker = true;
    }
}
