using UnityEngine;

public class DraculaSlashAttack : MonoBehaviour
{
    public int slashDamage = 10;
    public float slashDuration = 1.0f;
    public float slashRange = 2.5f;
    public float slashCooldown = 4f;

    public BoxCollider2D slashHitbox;
    public Animator animator;
    public DraculaDash draculaDash;

    private Transform player;
    private bool isSlashing = false;
    private float endTime;
    private float lastSlashTime = -Mathf.Infinity;

    void Start()
    {
        // Find the player by tag
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
    }

    void Update()
    {
        if (player == null) return;

        // 1. If we're in the middle of a slash, check for end
        if (isSlashing)
        {
            if (Time.time >= endTime)
                EndSlash();
            return;
        }

        // 2. If off cooldown and player in range, start a new slash
        bool offCooldown = Time.time >= lastSlashTime + slashCooldown;
        float dist = Vector2.Distance(transform.position, player.position);

        if (offCooldown && dist <= slashRange)
        {
            StartSlash();
        }
    }

    private void StartSlash()
    {
        isSlashing = true;
        endTime = Time.time + slashDuration;
        lastSlashTime = Time.time;

        // Play animation
        if (animator != null)
            animator.Play("Slash");

        // Enable hitbox
        if (slashHitbox != null)
            slashHitbox.enabled = true;
    }

    private void EndSlash()
    {
        isSlashing = false;

        // Disable hitbox
        if (slashHitbox != null)
            slashHitbox.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isSlashing) return;

        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
                playerHealth.TakeDamage(slashDamage);
        }
    }
}
