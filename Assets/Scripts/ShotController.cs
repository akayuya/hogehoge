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
    private float reroadInterval;
    private AudioSource gunAudioSource;






    // Use this for initialization
    void Start()
    {
        shotEffect = Resources.Load<GameObject>("Effects/ShotEffect");
        shotReachEffect = Resources.Load<GameObject>("Effects/ShotReachEffect");
        shotInterval = 0;
        reroadInterval = 1;
        gunAudioSource = gameObject.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        shotInterval += Time.deltaTime;
        reroadInterval += Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && shotInterval >= 0.5f && 
		reroadInterval >= 2f && _bullet > 0)
        {
            ShotGun();
        }

        if (Input.GetKeyDown(KeyCode.R) && _bullet < 30 && _bulletBox > 0
        && reroadInterval >= 2f && shotInterval >= 0.5f)
        {
            ReroadBullet();

        }


    }
    private void ShotGun()
    {

        _bullet -= 1;

        print(_bullet);

        gunAudioSource.PlayOneShot(shotSound);
        shotRay = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(shotRay, out gunShotHit))
        {
            hitObjPosition = gunShotHit.point;

            Instantiate(shotEffect, this.transform.position, Quaternion.identity);
            Instantiate(shotReachEffect, hitObjPosition, Quaternion.identity);

        }

        shotInterval = 0;

    }
    private void ReroadBullet()
    {

        gunAudioSource.PlayOneShot(reroadSound);
        for (int i = 1; _bullet < 30; ++i)
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
        reroadInterval = 0;

    }

}
