﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShotController : Photon.MonoBehaviour
{
    [SerializeField] private AudioClip shotSound;
    [SerializeField] private AudioClip reloadSound;
    private AudioSource gunAudioSource;
    private GameObject shotEffect;
    private GameObject shotReachEffect;

    public int _bulletBox;
    public int _bullet;
    private const int BULLET_STOCK_FULL = 30;
    private float shotInterval;
    private float reloadInterval;
    private const int RELOAD_BORDER_TIME = 2;
    private const float SHOT_BORDER_TIME = 0.5f;

    [SerializeField] private Image scopeImage;
    private bool _snipeMode;
    private const int ZOOM_IN_SCOPE = 20;
    private const int ZOOM_OUT_SCOPE = 60;


    void Start()
    {
        shotEffect = Resources.Load<GameObject>("Effects/ShotEffect");
        shotReachEffect = Resources.Load<GameObject>("Effects/ShotReachEffect");
        gunAudioSource = GetComponent<AudioSource>();

        scopeImage = GameObject.FindGameObjectWithTag("SnipeImage").GetComponent<Image>();
        scopeImage.gameObject.SetActive(false);
    }

    void Update()
    {
        shotInterval += Time.deltaTime;
        reloadInterval += Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            Shot();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadBullet();
        }
        if (Input.GetMouseButtonDown(1))
        {
            ZoomScope();
        }
    }

    private void Shot()
    {
        if (shotInterval < SHOT_BORDER_TIME) return;

        if (reloadInterval < RELOAD_BORDER_TIME) return;

        if (_bullet <= 0) return;

        _bullet -= 1;
        shotInterval = 0;
        gunAudioSource.PlayOneShot(shotSound);

        RaycastHit hitRay;
        Ray shotRay = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        if (Physics.Raycast(shotRay, out hitRay))
        {
            Vector3 hitObjPosition = hitRay.point;
            Collider hitObjCollider = hitRay.collider;
            TargetController targetController = hitObjCollider.gameObject.GetComponent<TargetController>();

            ShotEffect(hitObjPosition);
            
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

            if(hitObjCollider.gameObject.tag == "Player")
            {
                hitObjCollider.gameObject.GetPhotonView().RPC("HitPlayer",PhotonTargets.All);
            }
        }
    }

    private void ReloadBullet()
    {
        if (_bulletBox == 0) return;

        if (shotInterval < SHOT_BORDER_TIME) return;

        if (reloadInterval < RELOAD_BORDER_TIME) return;

        if (_bullet >= BULLET_STOCK_FULL) return;

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

    private void ZoomScope()
    {
        if (!_snipeMode)
        {
            Camera.main.fieldOfView = ZOOM_IN_SCOPE;
            scopeImage.gameObject.SetActive(true);
            _snipeMode = true;
        }
        else
        {
            Camera.main.fieldOfView = ZOOM_OUT_SCOPE;
            scopeImage.gameObject.SetActive(false);
            _snipeMode = false;
        }
    }

    private void ShotEffect(Vector3 hitObjPosition)
    {
        PhotonNetwork.Instantiate("shotEffect",this.transform.position, Quaternion.identity,0);
        PhotonNetwork.Instantiate("shotReachEffect", hitObjPosition, Quaternion.identity,0);
    } 
}