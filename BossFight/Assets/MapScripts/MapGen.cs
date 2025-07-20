using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGen : MonoBehaviour
{
    // 1 Bot Door, 2 Top Door, 3 Left Door, 4 Right Door]
    public int doorDirection;

    private RoomTemps templates;
    private int rand;
    private bool isSpawned = false;
    private bool isDestroyed = false;
    public float waitTime = 4f;

    void Start()
    {
        Destroy(gameObject, waitTime);
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemps>();
        Invoke("Spawn_map",0.5f);
    }

    void Spawn_map()
    {
        if (isSpawned == false)
        {
            if (doorDirection == 1)
            {
                rand = Random.Range(0, templates.botRooms.Length);
                Instantiate(templates.botRooms[rand], transform.position, templates.botRooms[rand].transform.rotation);
            }
            else if (doorDirection == 2)
            {
                rand = Random.Range(0, templates.topRooms.Length);
                Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);
            }
            else if (doorDirection == 3)
            {
                rand = Random.Range(0, templates.leftRooms.Length);
                Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
            }
            else if (doorDirection == 4)
            {
                rand = Random.Range(0, templates.rightRooms.Length);
                Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
            }
            
            isSpawned = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SpawnPoint")&& !other.CompareTag("Player"))
        {
            MapGen otherMapGen = other.GetComponent<MapGen>();
            if (otherMapGen != null && otherMapGen.isSpawned == false && isSpawned == false)
            {
                Destroy(gameObject);
            }
            isSpawned = true; 
        }
    }

}
