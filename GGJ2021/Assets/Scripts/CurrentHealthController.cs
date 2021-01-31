using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrentHealthController : MonoBehaviour
{
    private double _currentHealth;
    private TextMeshProUGUI _currentHealthText;
    public delegate void Action();
    private GameManager gameManager;
    public static event Action oreDestroyed;
    public void SetCurrentHealth(double health)
    {
        _currentHealth = health;
        UpdateCurrentHealthText();
    }
    void Awake()
    {
        _currentHealthText = GetComponent<TextMeshProUGUI>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        LoadProgress();
        UpdateCurrentHealthText();
    }
    void UpdateCurrentHealthText()
    {
        _currentHealthText.text = gameManager.ConvertBigNumber(_currentHealth);
    }
    public void ChangeCurrentHealth(double term)
    {
        _currentHealth -= term;
        SaveProgress();
        HPBarController.ChangeHealth(term);
        if (_currentHealth <= 0)
        {
            oreDestroyed?.Invoke();
        }
        else
        { 
            UpdateCurrentHealthText();
        }
    }

    void SaveProgress()
    {
        PlayerPrefs.SetString("_currentHealth", Convert.ToString(_currentHealth));
    }

    void LoadProgress()
    {
        _currentHealth = Convert.ToDouble(PlayerPrefs.GetString("currentHealth", _currentHealth.ToString()));
    }
}