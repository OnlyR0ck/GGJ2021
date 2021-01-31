using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreController : MonoBehaviour
{
    private GameManager gameManagerScript;
    public delegate void Action();
    private CurrentHealthController _currentHealth;
    public static event Action oreDestroyed;
    private void Awake()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        _currentHealth = GameObject.Find("CurrentHealth").GetComponent<CurrentHealthController>();
    }
    private Vector3 _currentPosition;
    private void OnMouseDown()
    {
        Debug.Log("Clicked\n");
        _currentHealth.ChangeCurrentHealth(gameManagerScript.GetManualDamage());
    }
    // Update is called once per frame
    void Update()
    {
        if (transform.position.x != 0)
        {
            _currentPosition = transform.position;
            _currentPosition.x--;
            transform.position = _currentPosition;
        }
    }
}
