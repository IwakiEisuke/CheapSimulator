using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] Text _text;

    static float _score, _onClickScoreAmount;
    static float _base;
    static List<Upgrade> _upgrades;
    
    private void Start()
    {
        _score = 0;
        _onClickScoreAmount = 1;
        _base = 1;
        _upgrades = new List<Upgrade>();
    }

    public static void AddScoreOnClick()
    {
        _score += _onClickScoreAmount;
    }

    public static void BuyUpgrade(Upgrade newUpgrade)
    {
        _upgrades.Add(newUpgrade);
        var newAmount = _base;
        foreach(var upgrade in _upgrades)
        {
            upgrade.Ammm(ref newAmount);
        }
        _onClickScoreAmount = newAmount;
    }

    public static void BuySkill()
    {

    }

    private void Update()
    {
        _text.text = _score.ToString("F0");
    }
}
