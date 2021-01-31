using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopController : MonoBehaviour
{
    private GameManager _gameManager;
    public static event Action shopAction;
    private void Start()
    {
        /*gameObject.SetActive(false);
        shopAction?.Invoke();*/
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    public void OnShopEnter()
    {
        gameObject.SetActive(true);
        shopAction?.Invoke();
    }

    public void OnShopExit()
    {
        gameObject.SetActive(false);
        shopAction?.Invoke();
    }
    
}
