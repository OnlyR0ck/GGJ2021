using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MaxHealthController : MonoBehaviour
{
    private double _maxHealth;
    private TextMeshProUGUI _textMesh;
    private GameManager gameManager;
    public void MaxHealthSet(double t)
    {
        _maxHealth = t;
        _textMesh.text = '/' + gameManager.ConvertBigNumber(t);
    }
    public double GetMaxHealth()
    {
        return _maxHealth;
    }
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _textMesh = GetComponent<TextMeshProUGUI>();
        _textMesh.text = "\\ 0";
    }
}
