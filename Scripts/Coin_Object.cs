using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin_Object : MonoBehaviour
{
    public GameObject local_coins;
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if(collision.gameObject.name == "Player")
        {
            Destroy(gameObject);
            All_Upgrades.Coins += 50;
            local_coins.GetComponent<Text>().text = (int.Parse(local_coins.GetComponent<Text>().text) + 50).ToString();
        }
    }
}
