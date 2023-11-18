using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsControl : MonoBehaviour
{
    public void StartAd()
    {
        if (gameObject.CompareTag("Player")) 
        {
            if (GetComponent<Control>().Start_of_game.activeSelf) { return; }
        }
        AudioListener.pause = true;
    }
    public void EndAd()
    {
        if (gameObject.CompareTag("Player")) { GetComponent<Control>().AD_reward(); }
    }
}
