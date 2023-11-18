using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin_Score : MonoBehaviour
{
    void Update()
    {
         GetComponent<Text>().text = All_Upgrades.Coins.ToString();
    }
}
