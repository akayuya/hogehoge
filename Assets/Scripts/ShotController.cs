using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShotController : MonoBehaviour
{
    [SerializeField] private AudioClip shotSound;
    [SerializeField] private AudioClip reloadSound;
    [SerializeField] public int _bulletBox;
    [SerializeField] public int _bullet;
    // [System.NonSerialized] public Vector3 hitObjPosition;
    // [System.NonSerialized] public Collider hitObjCollider;
    public RaycastHit hitTarget;
    private GameObject shotEffect;
    private GameObject shotReachEffect;
    private float shotInterval;
    private float reloadInterval;
    private bool _snipeMode;
    private AudioSource gunAudioSource;
    private const int RELOAD_BORDER_TIME = 2;
    private const float SHOT_BORDER_TIME = 0.5f;
    private const int BULLET_STOCK_FULL = 30;
    private const int ZOOM_IN_SCOPE = 20;
    private const int ZOOM_OUT_SCOPE = 60;
    [SerializeField] private Image scopeImage;
    // Use this for initialization
    void Start()
    {
        shotEffect = Resources.Load<GameObject>("Effects/ShotEffect");
        shotReachEffect = Resources.Load<GameObject>("Effects/ShotReachEffect");
        gunAudioSource = GetComponent<AudioSource>();
    }
    // Update is called once per frame
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

        Ray shotRay = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(shotRay, out hitTarget))
        {
            Vector3 hitObjPosition = hitTarget.point;
            Collider hitObjCollider = hitTarget.collider;

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
}