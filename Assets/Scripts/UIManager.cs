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
    public void UpdateText(float _timeLimit,float _targetScore, float _bulletBox, float _bullet, float _bulletStockFirst)
    {
        timeText.text = "Time：" + _timeLimit.ToString("f1") + "s";
        scoreText.text = "Pt：" + _targetScore.ToString("f1");
        bulletBoxText.text = "Bullet Box：" + _bulletBox;
        bulletText.text = "Bullet：" + _bullet + "/" + _bulletStockFirst;
    }
}