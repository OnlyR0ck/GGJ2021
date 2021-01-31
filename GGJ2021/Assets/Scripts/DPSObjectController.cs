using System;
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
        LoadProgress();
        UpdateDpsText();
    }

    private void OnEnable()
    {
        ShopController.shopAction += Hide;
    }

    private void OnDisable()
    {
        ShopController.shopAction -= Hide;
    }

    void Hide()
    {
        transform.parent.position = -transform.parent.position;
    }

    public void ChangeDPS(double term)
    {
        _dps += term;
        _dPFU = _dps / 50f;
        SaveProgress();
        UpdateDpsText();
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

    void SaveProgress()
    {
        PlayerPrefs.SetString("_dps", _dps.ToString());
        PlayerPrefs.SetString("_dPFU", _dPFU.ToString());
    }

    void LoadProgress()
    {
        _dps = Convert.ToDouble(PlayerPrefs.GetString("_dps", _dps.ToString()));
        _dPFU = Convert.ToDouble(PlayerPrefs.GetString("_dPFU", _dPFU.ToString()));
    }

    void UpdateDpsText()
    {
        _dpsText.text = gameManager.ConvertBigNumber(_dps);
    }
}
