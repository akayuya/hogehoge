using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    [SerializeField] ScoreController scoreController;
    [SerializeField] TargetController targetController;
    public Vector3 headMarkerCenter;
    public Vector3 parent;
    [SerializeField] GameObject target;
    AudioSource audio;

    // Use this for initialization
    void Start()
    {
        // HeadMarkerのColliderがTargetのColliderと分かれているためか
        // targetController._hitHeadMarkerだとGameManagerのtargetController._hitHeadMarkerがtrueにならないため
        // HeadMarkerにもtargetControllerを設定。
        headMarkerCenter = targetController.GetComponentInChildren<BoxCollider>().bounds.center;

        parent = targetController.GetComponent<BoxCollider>().bounds.center;
        audio = targetController.GetComponentInChildren<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        print(audio.gameObject.name);
        // targetController._hitHeadMarkerは
        print(targetController._hitHeadMarker);
        print(parent);
        print(headMarkerCenter);
        if (targetController._hitHeadMarker)
        {
            scoreController.CalcScore(headMarkerCenter,targetController.hitPosition);
            targetController._hitHeadMarker = false;
        }
    }
}
