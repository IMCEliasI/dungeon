using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slider_Connect : MonoBehaviour
{
    public Scrollbar slider;
    void Update()
    {
        GetComponent<Text>().text = (Mathf.RoundToInt(slider.GetComponent<Scrollbar>().value * 100)).ToString() + "%";
        All_Upgrades.volume = slider.GetComponent<Scrollbar>().value; 
    }
}
