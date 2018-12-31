using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ShotController : MonoBehaviour
{
    [SerializeField] private AudioClip shotSound;
    [SerializeField] private AudioClip reloadSound;
    [SerializeField] private int _bulletBox;
    [SerializeField] private int _bullet;
    [System.NonSerialized] public Vector3 hitObjPosition;
    public Ray shotRay;
    public RaycastHit gunShotHit;
    public Collider hitObjCollider;
    private GameObject shotEffect;
    private GameObject shotReachEffect;
    private float shotInterval;
    private float reloadInterval;
    private AudioSource gunAudioSource;
    private const int RELOAD_BORDER_TIME = 2;
    private const float SHOT_BORDER_TIME = 0.5f;
    private const int BULLET_STOCK_FULL = 30;

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

        gunAudioSource.PlayOneShot(shotSound);
        shotRay = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(shotRay, out gunShotHit))
        {
            hitObjPosition = gunShotHit.point;
            hitObjCollider = gunShotHit.collider;

            Instantiate(shotEffect, this.transform.position, Quaternion.identity);
            Instantiate(shotReachEffect, hitObjPosition, Quaternion.identity);

            TargetController targetController = hitObjCollider.gameObject.GetComponent<TargetController>();

            if (hitObjCollider.gameObject.tag == "HeadMarker")
            {
                targetController = hitObjCollider.gameObject.transform.parent.GetComponent<TargetController>();
                targetController.HitHeadMarker(hitObjPosition);
                targetController.HitTarget();
            }
            else if (targetController != null)
            {
                targetController.HitTarget();
            }
        }
    }

        private void ReloadBullet()
        {
            if (_bulletBox == 0) return;
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
                }
            }
        }
    }