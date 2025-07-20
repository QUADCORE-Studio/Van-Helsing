using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGrid : MonoBehaviour
{
    public RoomTemps roomTemps;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Check if the player is entering a new grid cell
            Vector2Int gridPosition = roomTemps.GetGridPosition(collision.gameObject);
            Debug.Log("Player entered grid position: " + gridPosition);
        }
    }
}
