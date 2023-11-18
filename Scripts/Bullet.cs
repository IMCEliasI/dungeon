using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject player;
    private float LifeTime = 0f;
    private float TimeFall = 2f;

    private bool fell = false;
    private float fellTimeLife = 0f;

    private float damage = 10f;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(!collider.gameObject.CompareTag("Bullet") && !collider.gameObject.CompareTag("Striker") && !collider.gameObject.CompareTag("Rope"))
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                player.GetComponent<Control>().Get_Damage();
                player.GetComponent<Control>().HP -= damage;

                if(GetComponent<CircleCollider2D>().enabled == true){ Destroy(gameObject);  }
            }
            else
            {
                GetComponent<Animator>().SetLayerWeight(0, 0);
                GetComponent<Animator>().SetLayerWeight(1, 1f);
                GetComponent<Animator>().PlayInFixedTime("Bullet_Burst", 1, 0);
                transform.rotation = Quaternion.Euler(0,0,0);
                GetComponent<Rigidbody2D>().velocity = new();

                fell = true;
                fellTimeLife = 7.5f;

                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

                GetComponent<CircleCollider2D>().enabled = false;
                GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }
    private void Start()
    {
        if (All_Upgrades.Hardmode) { damage *= 1.5f; }
    }
    public void Update()
    {
        if (fell) { fellTimeLife -= Time.deltaTime; }
        if (fell && fellTimeLife <= 0) { Destroy(gameObject); }

        if((LifeTime > TimeFall)) { return; }
        LifeTime += Time.deltaTime;
        if(LifeTime > TimeFall) { GetComponent<Rigidbody2D>().velocity = new(GetComponent<Rigidbody2D>().velocity.x, GetComponent<Rigidbody2D>().velocity.y * 100f); }
    }
}
