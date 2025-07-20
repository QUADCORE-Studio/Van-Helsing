using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRooms : MonoBehaviour
{
    private RoomTemps temps;

    void Start()
    {
        temps = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemps>();
        temps.rooms.Add(this.gameObject);
    }
}
