using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarController : MonoBehaviour
{
    public static Slider _sliderController;
    public static float health = 1;
    public static float damage = 1;
    
    // Start is called before the first frame update

    void OnEnable()
    {
        OreController.oreHit += HitReceived;
    }

    void OnDisable()
    {
        OreController.oreHit -= HitReceived;
    }
    void Start()
    {
        _sliderController = GetComponent<Slider>();
    }

    public void Refill()
    {
        _sliderController.maxValue = health;
    }

    void HitReceived()
    {
        _sliderController.value -= damage;
    }

    // Update is called once per frame
}
