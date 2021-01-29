using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreController : MonoBehaviour
{
    public float health;
    public float damage;
    public float score;
    public delegate void Action();

    public static event Action oreDestroyed;

    private GameManager _gameManager;
    private Vector3 _currentPosition;


    private void OnMouseDown()
    {
        health -= damage;
        if (health <= 0)
        {
            this.gameObject.SetActive(false);
            oreDestroyed?.Invoke();
        }
    }

    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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
