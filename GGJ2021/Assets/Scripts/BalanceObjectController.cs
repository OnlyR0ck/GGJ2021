using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BalanceObjectController : MonoBehaviour
{
    private TextMeshProUGUI _balanceText;
    private static double _balance;

    public double GetBalance()
    {
        return _balance;
    }

    public void ChangeBalance(double term)
    {
        _balance += term;
        _balanceText.text = $"Pott:{term}";

    }

    // Start is called before the first frame update
    void Start()
    {
        _balanceText = GetComponent<TextMeshProUGUI>();
    }
}
