using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Control : MonoBehaviour
{
    private float speed;
    private float max_speed = 7.5f;
    private readonly float jump_speed = 6.5f;
    private float double_jump = 0;

    private float Redder_damage = 0f;

    private float Damage = 10f;
    public float HP = 100;
    public GameObject Ball;
    public GameObject StepSound;
    public List<AudioClip> Audio;

    public GameObject Start_of_game;
    public GameObject Game_Over_UI;
    public GameObject Boss_Bar;
    public GameObject ESC;
    public GameObject PressF;
    public GameObject Unavailable;

    private bool already_show_f = false;
    private bool crouching = false;
    private float can_quit_rope = 0;
    private bool on_rope = false;
    private float Jump_timer = 0;
    private float staying;
    private int Max_HP = 100;
    public GameObject HP_text;
    public GameObject HP_slider;
    private float Full_width_hp;
    private float Start_pos_x;
    private bool anim_playing = false;
    private bool attack = false;
    private bool knife = false;
    private bool in_jump = false;

    private int dead_count = 0;
    void Start()
    {
        AudioListener.volume = All_Upgrades.volume;
        AudioListener.pause = true;

        transform.Find("Main_Theme").GetComponent<AudioSource>().Stop();

        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Locked;
        Full_width_hp = HP_slider.GetComponent<RectTransform>().rect.width;
        Start_pos_x = HP_slider.GetComponent<RectTransform>().anchoredPosition.x;

        Max_HP = Mathf.RoundToInt(100f * All_Upgrades.HP_modificator);
        HP = Max_HP;
        Link_HP();

        Damage = Mathf.RoundToInt(Damage * All_Upgrades.Damage_modificator);
        if (All_Upgrades.Hardmode) { Damage *= 1.5f; }

        max_speed = Mathf.RoundToInt(max_speed * All_Upgrades.Speed_modificator);
        speed = max_speed;

        transform.Find("Dark").transform.localScale = transform.Find("Dark").transform.localScale * All_Upgrades.Eye_modificator;

        if(All_Upgrades.Damage_Upgrade_Count >= 5)
        {
            knife = true;
        }
    }
    
    void Stop_anim()
    {
        GetComponent<Animator>().SetLayerWeight(0, 0);
        GetComponent<Animator>().SetLayerWeight(1, 0);
        GetComponent<Animator>().SetLayerWeight(2, 0);
        GetComponent<Animator>().SetLayerWeight(3, 0);
        GetComponent<Animator>().SetLayerWeight(4, 0);
        GetComponent<Animator>().SetLayerWeight(5, 0);
        GetComponent<Animator>().SetLayerWeight(6, 0);
        GetComponent<Animator>().SetLayerWeight(7, 0);
        GetComponent<Animator>().SetLayerWeight(8, 0);
        GetComponent<Animator>().SetLayerWeight(9, 0);
        GetComponent<Animator>().SetLayerWeight(10, 0);
        GetComponent<Animator>().SetLayerWeight(11, 0);
        GetComponent<Animator>().SetLayerWeight(12, 0);
        GetComponent<Animator>().SetLayerWeight(13, 0); // knife hit
        GetComponent<Animator>().SetLayerWeight(14, 0); // knife motion hit
        GetComponent<Animator>().SetLayerWeight(15, 0);
        GetComponent<Animator>().SetLayerWeight(16, 0); // jump strike
        GetComponent<Animator>().SetLayerWeight(17, 0); // jump knife strike
    }
    public void Finished_Hand_Attack()
    {
        anim_playing = false;
        attack = false;
        Damage_Enemy();
    }
    public void Finished_Jump()
    {
        anim_playing = false;
    }
    
    public void Finished_Head_Shoot()
    {
        anim_playing = false;
        attack = false;
    }
    
    public void Revived()
    {
        AudioListener.pause = false;
        anim_playing = false;
        HP = Max_HP;
    }
    public void Damage_Enemy()
    {
        RaycastHit2D right_hit = Physics2D.Raycast(transform.position, transform.right, 2f, (1 << 6));
        RaycastHit2D left_hit = Physics2D.Raycast(transform.position, -transform.right, 2f, (1 << 6));

        if (right_hit && transform.localScale.x == 1)
        {
            GameObject hit_object = right_hit.collider.gameObject;
            Set_Damage(hit_object);
        }
        if (left_hit && transform.localScale.x == -1)
        {
            GameObject hit_object = left_hit.collider.gameObject;
            Set_Damage(hit_object);
        }
    }
    public void Get_Damage()
    {
        Redder_damage = 0.1f;
        GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
    }
    void Set_Damage(GameObject hit_object)
    {
        attack = false;
        string name_of_object = hit_object.name;
        if (name_of_object == "Walker")
        {
            hit_object.GetComponent<Walker>().HP -= Damage;
            hit_object.GetComponent<Walker>().Get_Damaged();
        }
        if (name_of_object == "Striker")
        {
            hit_object.GetComponent<Striker>().HP -= Damage;
            hit_object.GetComponent<Striker>().Get_Damaged();
        }
        if (name_of_object == "Barrel")
        {
            hit_object.GetComponent<Barrel>().Get_Damaged();
        }
        if (name_of_object == "Slime")
        {
            hit_object.gameObject.GetComponent<Slime>().HP -= Damage;
        }
    }
    void Change_anim(int Anim_code)
    {
        if (anim_playing) { return; }
        attack = false;
        if (Anim_code == 0) // �����
        {
            StepSound.GetComponent<AudioSource>().Stop();
            Stop_anim();
            GetComponent<Animator>().SetLayerWeight(0, 1f);
        }
        if(Anim_code == 1) // ���
        {
            Stop_anim();
            GetComponent<Animator>().SetLayerWeight(1, 1f);

            if (!StepSound.GetComponent<AudioSource>().isPlaying)
            {
                StepSound.GetComponent<AudioSource>().loop = true;
                StepSound.GetComponent<AudioSource>().clip = Audio[0];
                StepSound.GetComponent<AudioSource>().Play();
            }
        }
        if(Anim_code == 2) // ���� �����
        {
            Stop_anim();

            if (!knife) 
            {
                GetComponent<Animator>().SetLayerWeight(2, 1f);
                GetComponent<Animator>().PlayInFixedTime("Player_Hand_Attack", 2, 0);

                StepSound.GetComponent<AudioSource>().clip = Audio[3];
                StepSound.GetComponent<AudioSource>().loop = false;
                StepSound.GetComponent<AudioSource>().Play();
            }
            else
            {
                StepSound.GetComponent<AudioSource>().clip = Audio[2];
                StepSound.GetComponent<AudioSource>().loop = false;
                StepSound.GetComponent<AudioSource>().Play();

                GetComponent<Animator>().SetLayerWeight(13, 1f);
                GetComponent<Animator>().PlayInFixedTime("Player_Knife_Hit", 13, 0);
            }
            anim_playing = true;
        }
        if(Anim_code == 3) // ������
        {
            Stop_anim();
            GetComponent<Animator>().SetLayerWeight(3, 1f);
            GetComponent<Animator>().PlayInFixedTime("Player_Jump", 3, 0);

            StepSound.GetComponent<AudioSource>().clip = Audio[1];
            StepSound.GetComponent<AudioSource>().loop = false;
            StepSound.GetComponent<AudioSource>().Play();

            anim_playing = true;
        }
        if (Anim_code == 4) // �������
        {
            Stop_anim();
            GetComponent<Animator>().SetLayerWeight(4, 1f);
            GetComponent<Animator>().PlayInFixedTime("Player_Head_Shoot", 4, 0);

            anim_playing = true;
        }
        if (Anim_code == 5) // ������
        {
            StepSound.GetComponent<AudioSource>().Stop();
            Stop_anim();
            GetComponent<Animator>().SetLayerWeight(5, 1f);
        }
        if (Anim_code == 6) // ������ ������
        {
            Stop_anim();

            StepSound.GetComponent<AudioSource>().clip = Audio[1];
            StepSound.GetComponent<AudioSource>().loop = false;
            StepSound.GetComponent<AudioSource>().Play();

            GetComponent<Animator>().SetLayerWeight(6, 1f);
            GetComponent<Animator>().PlayInFixedTime("Player_Second_Jump", 6, 0);
            anim_playing = true;
        }
        if (Anim_code == 7) // �������
        {
            Stop_anim();
            GetComponent<Animator>().SetLayerWeight(7, 1f);
            anim_playing = true;
        }
        if (Anim_code == 8) // ��� ������
        {
            Stop_anim();
            GetComponent<Animator>().SetLayerWeight(8, 1f);
            GetComponent<Animator>().PlayInFixedTime("Player_Die", 8, 0);
            anim_playing = true;
        }
        if (Anim_code == 9) // ������
        {
            Stop_anim();
            GetComponent<Animator>().SetLayerWeight(9, 1f);
            GetComponent<Animator>().PlayInFixedTime("Player_Crouching_Down", 9, 0);
            crouching = true;
            anim_playing = true;
        }
        if (Anim_code == 10) // �����
        {
            Stop_anim();
            GetComponent<Animator>().SetLayerWeight(10, 1f);
            GetComponent<Animator>().PlayInFixedTime("Player_Crouching_Up", 10, 0);
            crouching = false;
            anim_playing = true;
        }
        if (Anim_code == 11) // ���� ���
        {
            Stop_anim();
            GetComponent<Animator>().SetLayerWeight(11, 1f);
            anim_playing = true;
        }
        if (Anim_code == 12) // ����� � ���������
        {
            Stop_anim();

            if (!knife)
            {
                GetComponent<Animator>().SetLayerWeight(12, 1f);
                GetComponent<Animator>().PlayInFixedTime("Player_Attack_Motion", 12, 0);

                StepSound.GetComponent<AudioSource>().clip = Audio[3];
                StepSound.GetComponent<AudioSource>().loop = false;
                StepSound.GetComponent<AudioSource>().Play();
            }
            else
            {
                StepSound.GetComponent<AudioSource>().clip = Audio[2];
                StepSound.GetComponent<AudioSource>().loop = false;
                StepSound.GetComponent<AudioSource>().Play();

                GetComponent<Animator>().SetLayerWeight(14, 1f);
                GetComponent<Animator>().PlayInFixedTime("Player_Knife_Motion", 14, 0);
            }
            anim_playing = true;
        }
        if (Anim_code == 15) // revive
        {
            Stop_anim();
            GetComponent<Animator>().SetLayerWeight(15, 1f);
            GetComponent<Animator>().PlayInFixedTime("Player_Revive", 15, 0);
            anim_playing = true;
        }
        if (Anim_code == 16) // Jump Strike
        {
            StepSound.GetComponent<AudioSource>().clip = Audio[3];
            StepSound.GetComponent<AudioSource>().loop = false;
            StepSound.GetComponent<AudioSource>().Play();

            Stop_anim();
            GetComponent<Animator>().SetLayerWeight(16, 1f);
            GetComponent<Animator>().PlayInFixedTime("Player_Jump_Strike", 16, 0);
            anim_playing = true;
        }
        if (Anim_code == 17) // Jump knife Strike
        {
            StepSound.GetComponent<AudioSource>().clip = Audio[2];
            StepSound.GetComponent<AudioSource>().loop = false;
            StepSound.GetComponent<AudioSource>().Play();

            Stop_anim();
            GetComponent<Animator>().SetLayerWeight(17, 1f);
            GetComponent<Animator>().PlayInFixedTime("Player_Knife_Jump", 17, 0);
            anim_playing = true;
        }
    }
    void Moving()
    {
        if (on_rope) { GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0); return; }
        Vector3 direction = new();

        if (Input.GetKey(KeyCode.A)) { direction += -transform.right; transform.localScale = new Vector3(-1, 1, 1); staying = 2f; }
        if (Input.GetKey(KeyCode.D)) { direction += transform.right; transform.localScale = new Vector3(1, 1, 1); staying = 2f; }

        //if (Input.GetKey(KeyCode.A) && attack) { direction += -transform.right; staying = 2f; }
       // if (Input.GetKey(KeyCode.D) && attack) { direction += transform.right; staying = 2f; }

        if (!crouching) 
        {
            if (direction == new Vector3()) { Change_anim(5); } else { Change_anim(1); } 
        }
        else
        {
            if (direction != new Vector3()) { anim_playing = false; Change_anim(11); }

            if (direction == new Vector3() && GetComponent<Animator>().GetLayerWeight(11) == 1f) { GetComponent<Animator>().PlayInFixedTime("Player_Crouching_walk", 11, 0); }
        }

        if(staying <= 0) { Change_anim(0); }

        direction *= speed;
        GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x, GetComponent<Rigidbody2D>().velocity.y);
    }
    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Change_anim(4);
            attack = true;
        }
    }
    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (in_jump) 
            {
                if (!knife)
                {
                    anim_playing = false;
                    Stop_anim();
                    Change_anim(16);
                    attack = true;
                }
                else
                {
                    anim_playing = false;
                    Stop_anim();
                    Change_anim(17);
                    attack = true;
                }
            }
            else
            {
                if (GetComponent<Rigidbody2D>().velocity.x == 0) { Change_anim(2); } else { Change_anim(12); }
                attack = true;
            }
        }
    }
    void Jumping()
    {
        if (on_rope) { return; }

        if(!All_Upgrades.Double && Input.GetKeyDown(KeyCode.Space) && double_jump >= 1)
        {
            Unavailable.GetComponent<Text>().color = new Color(Unavailable.GetComponent<Text>().color.r, Unavailable.GetComponent<Text>().color.g, Unavailable.GetComponent<Text>().color.b, 1);
        }

        if (Input.GetKeyDown(KeyCode.Space) && Jump_timer <= 0 && (double_jump < 2))
        {
            double_jump += 1;
            Jump_timer = 0.25f;

            if (!All_Upgrades.Double && double_jump == 2) { return; }

            in_jump = true; 

            Vector2 direction = GetComponent<Rigidbody2D>().velocity;
            Vector2 vertical_speed;

            anim_playing = false;

            if (double_jump == 1) { Change_anim(3); }
            if (double_jump == 2) { Change_anim(6); }

            vertical_speed = new Vector2(direction.x, direction.y + jump_speed);

            GetComponent<Rigidbody2D>().velocity = vertical_speed;
        }
        if (GetComponent<Rigidbody2D>().velocity.y <= Mathf.Abs(0.1f))
        {
            RaycastHit2D on_floor = Physics2D.Raycast(transform.position, -transform.up, 2f, ~(1 << 3));
            if (on_floor) { double_jump = 0; in_jump = false; }
        }
    }
    void Link_HP()
    {
        if (GetComponent<Animator>().GetLayerWeight(15) != 1f)
        {
            HP_text.GetComponent<Text>().text = HP.ToString();

            HP_slider.GetComponent<RectTransform>().sizeDelta = new Vector2(Full_width_hp * HP / Max_HP, HP_slider.GetComponent<RectTransform>().rect.height);
            HP_slider.GetComponent<RectTransform>().anchoredPosition = new Vector2((Start_pos_x - Full_width_hp + Full_width_hp * HP / Max_HP) / 2, HP_slider.GetComponent<RectTransform>().anchoredPosition.y);
        }
    }
    public void AD_reward()
    {
        if (HP <= 0)
        {
            AudioListener.pause = false;
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Game_Over_UI.SetActive(false);

            HP = Max_HP;
            anim_playing = false;
            Change_anim(15);
        }
    }
    void Game_over()
    {
        if(HP <= 0 && GetComponent<Animator>().GetLayerWeight(8) != 1f)
        {
            if (All_Upgrades.lives > dead_count)
            {
                dead_count += 1;
                HP = Max_HP;
                anim_playing = false;
                Change_anim(15);
            }
            else
            {
                AudioListener.pause = true;
                anim_playing = false;
                Change_anim(8);
            }
        }
    }
    public void Goto_Menu()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Confined;
        Game_Over_UI.SetActive(true);
    }
    void Crouch()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            GetComponent<BoxCollider2D>().size = new Vector2(1.2f, 2f);
            GetComponent<BoxCollider2D>().offset = new Vector2(0, -0.5f);

            Change_anim(9);

            speed = max_speed / 4;
        }
        else
        {
            GetComponent<BoxCollider2D>().size = new Vector2(1.2f, 3.1f);
            GetComponent<BoxCollider2D>().offset = new Vector2(0f, 0.06f);

            speed = max_speed;
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            anim_playing = false;
            Change_anim(10);
        }
    }
    public void Crouching_Up()
    {
        anim_playing = false;
    }
    private void Show_press_f()
    {
        if (already_show_f) { return; }
        PressF.SetActive(true);
        already_show_f = true;
    }
    void Rope_detect()
    {
        RaycastHit2D right_hit = Physics2D.Raycast(transform.position, transform.right, 1f, 1 << 7);
        RaycastHit2D left_hit = Physics2D.Raycast(transform.position, -transform.right, 1f, 1 << 7);

        if (right_hit)
        {
            if (right_hit.collider.gameObject.CompareTag("Rope"))
            {
                Rope_Detected("right", right_hit.collider.gameObject.transform.position);
                Show_press_f();
            }
        }
        if (left_hit)
        {
            if (left_hit.collider.gameObject.CompareTag("Rope"))
            {
                Rope_Detected("left", left_hit.collider.gameObject.transform.position);
                Show_press_f();
            }
        }

        if(!right_hit & !left_hit) { PressF.SetActive(false); }
    }
    void Rope_Detected(string side, Vector3 rope_pos)
    {
        if (Input.GetKeyDown(KeyCode.F) && on_rope == false)
        {
            anim_playing = false;
            on_rope = true;
            Change_anim(7);

            can_quit_rope = 1f;

            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            transform.position = new Vector2(rope_pos.x, transform.position.y);
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
            GetComponent<Rigidbody2D>().freezeRotation = true;
        }
    }
    void Moving_on_rope()
    {
        if (on_rope)
        {
            if (Input.GetKey(KeyCode.W)) 
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
            }
            if (Input.GetKey(KeyCode.S))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, -speed);
            }

            if (Input.GetKey(KeyCode.A)){ transform.localScale = new Vector3(1, 1, 1); }
            if (Input.GetKey(KeyCode.D)) { transform.localScale = new Vector3(-1, 1, 1); }

            if (can_quit_rope <= 0f && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.F)))
            {
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                GetComponent<Rigidbody2D>().freezeRotation = true;

                on_rope = false;
                anim_playing = false;
            }
        }
    }
    public void Spawn_ball()
    {
        Vector2 spawn_position = new(transform.position.x + transform.localScale.x * 1.5f , transform.position.y + 0.4f);
        GameObject bullet = Instantiate(Ball, spawn_position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(8f * transform.localScale.x, 0);
        bullet.AddComponent<Player_Ball>();

        StepSound.GetComponent<AudioSource>().clip = Audio[4];
        StepSound.GetComponent<AudioSource>().loop = false;
        StepSound.GetComponent<AudioSource>().Play();
    }
    void Enable_Dark()
    {
        if (transform.position.y < -50)
        {
            transform.Find("Dark").gameObject.SetActive(true);
        }
        if (transform.position.y < -150)
        {
            transform.Find("Dark").gameObject.SetActive(false);
            Boss_Bar.SetActive(true);
        }
    }
    void Jerk()
    {
        if (All_Upgrades.Jerk)
        {
            if (Input.GetKeyDown(All_Upgrades.JerkKey))
            {
                Stop_anim();
                anim_playing = false;
                on_rope = false;
                crouching = false;
                attack = false;
                GetComponent<Rigidbody2D>().MovePosition(new Vector2(transform.position.x + transform.localScale.x * 10f, transform.position.y));

                foreach (Transform child in transform.Find("Shadow").transform)
                {
                    child.gameObject.GetComponent<Shadow>().Reset();
                    child.gameObject.SetActive(true);
                }
            }
            if (transform.Find("Shadow").transform.GetChild(4).gameObject.activeSelf)
            {
                RaycastHit2D JerkRay = Physics2D.Raycast(transform.position, transform.right * transform.localScale.x, 1.5f, 1 << 6);

                if (JerkRay)
                {
                    if (JerkRay.collider.gameObject.name != "Slime")
                    {
                        JerkRay.collider.gameObject.GetComponent<Rigidbody2D>().simulated = false;
                        JerkRay.collider.gameObject.GetComponent<CapsuleCollider2D>().isTrigger = true;
                    }
                    transform.localPosition += new Vector3(transform.localScale.x, 0);

                    string name_of_object = JerkRay.collider.gameObject.name;
                    if (name_of_object == "Walker")
                    {
                        JerkRay.collider.gameObject.GetComponent<Walker>().HP -= Damage;
                        JerkRay.collider.gameObject.GetComponent<Walker>().Get_Damaged();
                    }
                    if (name_of_object == "Striker")
                    {
                        JerkRay.collider.gameObject.GetComponent<Striker>().HP -= Damage;
                        JerkRay.collider.gameObject.GetComponent<Striker>().Get_Damaged();
                    }

                    if (!knife)
                    {
                        StepSound.GetComponent<AudioSource>().clip = Audio[3];
                        StepSound.GetComponent<AudioSource>().loop = false;
                        StepSound.GetComponent<AudioSource>().Play();
                    }
                    else
                    {
                        StepSound.GetComponent<AudioSource>().clip = Audio[2];
                        StepSound.GetComponent<AudioSource>().loop = false;
                        StepSound.GetComponent<AudioSource>().Play();
                    }

                    if (JerkRay.collider.gameObject.name != "Slime")
                    {
                        JerkRay.collider.gameObject.GetComponent<Rigidbody2D>().simulated = true;
                        JerkRay.collider.gameObject.GetComponent<CapsuleCollider2D>().isTrigger = false;
                    }
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(All_Upgrades.JerkKey))
            {
                Unavailable.GetComponent<Text>().color = new Color(Unavailable.GetComponent<Text>().color.r, Unavailable.GetComponent<Text>().color.g, Unavailable.GetComponent<Text>().color.b, 1);
            }
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!ESC.activeSelf)
            {
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.Confined;
                ESC.SetActive(true);
                AudioListener.pause = true;
            }
            else
            {
                if (!Start_of_game.activeSelf)
                {
                    Time.timeScale = 1;
                    Cursor.lockState = CursorLockMode.Locked;
                    ESC.SetActive(false);
                    AudioListener.pause = false;
                }
            }
        }

        if (!Start_of_game.activeSelf)
        {
            Moving();
            Jumping();
            Link_HP();
            Game_over();
            Crouch();
            Attack();
            Shoot();
            Rope_detect();
            Moving_on_rope();
            Enable_Dark();
            Jerk();

            Redder_damage -= Time.deltaTime;
            if (Redder_damage <= 0) { Redder_damage = 0; GetComponent<SpriteRenderer>().color = new Color(1, 1, 1); }

            staying -= Time.deltaTime;
            if (staying <= 0) { staying = 0; }

            Jump_timer -= Time.deltaTime;
            if (Jump_timer <= 0) { Jump_timer = 0; }

            can_quit_rope -= Time.deltaTime;
            if (can_quit_rope <= 0) { can_quit_rope = 0; }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                transform.Find("Main_Theme").GetComponent<AudioSource>().Play();
                Time.timeScale = 1;
                AudioListener.pause = false;
                Start_of_game.SetActive(false);
                transform.position = new(30f, 50f);
            }
        }
    }
}
