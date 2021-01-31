using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

public class ClickersUpgradesController : MonoBehaviour

{
    private double _cost;
    private double _oldCost = 0;
    [SerializeField] private double _oldDpsCount = 1;
    private double _dpsCount = 1;
    [SerializeField] private int _currentClickerLvl = 0;
    private BalanceObjectController _balanceScript;
    private DPSObjectController _dpsScript;
    private TextMeshProUGUI _textMesh;
    public delegate void Action(double dmg);
    public static event Action Upgrade;
    void Start()
    {
        _textMesh = GetComponentInChildren<TextMeshProUGUI>();
        UpdateCost(_oldCost);
        _balanceScript = GameObject.Find("BalanceObject").GetComponent<BalanceObjectController>();
        _dpsScript = GameObject.Find("DPSObject").GetComponent<DPSObjectController>();
    }
    public void OnAutoClickerUpgrade()
    {
        if (_balanceScript.GetBalance() >= _cost)
        {
            _currentClickerLvl++;
            _balanceScript.ChangeBalance(-_cost);
            _dpsScript.ChangeDPS(_dpsCount);
            ChangeAutoClickerParams();
        }
    }
    private void ChangeAutoClickerParams()
    {
        _oldDpsCount += _oldDpsCount * _currentClickerLvl / 20f;
        _oldCost += _oldCost * Math.Exp(_currentClickerLvl);
        _cost = _oldCost + _oldCost * Math.Pow(-1, Random.Range(0, 2)) / 10f;
        _dpsCount = _oldDpsCount + _oldDpsCount * Math.Pow(-1, Random.Range(0, 2)) / 10f;
        UpdateCost(_cost);
    }
    private void ChangeManualClickerParams()
    {
        _oldDpsCount = _oldDpsCount * Mathf.Exp(_currentClickerLvl / 20f);
        _oldCost += _oldCost * Math.Exp(_currentClickerLvl);
        _cost = _oldCost + _oldCost * Math.Pow(-1, Random.Range(0, 2)) / 10f;
        _dpsCount = _oldDpsCount + _oldDpsCount * Math.Pow(-1, Random.Range(0, 2)) / 10f;
        UpdateCost(_cost);
    }
    public void OnTapUpgrade()
    {
        if (_balanceScript.GetBalance() >= _cost)
        {
            _balanceScript.ChangeBalance(-_cost);
            _currentClickerLvl++;
            Upgrade?.Invoke(_dpsCount);
            ChangeManualClickerParams();
        }
    }
    private void UpdateCost(double _cost1)
    {
        _textMesh.text = $"{_cost1 : 0}";
    }
}