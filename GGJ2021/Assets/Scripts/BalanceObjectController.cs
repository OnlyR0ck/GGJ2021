using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BalanceObjectController : MonoBehaviour
{
    private TextMeshProUGUI _balanceText;
    private static double _balance;
    private GameManager gameManager;
    public double GetBalance()
    {
        return _balance;
    }

    public void ChangeBalance(double term)
    {
        _balance += term;
        _balanceText.text = gameManager.ConvertBigNumber(_balance);
    }

    // Start is called before the first frame update
    void Start()
    {
        _balanceText = GetComponent<TextMeshProUGUI>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
}
