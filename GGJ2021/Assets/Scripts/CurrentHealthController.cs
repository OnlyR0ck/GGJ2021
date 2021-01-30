using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrentHealthController : MonoBehaviour
{
    private double _currentHealth;
    private TextMeshProUGUI _currentHealthText;
    
    public delegate void Action();
    public static event Action oreDestroyed;

    public void SetCurrentHealth(double health)
    {
        _currentHealth = health;
        UpdateCurrentHealthText();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _currentHealthText = GetComponent<TextMeshProUGUI>();
    }

    void UpdateCurrentHealthText()
    {
        _currentHealthText.text = $"{_currentHealth}";
    }

    public void ChangeCurrentHealth(double term)
    {
        if (_currentHealth <= 0)
        {
            oreDestroyed?.Invoke();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
