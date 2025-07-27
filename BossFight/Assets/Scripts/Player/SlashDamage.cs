// using UnityEngine;

// public class SlashDamage : MonoBehaviour
// {
//     private void OnTriggerEnter2D(Collider2D other)
//     {
//         if (other.CompareTag("Dracula"))
//         {
//             DraculaBoss boss = other.GetComponent<DraculaBoss>();
//             if (boss != null && boss.IsVulnerable())
//             {
//                 boss.TakeDamage();

//             }
//         }

//         if (other.CompareTag("Mob"))
//         {
//             EnemyAI bat = other.GetComponent<EnemyAI>();
//             if (bat != null)
//             {
//                 bat.TakeDamage(1f);
//             }
//         }
//     }
// }

using UnityEngine;

public class SlashDamage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Handle Dracula Phase 1
        DraculaBoss boss = other.GetComponent<DraculaBoss>();
        if (boss != null && boss.IsVulnerable())
        {
            boss.TakeDamage();
            return;
        }

        // Handle Dracula Phase 2
        phase_2_manager phase2 = other.GetComponent<phase_2_manager>();
        if (phase2 != null)
        {
            phase2.TakeDamage(1);
            return;
        }

        // Handle Dracula Phase 3
        DraculaPhase3 phase3 = other.GetComponent<DraculaPhase3>();
        if (phase3 != null)
        {
            phase3.TakeDamage(1);
            return;
        }

        // Handle mobs like bats
        if (other.CompareTag("Mob"))
        {
            EnemyAI bat = other.GetComponent<EnemyAI>();
            if (bat != null)
            {
                bat.TakeDamage(1f);
            }
        }
    }
}

