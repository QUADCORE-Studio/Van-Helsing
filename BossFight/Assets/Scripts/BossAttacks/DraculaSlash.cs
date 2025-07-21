using UnityEngine;

public class DraculaSlashAttack : MonoBehaviour
{
    [Header("Slash Settings")]
    public float slashRange = 1.5f;
    public float damage = 1f;
    public float cooldown = 3f;
    public LayerMask playerLayer;

    [Header("References")]
    public Transform player;
    //public Animator animator;

    private float lastSlashTime = -Mathf.Infinity;

    void Update()
    {
        if (CanSlash())
        {
            PerformSlash();
        }
    }

    private bool CanSlash()
    {
        if (Time.time < lastSlashTime + cooldown)
            return false;

        float dist = Vector2.Distance(transform.position, player.position);
        return dist <= slashRange;
    }

    private void PerformSlash()
    {
        lastSlashTime = Time.time;

        // Play slash animation
        //if (animator != null)
            //animator.SetTrigger("Slash");

        // Damage check
        Collider2D hit = Physics2D.OverlapCircle(transform.position, slashRange, playerLayer);
        if (hit != null && hit.CompareTag("Player"))
        {
            // Example: apply damage
            hit.GetComponent<PlayerHealth>()?.TakeDamage(damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, slashRange);
    }
}
