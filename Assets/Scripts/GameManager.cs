using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    [SerializeField] ScoreController scoreController;
    [SerializeField] TargetController targetController;
    [SerializeField] TargetController headMarker_targetController;
    public Vector3 headMarkerCenter;
    // Use this for initialization
    void Start()
    {
        // HeadMarkerのColliderがTargetのColliderと分かれているためか
        // targetController._hitHeadMarkerだとGameManagerのtargetController._hitHeadMarkerがtrueにならないため
        // HeadMarkerにもtargetControllerを設定。
        headMarkerCenter = headMarker_targetController.GetComponent<BoxCollider>().bounds.center;
    }
    // Update is called once per frame
    void Update()
    {
        if (headMarker_targetController._hitHeadMarker)
        {
            scoreController.CalcScore(headMarkerCenter, headMarker_targetController.hitPosition);
            headMarker_targetController._hitHeadMarker = false;
        }
    }
}
