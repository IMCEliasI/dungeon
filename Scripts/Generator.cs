using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public bool REGENERATE_LIGHT = false;
    public bool REGENERATE_DARK = false;
    private readonly int max_level_len = 10;
    public GameObject TileParent_light;
    public GameObject TileParent_dark;

    public GameObject Player;

    public List<GameObject> all_tiles = new();
    public GameObject Trap;
    public GameObject Boss_room;

    public List<GameObject> all_mobs = new();

    int Regenerate_int(int ignoring_int)
    {
        int rand = Random.Range(0, all_tiles.Count);

        if (rand == 2) { rand = Regenerate_int(2); }
        if (rand == 6) { rand = Regenerate_int(6); }

        return rand;
    }
    void Delete_light_level()
    {
        foreach(Transform child in TileParent_light.transform)
        {
            Destroy(child.gameObject);
        }
    }
    void Delete_dark_level()
    {
        foreach (Transform child in TileParent_dark.transform)
        {
            Destroy(child.gameObject);
        }
    }
    void Generate_Light_Level()
    {
        REGENERATE_LIGHT = true;
        Instantiate(all_tiles[6], new Vector3(30, 0, 0), Quaternion.identity, TileParent_light.transform);

        for (int length_of_level = 0; length_of_level <= max_level_len; length_of_level++)
        {
            int rand = Random.Range(0, all_tiles.Count);

            if (rand == 2) { rand = Regenerate_int(2); }
            if (rand == 6) { rand = Regenerate_int(6); }
            if (length_of_level == max_level_len) { rand = 2; }

            Instantiate(all_tiles[rand], new Vector3(20 * length_of_level + 50, 0, 0), Quaternion.identity, TileParent_light.transform);

            if (Spawn_Mob())
            {
                int random_mob = Random.Range(0, 3);
                if (random_mob > 1) { random_mob = 0; }

                GameObject newMob = Instantiate(all_mobs[random_mob], new Vector3(20 * length_of_level + 50, 0, 0), Quaternion.identity, TileParent_light.transform);
                newMob.name = all_mobs[random_mob].name;
            }
        }
    }
    void Generate_Dark_Level()
    {
        REGENERATE_DARK = true;
        GameObject newTile = Instantiate(all_tiles[2], new Vector3(30, -100, 0), Quaternion.identity, TileParent_dark.transform);
        newTile.transform.localScale = new(-1, 1, 1);
        newTile.transform.Find("Background").GetComponent<SpriteRenderer>().color = new(255, 0, 0, 255);

        for (int length_of_level = 0; length_of_level <= max_level_len; length_of_level++)
        {
            int rand = Random.Range(-1, all_tiles.Count);

            if (rand == 2) { rand = Regenerate_int(2); }
            if (rand == 6) { rand = Regenerate_int(6); }
            if (length_of_level == max_level_len) { rand = 6; }

            if (rand != -1)
            {
                newTile = Instantiate(all_tiles[rand], new Vector3(20 * length_of_level + 50, -100, 0), Quaternion.identity, TileParent_dark.transform);
                newTile.transform.localScale = new(-1, 1, 1);
                newTile.transform.Find("Background").GetComponent<SpriteRenderer>().color = new(255, 0, 0, 255);
            }
            else
            {
                newTile = Instantiate(Trap, new Vector3(20 * length_of_level + 50, -100, 0), Quaternion.identity, TileParent_dark.transform);
                newTile.transform.localScale = new(-1, 1, 1);
                newTile.transform.Find("Background").GetComponent<SpriteRenderer>().color = new(255, 0, 0, 255);
            }

            if (Spawn_Mob())
            {
                int random_mob = Random.Range(0, 3);
                if(random_mob > 1) { random_mob = 0; }

                GameObject newMob = Instantiate(all_mobs[random_mob], new Vector3(20 * length_of_level + 50, -100, 0), Quaternion.identity, TileParent_dark.transform);
                newMob.name = all_mobs[random_mob].name;
            }
        }
    }
    void Generate_Boss_Room()
    {
        Instantiate(Boss_room, new Vector3(45, -234.5f, 0), Quaternion.identity);
    }
    bool Spawn_Mob()
    {
        int rand = Random.Range(0, 5);

        if(rand == 0) { return false; }
        return true;
    }
    void Update()
    {
        if (!REGENERATE_LIGHT) 
        {
            Delete_light_level();
            Generate_Light_Level();
        }
        if (!REGENERATE_DARK)
        {
            Delete_dark_level();
            Generate_Dark_Level();
            Generate_Boss_Room();
        }
    }
}
