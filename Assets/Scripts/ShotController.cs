using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour
{

    [SerializeField] private AudioClip shotSound;
    [SerializeField] private AudioClip reroadSound;
    [SerializeField] private int _bulletBox;
    [SerializeField] private int _bullet;

    private Ray shotRay;
    private RaycastHit gunShotHit;
    private GameObject shotEffect;
    private GameObject shotReachEffect;
    private Vector3 hitObjPosition;
    private float shotInterval;
    private float reloadInterval;
    private bool _interval;
    private bool _isFulledBullet;
    private bool _emptyBullet;
    private AudioSource gunAudioSource;






    // Use this for initialization
    void Start()
    {
        shotEffect = Resources.Load<GameObject>("Effects/ShotEffect");
        shotReachEffect = Resources.Load<GameObject>("Effects/ShotReachEffect");
        shotInterval = 0;
        reloadInterval = 0;
        gunAudioSource = gameObject.GetComponent<AudioSource>();
        _isFulledBullet = true;
        _interval = true;

    }

    // Update is called once per frame
    void Update()
    {
        shotInterval += Time.deltaTime;
        reloadInterval += Time.deltaTime;

        if (shotInterval >= 0.5f && reloadInterval >= 2f)
        {
            _interval = true;
        }






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





        if (_interval == _emptyBullet)
        {


            _bullet -= 1;

            gunAudioSource.PlayOneShot(shotSound);
            shotRay = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            if (Physics.Raycast(shotRay, out gunShotHit))
            {
                hitObjPosition = gunShotHit.point;

                Instantiate(shotEffect, this.transform.position, Quaternion.identity);
                Instantiate(shotReachEffect, hitObjPosition, Quaternion.identity);

            }

            shotInterval = 0;
            print(_bullet);

            _interval = false;
            _isFulledBullet = false;

            if (_bullet == 0)
            {
                _emptyBullet = true;
            }


        }



    }
    private void ReloadBullet()
    {
        if (_isFulledBullet != _interval)
        {

            gunAudioSource.PlayOneShot(reroadSound);
            for (int i = 1; _isFulledBullet; ++i)
            {
                if (_bulletBox > 0)
                {
                    _bullet += 1;
                    _bulletBox -= 1;
                    print(_bullet);
                    print(_bulletBox);

                }
                else
                {
                    break;
                }

            }
            reloadInterval = 0;


            _interval = false;
            _isFulledBullet = true;


        }


    }

}
