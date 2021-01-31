using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarController : MonoBehaviour
{
    public static Slider _sliderController;
    public static float health = 1;
    public static double Damage = 1;
    
    // Start is called before the first frame update

    void OnEnable()
    {
        _sliderController = GetComponent<Slider>();
    }
    void Start()
    {

    }

    public void Refill()
    {
        _sliderController.maxValue = health;
    }
}
