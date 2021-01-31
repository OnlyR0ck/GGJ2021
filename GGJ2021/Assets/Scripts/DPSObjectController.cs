using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DPSObjectController : MonoBehaviour
{
    private double _dps;
    private double _dPFU;
    private int i = 0;
    private TextMeshProUGUI _dpsText;
    private CurrentHealthController _currentHealth;
    private GameManager gameManager;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _currentHealth = GameObject.Find("CurrentHealth").GetComponent<CurrentHealthController>();
        _dpsText = GetComponent<TextMeshProUGUI>();
    }
    public void ChangeDPS(double term)
    {
        _dps += term;
        _dPFU = _dps / 50f;
        _dpsText.text = gameManager.ConvertBigNumber(_dps);
    }
    private void FixedUpdate()
    {
        i++;
        double temp = i * _dPFU;
        if (temp >= 1)
        {
            _currentHealth.ChangeCurrentHealth(i * _dPFU);
            i = 0;
        }
    }
}
