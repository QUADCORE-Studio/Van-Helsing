using UnityEngine;

public class RoomTrigger : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            EnemyAI[] allEnemies = FindObjectsByType<EnemyAI>(FindObjectsSortMode.None);
            foreach (var enemy in allEnemies)
            {
                if (enemy.IsInRoom(this))
                {
                    enemy.Activate();
                }
            }
        }
    }
}
