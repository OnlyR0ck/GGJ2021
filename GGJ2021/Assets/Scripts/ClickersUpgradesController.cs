using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

public class ClickersUpgradesController : MonoBehaviour

{
    [SerializeField] private double _cost;
    [SerializeField] private double _dpsCount = 1;
    [SerializeField] private int _currentClickerLvl = 0;
    private BalanceObjectController _balanceScript;
    private DPSObjectController _dpsScript;

    
    public delegate void Action(double dmg);
    public static event Action Upgrade;

    public void OnAutoClickerUpgrade()
    {
        if (_balanceScript.GetBalance() >= _cost)
        {
            _balanceScript.ChangeBalance(-_cost);
            _dpsScript.ChangeDPS(_dpsCount);
            ChangeAutoClickerParams();
        }
    }

    private void ChangeManualClickerParams()
    {
        _currentClickerLvl++;
        _dpsCount = _dpsCount * Mathf.Exp(_currentClickerLvl / 20f);
        _cost = _cost + _cost * Math.Exp(_currentClickerLvl) + Math.Pow(-1, Random.Range(0, 2)) * _cost / 10f;
        Upgrade?.Invoke(_dpsCount);

    }

    private void ChangeAutoClickerParams()
    {
        _currentClickerLvl++;
        _dpsCount = _dpsCount + _dpsCount * _currentClickerLvl / 20f;
        _cost = _cost + _cost * Math.Exp(_currentClickerLvl) + Math.Pow(-1, Random.Range(0, 2)) * _cost / 10;
    }
// Start is called before the first frame update
    void Start()
    {
        _balanceScript = GameObject.Find("BalanceObject").GetComponent<BalanceObjectController>();
        _dpsScript = GameObject.Find("DPSObject").GetComponent<DPSObjectController>();
    }

    public void OnTapUpgrade()
    {
        if (_balanceScript.GetBalance() >= _cost)
        {
            _balanceScript.ChangeBalance(-_cost);
            _dpsScript.ChangeDPS(_dpsCount);
            ChangeManualClickerParams();
        }
    }
}
