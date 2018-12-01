using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour
{

    [SerializeField] private AudioClip shotSound;
    [SerializeField] private AudioClip reloadSound;
    [SerializeField] private int _bulletBox;
    [SerializeField] private int _bullet;

    private Ray shotRay;
    private RaycastHit gunShotHit;
    private GameObject shotEffect;
    private GameObject shotReachEffect;
    private Vector3 hitObjPosition;
    private float shotInterval;
    private float reloadInterval;

    private float hitObjPosY;
    private AudioSource gunAudioSource;

    private const int RELOAD_BORDER_TIME = 2;

    private const float SHOT_BORDER_TIME = 0.5f;

    private const int BULLET_STOCK_FULL = 30;

    private const int HIT_HEADMARKER_SCORE = 50;

    private const int HIT_TARGET_SCORE = 3;

    private TargetController targetController = new TargetController();



    // Use this for initialization
    void Start()
    {
        shotEffect = Resources.Load<GameObject>("Effects/ShotEffect");
        shotReachEffect = Resources.Load<GameObject>("Effects/ShotReachEffect");
        gunAudioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        shotInterval += Time.deltaTime;
        reloadInterval += Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            ShotGun();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadBullet();
        }

    }
    private void ShotGun()
    {

        if (shotInterval < SHOT_BORDER_TIME)
        {
            return;
        }

        if (reloadInterval < RELOAD_BORDER_TIME)
        {
            return;
        }

        if (_bullet <= 0)
        {
            return;
        }
        _bullet -= 1;
        shotInterval = 0;
        print(_bullet);

        gunAudioSource.PlayOneShot(shotSound);
        shotRay = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(shotRay, out gunShotHit))
        {
            hitObjPosition = gunShotHit.point;

            hitObjPosY = hitObjPosition.y;

            Debug.DrawLine(shotRay.origin, hitObjPosition, Color.black);

            Instantiate(shotEffect, this.transform.position, Quaternion.identity);
            Instantiate(shotReachEffect, hitObjPosition, Quaternion.identity);


            HitTargetScoreHp();


        }

    }


    private void ReloadBullet()
    {

        if (_bulletBox == 0)
        {
            return;
        }
        if (shotInterval < SHOT_BORDER_TIME)
        {
            return;
        }
        if (reloadInterval < RELOAD_BORDER_TIME)
        {
            return;
        }

        if (_bullet >= BULLET_STOCK_FULL)
        {
            return;
        }
        reloadInterval = 0;
        gunAudioSource.PlayOneShot(reloadSound);
        for (int i = 1; _bullet < BULLET_STOCK_FULL; ++i)
        {
            if (_bulletBox > 0)
            {
                _bullet += 1;
                _bulletBox -= 1;
                print(_bullet);
                print(_bulletBox);

            }
        }

    }

    private void HitTargetScoreHp()
    {

        if (gunShotHit.collider.gameObject.tag == "HeadMarker")
        {
            targetController._targetHP--;
            targetController._targetScore += HIT_HEADMARKER_SCORE;
            print(targetController._targetHP);
            print(targetController._targetScore);

        }

        if (gunShotHit.collider.gameObject.tag == "Target")
        {
            targetController._targetHP--;
            targetController._targetScore += hitObjPosY * HIT_TARGET_SCORE;
            print(targetController._targetHP);
            print(targetController._targetScore);

        }


    }

}
