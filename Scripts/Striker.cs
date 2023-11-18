using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Striker : MonoBehaviour
{
    private int direction_of_moving = 1;

    private readonly float speed = 5f;
    private readonly float reload = 2f;

    public float HP = 25f;
    private float delete_timer = 0f;
    private float Redder_damage = 0f;
    private bool timer_start = false;
    private bool already_dead = false;

    private float shoot_timer = 0f;
    private bool spawn_bullets = false;

    private bool anim_playing = false;

    public GameObject player;
    public GameObject bullet;
    public GameObject local_coins;

    void Stop_anim()
    {
        GetComponent<Animator>().SetLayerWeight(0, 0);
        GetComponent<Animator>().SetLayerWeight(1, 0);
        GetComponent<Animator>().SetLayerWeight(2, 0);
    }
    void Moving()
    {
        if (already_dead) { return; }
        if (anim_playing) { return; }
        GetComponent<Rigidbody2D>().velocity = new Vector2(direction_of_moving * speed, GetComponent<Rigidbody2D>().velocity.y);
        transform.localScale = new Vector3(-direction_of_moving, 1, 1);
    }
    void Wall_detect()
    {
        Vector3 Bias_pos = new(-transform.localScale.x * 1.75f, 0, 0);
        RaycastHit2D right_hit = Physics2D.Raycast(transform.position + Bias_pos, transform.right, 0.2f, ~(1 << 2 | 1 << 7));
        RaycastHit2D left_hit = Physics2D.Raycast(transform.position + Bias_pos, -transform.right, 0.2f, ~(1 << 2 | 1 << 7));

        if (right_hit)
        {
            if (right_hit.rigidbody.gameObject.name != "Player")
            {
                direction_of_moving = -1;
            }
        }
        if (left_hit)
        {
            if (left_hit.rigidbody.gameObject.name != "Player")
            {
                direction_of_moving = 1; 
            }
        }
    }
    void Player_detect()
    {
        shoot_timer -= Time.deltaTime;
        if(shoot_timer < -1) { shoot_timer = -1; }

        RaycastHit2D right_hit = Physics2D.Raycast(transform.position + new Vector3(0, 1f), transform.right, 15f, ~(1 << 6 | 1 << 2 | 1 << 7));
        RaycastHit2D left_hit = Physics2D.Raycast(transform.position + new Vector3(0, 1f), -transform.right, 15f, ~(1 << 6 | 1 << 2 | 1 << 7));

        bool find_player = false;

        if (right_hit)
        {
            if (right_hit.rigidbody.gameObject.name == "Player")
            {
                find_player = true;
                if (shoot_timer <= 0)
                {
                    shoot_timer = reload;
                    transform.localScale = new Vector3(-1, 1, 1);
                    if (GetComponent<Rigidbody2D>().bodyType != RigidbodyType2D.Static) { GetComponent<Rigidbody2D>().velocity = new(0, GetComponent<Rigidbody2D>().velocity.y); }
                    Shoot(1);
                }
            }
        }
        if (left_hit)
        {
            if (left_hit.rigidbody.gameObject.name == "Player")
            {
                find_player = true;
                if (shoot_timer <= 0)
                {
                    shoot_timer = reload;
                    transform.localScale = new Vector3(1, 1, 1);
                    if (GetComponent<Rigidbody2D>().bodyType != RigidbodyType2D.Static) { GetComponent<Rigidbody2D>().velocity = new(0, GetComponent<Rigidbody2D>().velocity.y); }
                    Shoot(-1);
                }
            }
        }

        if(!find_player && GetComponent<Animator>().GetLayerWeight(1) == 1f) 
        { 
            anim_playing = false; Stop_anim(); 
            GetComponent<Animator>().SetLayerWeight(0, 1f);
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }

        if (direction_of_moving == 0 && shoot_timer <= -1) { direction_of_moving = Random.Range(-1, 2); }
    }
    void Shoot(int shoot_direction)
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        if (!anim_playing)
        {
            Stop_anim();
            GetComponent<Animator>().SetLayerWeight(1, 1f);
            GetComponent<Animator>().PlayInFixedTime("Striker_Shoot", 1, 0);
            anim_playing = true;
        }
    }
    void HP_detect()
    {
        if (HP <= 0 && !already_dead)
        {
            if (GetComponent<Rigidbody2D>().bodyType != RigidbodyType2D.Static)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
            }

            GetComponent<Rigidbody2D>().simulated = false;

            already_dead = true;

            Stop_anim();

            GetComponent<Animator>().SetLayerWeight(2, 1f);
            GetComponent<Animator>().PlayInFixedTime("Striker_Die", 2, 0);
            anim_playing = true;
        }
    }
    public void Dead()
    {
        delete_timer = 5f;
        timer_start = true;

        local_coins.GetComponent<Text>().text = (int.Parse(local_coins.GetComponent<Text>().text) + Mathf.RoundToInt(20 * All_Upgrades.Coins_modificator)).ToString();
        All_Upgrades.Coins += Mathf.RoundToInt(20 * All_Upgrades.Coins_modificator); ;
    }
    public void Get_Damaged()
    {
        Redder_damage = 0.1f;
        GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
    }
    public void Start_Spawn()
    {
        spawn_bullets = true;
    }
    private void Spawn_Bullet()
    {
        GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
        newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(5 * -transform.localScale.x, -0.1f);
        newBullet.transform.rotation = Quaternion.Euler(0, 0, -transform.localScale.x * 45);

        newBullet.AddComponent<Bullet>();
        newBullet.GetComponent<Bullet>().player = player;

        newBullet.transform.position = new Vector2(newBullet.transform.position.x + 0.2f, newBullet.transform.position.y + 2.2f);
    }
    void Update()
    {
        if (!already_dead)
        {
            Moving();
            Wall_detect();
            Player_detect();
        }
        HP_detect();

        Redder_damage -= Time.deltaTime;
        if (Redder_damage <= 0) { Redder_damage = 0; GetComponent<SpriteRenderer>().color = new Color(1, 1, 1); }

        if (timer_start)
        {
            delete_timer -= Time.deltaTime;
            if (delete_timer <= 0) { delete_timer = 0f; Destroy(gameObject); }
        }

        if (shoot_timer == reload && GetComponent<Animator>().GetLayerWeight(1) == 1f && spawn_bullets) { Spawn_Bullet(); }
    }
}
