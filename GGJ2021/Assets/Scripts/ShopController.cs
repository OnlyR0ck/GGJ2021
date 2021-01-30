using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopController : MonoBehaviour
{

    public void OnShopEnter()
    {
        gameObject.SetActive(true);
    }

    public void OnShopExit()
    {
        gameObject.SetActive(false);
    }
    
}
