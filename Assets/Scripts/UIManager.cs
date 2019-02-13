using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text timeLimtText;
    [SerializeField] private Text bulletText;
    [SerializeField] private Text bulletBoxText;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text hpText;
    
    public void UpdateText(float _timeLimit,float _score, float _bulletBox, float _bullet, float _bulletStockFirst,int _playerHP)
    {
        timeLimtText.text = "Time：" + _timeLimit.ToString("f1") + "s";
        scoreText.text = "Pt：" + _score.ToString("f1");
        bulletBoxText.text = "Bullet Box：" + _bulletBox;
        bulletText.text = "Bullet：" + _bullet + "/" + _bulletStockFirst;
        hpText.text = "HP" + _playerHP;
    }
}