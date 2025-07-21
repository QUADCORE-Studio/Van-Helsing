// using UnityEngine;

// public class BatSpawner : MonoBehaviour
// {
//     public static BatSpawner Instance;
//     public GameObject batPrefab;
//     public float spawnRate = 2f;

//     private bool isSpawning = false;
//     private float timer;

//     void Awake() => Instance = this;

//     void Update()
//     {
//         if (!isSpawning) return;

//         timer += Time.deltaTime;
//         if (timer >= spawnRate)
//         {
//             timer = 0;
//             SpawnBat();
//         }
//     }

//     public void StartSpawning() => isSpawning = true;
//     public void StopSpawning() => isSpawning = false;

//     void SpawnBat()
//     {
//         // Choose random spawn point
//         Instantiate(batPrefab, GetRandomPoint(), Quaternion.identity);
//     }

//     Vector2 GetRandomPoint() => // pick a random bat spawn position
// }
