using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreController : MonoBehaviour
{
    public int health = 10;
    public int scoreToAdd = 10;
    public int damage = 1;

    private GameManager _gameManager;
    private Vector3 _currentPosition;


    private void OnMouseDown()
    {
        health -= damage;
    }

    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            this.gameObject.SetActive(false);
            health = 10;
            _gameManager.requireOreToSpawn = true;
        }

        if (transform.position.x != 0)
        {
            _currentPosition = transform.position;
            _currentPosition.x--;
            transform.position = _currentPosition;
        }
    }
}
