using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarController : MonoBehaviour
{
    public static Slider _sliderController;

    // Start is called before the first frame update

    void OnEnable()
    {
        _sliderController = GetComponent<Slider>();
    }

    public static void ChangeHealth(double term)
    {
        _sliderController.value -= (float) term;
    }
}
