using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unavailable : MonoBehaviour
{
    void Update()
    {
        if(GetComponent<Text>().color.a > 0)
        {
            GetComponent<Text>().color -= new Color(0, 0, 0, Time.deltaTime / 1.5f);
        }
    }
}
