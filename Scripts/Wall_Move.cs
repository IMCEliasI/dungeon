using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall_Move : MonoBehaviour
{
    public GameObject Player;
    void Update()
    {
        if (Player.transform.position.x - transform.position.x >= 40)
        {
            transform.position = Player.transform.position + new Vector3(-40, 0);
        }
    }
}
