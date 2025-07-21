using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    public Transform targetRoomSpawn;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("collision");
        if (other.CompareTag("Player"))
        {
            Debug.Log("player");
            other.transform.position = targetRoomSpawn.position;
        }
    }
}
