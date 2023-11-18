using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator_Endless : MonoBehaviour
{
    public bool REGENERATE_DARK = false;
    private readonly int max_level_len = 25;
    public GameObject TileParent_dark;

    public GameObject Player;
    public GameObject Wall;

    public List<GameObject> all_tiles = new();
    public GameObject Trap;

    private float End_pos_x;

    public List<GameObject> all_mobs = new();

    int Regenerate_int(int ignoring_int)
    {
        int rand = Random.Range(0, all_tiles.Count);

        if (rand == 2) { rand = Regenerate_int(2); }
        if (rand == 6) { rand = Regenerate_int(6); }

        return rand;
    }
    void Delete_dark_level()
    {
        foreach (Transform child in TileParent_dark.transform)
        {
            Destroy(child.gameObject);
        }
    }
    void Generate_Dark_Level()
    {
        REGENERATE_DARK = true;
        GameObject newTile = Instantiate(all_tiles[2], new Vector3(30, -100, 0), Quaternion.identity, TileParent_dark.transform);
        newTile.transform.localScale = new(-1, 1, 1);
        newTile.transform.Find("Background").GetComponent<SpriteRenderer>().color = new(255, 0, 0, 255);

        End_pos_x = 20 * max_level_len + 50;

        for (int length_of_level = -2; length_of_level <= max_level_len; length_of_level++)
        {
            int rand = Random.Range(-1, all_tiles.Count);

            if (rand != -1)
            {
                newTile = Instantiate(all_tiles[rand], new Vector3(20 * length_of_level + 50, 50, 0), Quaternion.identity, TileParent_dark.transform);
                newTile.transform.Find("Background").GetComponent<SpriteRenderer>().color = new(255, 0, 0, 255);
            }
            else
            {
                newTile = Instantiate(Trap, new Vector3(20 * length_of_level + 50, 50, 0), Quaternion.identity, TileParent_dark.transform);
                newTile.transform.Find("Background").GetComponent<SpriteRenderer>().color = new(255, 0, 0, 255);
            }

            if (Spawn_Mob())
            {
                int random_mob = Random.Range(0, 3);
                if (random_mob > 1) { random_mob = 0; }

                GameObject newMob = Instantiate(all_mobs[random_mob], new Vector3(20 * length_of_level + 50, 50, 0), Quaternion.identity, TileParent_dark.transform);
                newMob.name = all_mobs[random_mob].name;
            }
        }
    }
    bool Spawn_Mob()
    {
        int rand = Random.Range(0, 5);

        if (rand == 0) { return false; }
        return true;
    }
    void Update()
    {
        if(Player.transform.position.x >= End_pos_x)
        {
            Player.transform.position = new(30f, 50f);
            Wall.transform.position = Player.transform.position + new Vector3(-40, 0);
            Delete_dark_level();
            Generate_Dark_Level();
        }
        if (!REGENERATE_DARK)
        {
            Delete_dark_level();
            Generate_Dark_Level();
        }
    }
}
