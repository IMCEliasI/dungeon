using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public GameObject player;

    private float damage = 15f;
    private void Start()
    {
        if (All_Upgrades.Hardmode) { damage *= 1.5f; }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!collider.gameObject.CompareTag("Bullet") && !collider.gameObject.CompareTag("Striker") && !collider.gameObject.CompareTag("Rope"))
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                player.GetComponent<Control>().Get_Damage();
                player.GetComponent<Control>().HP -= damage;
            }
            Destroy(gameObject);
        }
    }
}
