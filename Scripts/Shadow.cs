using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    public float Current_a = 0;
    private float invis = 1;

    private void Start()
    {
        invis = Current_a / 255;
    }
    public void Reset()
    {
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Current_a / 255);
        invis = Current_a / 255;
        gameObject.SetActive(false);
    }
    void Update()
    {
        if (gameObject.activeSelf)
        {
            if (!(GetComponent<SpriteRenderer>().color.a <= 0))
            {
                invis -= Time.deltaTime * 0.75f;
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, invis);
            }
            else
            {
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Current_a/255);
                invis = Current_a/255;
                gameObject.SetActive(false);
            }
        }
    }
}
