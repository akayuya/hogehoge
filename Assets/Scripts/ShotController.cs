using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour
{

    [SerializeField] private AudioClip shotSound;
    [SerializeField] private AudioClip reroadSound;



    private Ray shotRay;
    private RaycastHit gunShotHit;
    private GameObject shotEffect;
    private GameObject shotReachEffect;
    private Vector3 hitObjPosition;
    private float shotInterval;
    private AudioSource gunAudioSource;

    private float bulletBox;

    private float bullet;




    // Use this for initialization
    void Start()
    {
        shotEffect = Resources.Load<GameObject>("Effects/ShotEffect");
        shotReachEffect = Resources.Load<GameObject>("Effects/ShotReachEffect");
        shotInterval = 0;
        gunAudioSource = gameObject.GetComponent<AudioSource>();

        bulletBox = 150f;
        bullet = 30f;

    }

    // Update is called once per frame
    void Update()
    {

        shotInterval += Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && shotInterval >= 0.5f && bullet > 0)
        {
            gunAudioSource.PlayOneShot(shotSound);
            shotRay = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            if (Physics.Raycast(shotRay, out gunShotHit))
            {
                hitObjPosition = gunShotHit.point;
                bullet -= 1;
                print(bullet);
            }

            Instantiate(shotEffect, this.transform.position, Quaternion.identity);
            Instantiate(shotReachEffect, hitObjPosition, Quaternion.identity);
            shotInterval = 0;
		}

        if (Input.GetKeyDown(KeyCode.R) && bullet < 30)
        {
            gunAudioSource.PlayOneShot(reroadSound);
            for (int i = 1; bullet < 30; ++i)
            {

                bullet += 1;
                bulletBox -= 1;
                print(bullet);
                print(bulletBox);
            }



            }

        
    }
}
