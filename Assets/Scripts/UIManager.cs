using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [SerializeField] private Text timeText;
    [SerializeField] private Text bulletText;
    [SerializeField] private Text bulletBoxText;
    [SerializeField] private Text scoreText;
    [SerializeField] private ScoreController scoreController;
    [SerializeField] private ShotController shotController;
    [SerializeField] private GameManager gameManager;
    private int _bulletStockFirst;
    // Use this for initialization

    // Update is called once per frame
    void Start()
    {
        _bulletStockFirst = shotController._bullet;
    }

    void Update()
    {
        timeText.text = "Time：" + gameManager._timeLimit + "s";
        scoreText.text = "Pt：" + scoreController._targetScore;
        bulletBoxText.text = "Bullet Box：" + shotController._bulletBox;
        bulletText.text = "Bullet：" + shotController._bullet + "/" + _bulletStockFirst;
    }
}
