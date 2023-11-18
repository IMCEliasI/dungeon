using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hardmode : MonoBehaviour
{
    void Update()
    {
        All_Upgrades.Hardmode = GetComponent<Toggle>().isOn;
    }
}
