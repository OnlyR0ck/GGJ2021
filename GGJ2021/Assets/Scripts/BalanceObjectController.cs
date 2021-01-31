using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BalanceObjectController : MonoBehaviour
{
    private TextMeshProUGUI _balanceText;
    private static double _balance;
    private string _balanceSave = String.Empty;
    private GameManager gameManager;
    public double GetBalance()
    {
        return _balance;
    }

    public void ChangeBalance(double term)
    {
        _balance += term;
        ChangeBalanceText();
        SaveProgress();
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadProgress();
        _balanceText = GetComponent<TextMeshProUGUI>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (_balance != 0) ChangeBalanceText();
    }

    void SaveProgress()
    {
        _balanceSave = Convert.ToString(_balance);
        PlayerPrefs.SetString("Balance", _balanceSave);
    }

    void LoadProgress()
    {
        _balanceSave = PlayerPrefs.GetString("Balance", "0");
        _balance = Convert.ToDouble(_balanceSave);
    }

    void ChangeBalanceText()
    {
        _balanceText.text = gameManager.ConvertBigNumber(_balance);
    }
}
