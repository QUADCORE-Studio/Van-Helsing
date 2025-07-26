using UnityEngine;

public class SlashDamage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Dracula"))
        {
            DraculaBoss boss = other.GetComponent<DraculaBoss>();
            if (boss != null && boss.IsVulnerable())
            {
                boss.TakeDamage();
            }
        }

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
