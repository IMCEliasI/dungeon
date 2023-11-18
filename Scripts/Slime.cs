using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slime : MonoBehaviour
{
    private bool Anim_playing = false;
    private float jump_speed = 5f;

    private float timer_calm = 0f;

    public GameObject player;
    public GameObject Wave;
    public List<AudioClip> Audio;
    public GameObject walker;
    public GameObject Victory_UI;
    public GameObject local_coins;

    public float HP = 500;
    private float HP_max = 500;
    public GameObject BossBar;
    private GameObject HP_slider;
    private bool Second_phase = false;
    private bool Upped = false;
    void Stop_Anim()
    {
        GetComponent<Animator>().SetLayerWeight(0, 0);
        GetComponent<Animator>().SetLayerWeight(1, 0);
    }
    public void Spawn_Wave()
    {
        if (timer_calm >= 4f)
        {
            GameObject newWaveRight = Instantiate(Wave, transform.position + new Vector3(0, -2.5f, 0), Quaternion.identity);
            newWaveRight.GetComponent<Rigidbody2D>().velocity = new Vector2(-10, 0);
            newWaveRight.AddComponent<Wave>();

            GameObject newWaveLeft = Instantiate(Wave, transform.position + new Vector3(0, -2.5f, 0), Quaternion.identity);
            newWaveLeft.GetComponent<Rigidbody2D>().velocity = new Vector2(10, 0);
            newWaveLeft.transform.localScale = new Vector3(-1, 1, 1);
            newWaveLeft.AddComponent<Wave>();

            newWaveRight.GetComponent<Wave>().player = player;
            newWaveLeft.GetComponent<Wave>().player = player;
        }
    }
    public void Finished_Jump()
    {
        Anim_playing = false;
    }
    void Jump()
    {
        if (!Anim_playing && GetComponent<Rigidbody2D>().velocity.y == 0 && !Second_phase && HP > 0)
        {
            Anim_playing = true;

            AudioSource Sound = GetComponent<AudioSource>();

            Sound.GetComponent<AudioSource>().clip = Audio[0];
            Sound.GetComponent<AudioSource>().loop = false;
            Sound.GetComponent<AudioSource>().Play();

            Stop_Anim();
            GetComponent<Animator>().SetLayerWeight(0, 1f);
            GetComponent<Animator>().PlayInFixedTime("Slime_Run", 0, 0);

            Vector2 vertical_speed = new(GetComponent<Rigidbody2D>().velocity.x, GetComponent<Rigidbody2D>().velocity.y + jump_speed);

            GetComponent<Rigidbody2D>().velocity = vertical_speed;
        }
    }
    void Link_HP()
    {
        float calc_Hp = HP / HP_max;

        HP_slider.GetComponent<RectTransform>().sizeDelta = new(750 * calc_Hp, HP_slider.GetComponent<RectTransform>().sizeDelta.y);
        HP_slider.GetComponent<RectTransform>().anchoredPosition = new(375 * (calc_Hp - 1), HP_slider.GetComponent<RectTransform>().anchoredPosition.y);
    }
    private void Start()
    {
        HP_slider = BossBar.transform.Find("HP_slider").gameObject;
    }
    void Second_Phase()
    {
        if (HP < 250 && !Second_phase && HP > 0)
        {
            Second_phase = true;

            GetComponent<SpriteRenderer>().color = new(255, 0, 0, 255);

            Stop_Anim();

            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
    }
    void Spawn_Walkers()
    {
        Instantiate(walker, transform.position + new Vector3(15, 7), Quaternion.identity, transform.parent);

        Instantiate(walker, transform.position + new Vector3(-15, 7), Quaternion.identity, transform.parent);
    }
    void Dead()
    {
        if(HP <= 0 && GetComponent<Animator>().GetLayerWeight(1) != 1f)
        {
            Stop_Anim();
            GetComponent<Animator>().SetLayerWeight(1, 1f);
            GetComponent<Animator>().PlayInFixedTime("Slime_Die", 1, 0);
            GetComponent<SpriteRenderer>().color = new(1, 1, 1, 1);

            local_coins.GetComponent<Text>().text = (int.Parse(local_coins.GetComponent<Text>().text) + Mathf.RoundToInt(200 * All_Upgrades.Coins_modificator)).ToString();
            All_Upgrades.Coins += Mathf.RoundToInt(200 * All_Upgrades.Coins_modificator);
        }
    }
    public void Victory()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Confined;
        Victory_UI.SetActive(true);
    }
    void Update()
    {
        Jump();
        Link_HP();
        Second_Phase();
        Dead();

        if (Second_phase)
        {
            if(transform.localPosition.y < 3 && !Upped)
            {
                transform.localPosition = transform.localPosition + new Vector3(0, 4f * Time.deltaTime);

                if(transform.localPosition.y >= 3) { Upped = true; InvokeRepeating("Spawn_Walkers", 5f, 5f); GetComponent<PolygonCollider2D>().isTrigger = true; }
            }

            if (Upped)
            {
                transform.localPosition = new Vector3(Random.Range(-0.75f, 5.75f), Random.Range(2.5f, 3.5f));
            }
        }



        timer_calm -= Time.deltaTime;
        if(timer_calm <= 0)
        {
            timer_calm = 15f;
        }

        if(transform.localPosition.x > 2.5 && !Second_phase) { transform.localPosition = transform.localPosition - new Vector3(0.1f, 0); }
        if (transform.localPosition.x < 2.5 && !Second_phase) { transform.localPosition = transform.localPosition + new Vector3(0.1f, 0); }
    }
}
