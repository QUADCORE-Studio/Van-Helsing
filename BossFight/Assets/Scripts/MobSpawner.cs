using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    [System.Serializable]
    public class spawnPoint
    {
        public Transform point;
        public GameObject[] mobPrefabs;
    }

    public spawnPoint[] spawnPoints;
    public RoomTrigger roomTrigger;

    void Start()
    {
        foreach (var sp in spawnPoints)
        {
            if (sp != null && sp.point != null && sp.mobPrefabs != null && sp.mobPrefabs.Length > 0)
            {
                GameObject randomPrefab = sp.mobPrefabs[Random.Range(0, sp.mobPrefabs.Length)];
                GameObject mob = Instantiate(randomPrefab, sp.point.position, Quaternion.identity);

                EnemyAI ai = mob.GetComponent<EnemyAI>();
                if (ai != null)
                {
                    RoomTrigger nearestRoom = FindNearestRoom(sp.point.position);
                    if (nearestRoom != null)
                    {
                        ai.SetRoom(nearestRoom);
                    }
                }
            }
        }
    }


    RoomTrigger FindNearestRoom(Vector3 spawnPosition)
    {
        RoomTrigger[] rooms = FindObjectsByType<RoomTrigger>(FindObjectsSortMode.None);
        RoomTrigger nearest = null;
        float closestDistance = float.MaxValue;

        foreach (RoomTrigger room in rooms)
        {
            float distance = Vector3.Distance(room.transform.position, spawnPosition);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                nearest = room;
            }
        }

        return nearest;
    }
}
