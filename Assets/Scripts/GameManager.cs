using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject ScoreControl;
    [SerializeField] GameObject target;
    private ScoreController scoreController;
    private TargetController targetController;
	private TargetController headMarkerController;
	[SerializeField] GameObject headMarker;
	public Vector3 headMarkerCenter;
    // Use this for initialization
    void Start()
    {
        scoreController = ScoreControl.GetComponent<ScoreController>();
        targetController = target.GetComponent<TargetController>();
		headMarkerController = headMarker.GetComponent<TargetController>();
		headMarkerCenter = headMarker.GetComponent<BoxCollider>().bounds.center;
    }
    // Update is called once per frame
    void Update()
    {
        if (headMarkerController._hitHeadMarker)
        {
            scoreController.CalcScore(headMarkerCenter, headMarkerController.hitPosition);
			headMarkerController._hitHeadMarker = false;
        }
    }
}
