using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreController : MonoBehaviour
{
    public double health;
    public double damage;
    public double score;
    public delegate void Action();

    public static event Action oreDestroyed;
    public static event Action oreHit;

    private Vector3 _currentPosition;

    private void OnEnable()
    {
        ClickersUpgradesController.Upgrade += ChangeManualDamage;
    }

    private void OnDisable()
    {
        ClickersUpgradesController.Upgrade -= ChangeManualDamage;
    }

    private void OnMouseDown()
    {
        health -= damage;
        oreHit?.Invoke();
        
        if (health <= 0)
        {
            this.gameObject.SetActive(false);
            oreDestroyed?.Invoke();
        }
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
    
    private void ChangeManualDamage(double term)
    {
        damage += term;
    }
}
