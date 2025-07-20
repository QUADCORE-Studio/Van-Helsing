using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemps : MonoBehaviour
{
    public enum Direction
    {
        North,
        South,
        East,
        West
    }
    public GameObject[] botRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;

    public List<GameObject> rooms;
    public Dictionary<Vector2Int, GameObject> SpawnedRooms = new();
    public float roomWidth = 10f; // Width of each room
    public float roomHeight = 10f; // Height of each room
    public Vector2Int currentRoomPos; // Current position in the grid

    // Start is called before the first frame update
    void Start()
    {
        // Set starting position
        currentRoomPos = GetGridPosition(rooms[0]);
    }
    void Update()
    {
        foreach (GameObject room in rooms)
        {
            AddRoomToDictionary(room);
        }
    }   
    public void AddRoomToDictionary(GameObject room)
    {
        Vector2Int gridPos = GetGridPosition(room);

        if (!SpawnedRooms.ContainsKey(gridPos))
        {
            SpawnedRooms.Add(gridPos, room);
        }
    }
    
    
    public Vector2Int GetGridPosition(GameObject room)
    {
        Transform center = room.transform.Find("CenterRoom");
        if (center == null)
        {
            Debug.LogError("Room prefab missing a 'CenterRoom' GameObject.");
            return Vector2Int.zero;
        }

        Vector3 pos = center.position;
        int x = Mathf.RoundToInt(pos.x / roomWidth);
        int y = Mathf.RoundToInt(pos.y / roomHeight);
        return new Vector2Int(x, y);
    }
}
