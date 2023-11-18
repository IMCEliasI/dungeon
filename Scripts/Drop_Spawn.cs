using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop_Spawn : MonoBehaviour
{
    public GameObject Drop;
    void Spawn_Drop()
    {
        int rand = Random.Range(0, 2);

        if(rand == 0)
        {
            GameObject newDrop = Instantiate(Drop, transform.position, Quaternion.identity, transform);
            newDrop.GetComponent<Rigidbody2D>().simulated = true;
        }
    }
    void Start()
    {
        InvokeRepeating("Spawn_Drop", 2f, 2f);
    }
}
