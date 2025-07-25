using UnityEngine;

public class DraculaSlashAttack : MonoBehaviour
{
    public GameObject Slash;
    public BoxCollider2D slashHitbox;
    public Animator animator;
    public int slashDamage = 20;
    public float slashDuration = 1.0f;

    private bool isSlashing = false;
    private float endTime;

    private void Start()
    {
        if (slashHitbox != null)
            slashHitbox.enabled = false;
    }

    private void Update()
    {
        if (isSlashing && Time.time >= endTime)
        {
            EndSlash();
        }
    }

    public bool StartSlash()
    {
        if (isSlashing) return false;
        Slash.SetActive(true);
        isSlashing = true;
        endTime = Time.time + slashDuration;
        animator.Play("Slash");
        if (animator != null)
            animator.SetTrigger("Slash");

        if (slashHitbox != null)
            slashHitbox.enabled = true;

        return true;
    }

    private void EndSlash()
    {
        Slash.SetActive(false);
        isSlashing = false;
        if (slashHitbox != null)
            slashHitbox.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isSlashing) return;

        if (collision.CompareTag("Player"))
        {
            PlayerHealth player = collision.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(slashDamage);
            }
        }
    }

    public bool IsSlashReady()
    {
        return !isSlashing;
    }
}
