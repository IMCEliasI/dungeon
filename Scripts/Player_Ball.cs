using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Ball : MonoBehaviour
{
    private float damage = 10f * All_Upgrades.Damage_modificator;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!collider.gameObject.CompareTag("Bullet") && !collider.gameObject.CompareTag("Player") && !collider.gameObject.CompareTag("Rope"))
        {
            if (collider.gameObject.name == "Walker")
            {
                collider.gameObject.GetComponent<Walker>().HP -= damage;
                collider.gameObject.GetComponent<Walker>().Get_Damaged();
            }
            if (collider.gameObject.name == "Striker")
            {
                collider.gameObject.GetComponent<Striker>().HP -= damage;
                collider.gameObject.GetComponent<Striker>().Get_Damaged();
            }
            if (collider.gameObject.name == "Barrel")
            {
                collider.gameObject.GetComponent<Barrel>().Get_Damaged();
            }
            if (collider.gameObject.name == "Slime")
            {
                collider.gameObject.GetComponent<Slime>().HP -= damage;
            }
            Destroy(gameObject);
        }
    }
}
