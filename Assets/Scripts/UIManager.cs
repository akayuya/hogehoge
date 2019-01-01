using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [SerializeField] public Text timeText;
    [SerializeField] public Text bulletText;
    [SerializeField] public Text bulletBoxText;
    [SerializeField] public Text scoreText;
    // Use this for initialization
    public void IndicateText(float _timeLimit,float _targetScore, float _bulletBox, float _bullet, float _bulletStockFirst)
    {
        timeText.text = "Time：" + _timeLimit.ToString("f1") + "s";
        scoreText.text = "Pt：" + _targetScore.ToString("f1");
        bulletBoxText.text = "Bullet Box：" + _bulletBox;
        bulletText.text = "Bullet：" + _bullet + "/" + _bulletStockFirst;
    }
}
