using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Walker : MonoBehaviour
{
    public GameObject player;
    public GameObject local_coins;
    private int direction_of_moving = 1;

    private float damage = 10f;
    private readonly float speed = 5f;
    private readonly float reload = 1f;

    public float HP = 25f;

    private bool anim_playing = false;
    private float delete_timer = 0f;
    private bool timer_start = false;

    private bool already_took_arm = false;

    private float shoot_timer = 0;
    private float Redder_damage = 0f;

    public AudioClip ArmTake;
    public AudioClip DeadBones;
    private void Start()
    {
        if (All_Upgrades.Hardmode) { damage *= 1.5f; }
    }
    public void Get_Damaged()
    {
        Redder_damage = 0.1f;
        GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
    }
    void Stop_Anim()
    {
        GetComponent<Animator>().SetLayerWeight(0, 0);
        GetComponent<Animator>().SetLayerWeight(1, 0); // 
        GetComponent<Animator>().SetLayerWeight(2, 0); // взять руку
        GetComponent<Animator>().SetLayerWeight(3, 0); // Партия! УДАР!
        GetComponent<Animator>().SetLayerWeight(4, 0); // вернуть руку
    }
    void HP_detect()
    {
        if(HP <= 0 && !anim_playing)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
            GetComponent<Rigidbody2D>().simulated = false;
            Stop_Anim();
            GetComponent<Animator>().SetLayerWeight(1, 1f);
            GetComponent<Animator>().PlayInFixedTime("Walker_Die", 1, 0);
            anim_playing = true;

            GetComponent<AudioSource>().loop = false;
            GetComponent<AudioSource>().clip = DeadBones;
            GetComponent<AudioSource>().Play();
        }
    }
    public void Took_Arm()
    {
        anim_playing = false;
        GetComponent<Animator>().SetLayerWeight(0, 1f);

        GetComponent<AudioSource>().loop = false;
        GetComponent<AudioSource>().clip = ArmTake;
        GetComponent<AudioSource>().Play();

        already_took_arm = true;
    }
    public void Dead()
    {
        delete_timer = 5f;
        timer_start = true;

        local_coins.GetComponent<Text>().text = (int.Parse(local_coins.GetComponent<Text>().text) + Mathf.RoundToInt(10 * All_Upgrades.Coins_modificator)).ToString();
        All_Upgrades.Coins += Mathf.RoundToInt(10 * All_Upgrades.Coins_modificator);
    }
    void Moving()
    {
        if (anim_playing) { return; }
        GetComponent<Rigidbody2D>().velocity = new Vector2(direction_of_moving * speed, GetComponent<Rigidbody2D>().velocity.y);
        transform.localScale = new Vector3(direction_of_moving, 1, 1);
    }
    void Check_Height()
    {
        if (transform.position.y <= -200f)
        {
            HP -= 2 * Time.deltaTime;
        }
    }
    void Wall_detect() 
    {
        shoot_timer -= Time.deltaTime;
        if (shoot_timer < -1) { shoot_timer = -1; }

        Vector3 Bias_pos = new(transform.localScale.x * 1.5f, 0, 0);
        RaycastHit2D right_hit = Physics2D.Raycast(transform.position + Bias_pos, transform.right, 0.2f, ~(1 << 2 | 1 << 7));
        RaycastHit2D left_hit = Physics2D.Raycast(transform.position + Bias_pos, -transform.right, 0.2f, ~(1 << 2 | 1 << 7));

        if (anim_playing) { return; }

        if (right_hit)
        {
            if (right_hit.rigidbody.gameObject.name != "Player")
            {
                direction_of_moving = -1;
                if (already_took_arm)
                {
                    already_took_arm = false;
                    Return_arm();
                }
            }
            else
            {
                if(shoot_timer <= 0)
                {
                    shoot_timer = reload;
                    Strike();
                }
            }
        }
        if (left_hit)
        {
            if (left_hit.rigidbody.gameObject.name != "Player")
            {
                direction_of_moving = 1;
                if (already_took_arm)
                {
                    already_took_arm = false;
                    Return_arm();
                }
            }
            else
            {
                if (shoot_timer <= 0)
                {
                    shoot_timer = reload;
                    Strike();
                }
            }
        }
    }
    void Return_arm()
    {
        Stop_Anim();
        GetComponent<Animator>().SetLayerWeight(4, 1f);
        GetComponent<Animator>().PlayInFixedTime("Walker_Return_Arm", 4, 0);
        anim_playing = true;
    }
    public void Returned_Arm()
    {
        anim_playing = false;
        Stop_Anim();
        GetComponent<Animator>().SetLayerWeight(0, 1f);
        GetComponent<Animator>().PlayInFixedTime("Walker_Walk", 0, 0);
    }
    void Strike()
    {
        if (!anim_playing && !already_took_arm)
        {
            Stop_Anim();
            GetComponent<Animator>().SetLayerWeight(2, 1f);
            GetComponent<Animator>().PlayInFixedTime("Walker_Take_Arm", 2, 0);
            anim_playing = true;
        }
        if (!already_took_arm) { return; }

        Stop_Anim();
        GetComponent<Animator>().SetLayerWeight(3, 1f);
        GetComponent<Animator>().PlayInFixedTime("Walker_Attack", 3, 0);
    }
    public void Set_Damage()
    {
        player.GetComponent<Control>().HP -= damage;
        player.GetComponent<Control>().Get_Damage();
    }
    void Player_detect()
    {
        RaycastHit2D right_hit = Physics2D.Raycast(transform.position, transform.right, 15f, ~(1 << 6 | 1 << 2 | 1 << 7));
        RaycastHit2D left_hit = Physics2D.Raycast(transform.position, -transform.right, 15f, ~(1 << 6 | 1 << 2 | 1 << 7));

        if (right_hit)
        {
            if (right_hit.rigidbody.gameObject.name == "Player")
            {
                direction_of_moving = 1;
            }
        }
        if (left_hit)
        {
            if (left_hit.rigidbody.gameObject.name == "Player")
            {
                direction_of_moving = -1;
            }
        }
    }
    public void Update()
    {
        if (HP > 0)
        {
            Check_Height();
            Moving();
            Wall_detect();
            Player_detect();
        }
        HP_detect();

        Redder_damage -= Time.deltaTime;
        if (Redder_damage <= 0) { Redder_damage = 0; GetComponent<SpriteRenderer>().color = new Color(1, 1, 1); }

        if (timer_start) {
            delete_timer -= Time.deltaTime;
            if (delete_timer <= 0) { delete_timer = 0f; Destroy(gameObject); }
        }
    }
}
