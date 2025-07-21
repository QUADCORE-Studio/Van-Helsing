using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    public Transform targetRoomSpawn;
    public Transform newRoomCenter;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = targetRoomSpawn.position;
            Camera.main.GetComponent<CameraController>().SetTarget(newRoomCenter);
        }
    }
}
