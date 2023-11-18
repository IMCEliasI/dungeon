using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    float Damage = 65f;
    float Max_distance = 10f;

    bool Will_explode = false;

    public AudioClip Explosion;
    public void Damage_All()
    {
        Collider2D[] all_objects = Physics2D.OverlapCircleAll(transform.position + new Vector3(0, -0.6f, 0), Max_distance, (1 << 6 | 1 << 3));

        foreach (Collider2D entity in all_objects)
        {
            string name_of_entity = entity.gameObject.name;

            if (name_of_entity == "Walker")
            {
                entity.gameObject.GetComponent<Walker>().HP -= Calc_Damage(entity.gameObject);
                entity.gameObject.GetComponent<Walker>().Get_Damaged();
            }
            if (name_of_entity == "Striker")
            {
                entity.gameObject.GetComponent<Striker>().HP -= Calc_Damage(entity.gameObject);
                entity.gameObject.GetComponent<Striker>().Get_Damaged();
            }
            if (name_of_entity == "Player")
            {
                entity.gameObject.GetComponent<Control>().HP -= Mathf.RoundToInt(Calc_Damage(entity.gameObject));
                entity.gameObject.GetComponent<Control>().Get_Damage();
            }
        }

        Destroy(GetComponent<SpriteRenderer>());
        Invoke("Clear", 2f);
    }
    void Clear()
    {
        Destroy(gameObject);
    }
    float Calc_Damage(GameObject entity)
    {
        float distance = Mathf.Sqrt(Mathf.Pow(transform.position.x - entity.transform.position.x, 2) + Mathf.Pow(transform.position.y - entity.transform.position.y, 2) + Mathf.Pow(transform.position.z - entity.transform.position.z, 2));

        float calculated_damage = ((Max_distance - distance)/ Max_distance) * Damage;
        if(calculated_damage < 0) { calculated_damage = 0; }

        return calculated_damage;
    }
    public void Get_Damaged()
    {
        GetComponent<Animator>().enabled = true;
        GetComponent<Animator>().SetLayerWeight(0, 1f);
        GetComponent<Animator>().PlayInFixedTime("Barrel_Explosion", 0, 0);

        AudioSource Sound = GetComponent<AudioSource>();
        Sound.GetComponent<AudioSource>().clip = Explosion;
        Sound.GetComponent<AudioSource>().loop = false;
        Sound.GetComponent<AudioSource>().Play();

        GetComponent<PolygonCollider2D>().enabled = false;
    }
    void Fall()
    {
        if(Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y) >= 10f)
        {
            Will_explode = true;
        }
        if(Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y) <= 0.1f && Will_explode)
        {
            Get_Damaged();
        }
    }
    private void Update()
    {
        Fall();
    }
    private void Start()
    {
        GetComponent<Animator>().enabled = false;
    }
}
