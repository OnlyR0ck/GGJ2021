using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DPSObjectController : MonoBehaviour
{
    private double _dps;
    private TextMeshProUGUI _dpsText;

    public void ChangeDPS(double term)
    {
        _dps += term;
        _dpsText.text = $"{_dps}";
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _dpsText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
