using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    public List<Sprite> Images;
    private int frame;
    private float timer = 0;
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            if (frame >= Images.Count) { frame = 0; }
            GetComponent<Image>().sprite = Images[frame];
            frame += 1;
            timer = 0.07f;
        }
    }
}
