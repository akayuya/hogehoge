using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD
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
=======
public class TargetController : MonoBehaviour {
>>>>>>> parent of 59554be... アニメーションの設定、回復時間の設定


	public int _targetHP;
	public float _targetScore;

	

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	
}
