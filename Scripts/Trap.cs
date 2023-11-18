using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    void Detect_Player()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, 12f, 1 << 3);

        if (hit)
        {
            gameObject.SetActive(false);
        }
    }
    void Start()
    {
        InvokeRepeating("Detect_Player",1f ,0.1f);
    }
}
