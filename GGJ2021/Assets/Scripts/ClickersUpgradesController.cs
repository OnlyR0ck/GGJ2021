using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

public class ClickersUpgradesController : MonoBehaviour

{
    private double _cost;
    [SerializeField] private double _oldCost;
    [SerializeField] private double _oldDpsCount;
    private double _dpsCount;
    private int _currentClickerLvl;
    private BalanceObjectController _balanceScript;
    private DPSObjectController _dpsScript;
    private TextMeshProUGUI _textMesh;
    private string _prefabName;
    public delegate void Action(double dmg);
    private GameManager gameManager;
    public static event Action Upgrade;
    void Start()
    { 
        //PlayerPrefs.DeleteAll();
        _prefabName = this.gameObject.name;
        LoadProgress();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _textMesh = GetComponentInChildren<TextMeshProUGUI>();
        UpdateCost(_oldCost);
        _balanceScript = GameObject.Find("BalanceObject").GetComponent<BalanceObjectController>();
        _dpsScript = GameObject.Find("DPSObject").GetComponent<DPSObjectController>();
        _cost = _oldCost;
        _dpsCount = _oldDpsCount;
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
        SaveProgress();
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
            SaveProgress();
        }
    }
    private void UpdateCost(double _cost1)
    {
        _textMesh.text = gameManager.ConvertBigNumber(_cost1);
    }

    void SaveProgress()
    {
        PlayerPrefs.SetString($"{_prefabName} _cost", Convert.ToString(_cost));
        PlayerPrefs.SetString($"{_prefabName} _oldCost", Convert.ToString(_oldCost));
        PlayerPrefs.SetString($"{_prefabName} _dpsCount", Convert.ToString(_dpsCount));
        PlayerPrefs.SetString($"{_prefabName} _oldDpsCount", Convert.ToString(_oldDpsCount));
        PlayerPrefs.SetInt($"{_prefabName} _currentLvl",_currentClickerLvl);
    }

    void LoadProgress()
    {
        _cost = Convert.ToDouble(PlayerPrefs.GetString($"{_prefabName} _cost", _cost.ToString()));
        _oldCost = Convert.ToDouble(PlayerPrefs.GetString($"{_prefabName} _oldCost", _oldCost.ToString()));
        _dpsCount = Convert.ToDouble(PlayerPrefs.GetString($"{_prefabName} _dpsCount", _dpsCount.ToString()));
        _oldDpsCount = Convert.ToDouble(PlayerPrefs.GetString($"{_prefabName} _oldDpsCount", _oldDpsCount.ToString()));
        _currentClickerLvl = PlayerPrefs.GetInt($"{_prefabName} _currentLvl", 0);
    }
}