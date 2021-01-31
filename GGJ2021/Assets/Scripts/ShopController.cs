using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopController : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void OnShopEnter()
    {
        gameObject.SetActive(true);
    }

    public void OnShopExit()
    {
        gameObject.SetActive(false);
    }
    
}
