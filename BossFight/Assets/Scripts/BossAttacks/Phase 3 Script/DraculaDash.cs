using System.Collections;
using UnityEngine;


public class DraculaDash : MonoBehaviour
{
    public Vector3 lastPlayerDirection;
    public float dashSpeed = 35f;
    public float dashCooldown = 10f;
    public bool isVulnerable = false;
    public float vulnerableDuration = 10f;
    public LayerMask pillarLayer;
    public Animator animator;

    public Rigidbody2D rb;
    private PlayerHealth playerHealth;
    private bool isDashing = false;
    private float lastDashTime = -Mathf.Infinity;
    private float dashDamage= 10f;
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) playerHealth = player.GetComponent<PlayerHealth>(); // Get the PlayerHealth component from the player)
    }

    
    void Update()
    {
        if (isDashing)
        {
            rb.linearVelocity = lastPlayerDirection * dashSpeed;
        }
    }

    // Call this method to update the direction towards the player
    public void UpdatePlayerDirection(Vector3 playerPosition)
    {
        lastPlayerDirection = (playerPosition - transform.position).normalized;
    }

    // Call this method to perform the dash
    public bool Dash()
    {
        if (Time.time >= lastDashTime + dashCooldown && lastPlayerDirection != Vector3.zero)
        {
            animator.Play("Charge");
            isDashing = true;
            lastDashTime = Time.time;
            return true; // Dash performed
        }
        return false; // Dash not performed due to cooldown
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & pillarLayer) != 0 && isDashing)
        {
            isDashing = false;
            rb.linearVelocity = Vector2.zero;

            Pillars pillarScript = collision.gameObject.GetComponent<Pillars>();
            if (pillarScript != null)
            {
                pillarScript.BreakPillar();
            }
            BecomeVulnerable();

        }
        if (isDashing && !isVulnerable && collision.gameObject.CompareTag("Player")&& playerHealth !=null)
        {
            playerHealth.TakeDamage(dashDamage);
        }
    }
    
    public bool IsDashReady()
    {
        return Time.time >= lastDashTime + dashCooldown;
    }

    public void BecomeVulnerable()
    {
        // If we’re already in the middle of vulnerability, bail out
        if (isVulnerable) return;

        StartCoroutine(VulnerabilityRoutine());
    }

    private IEnumerator VulnerabilityRoutine()
    {
        isVulnerable = true;
        Debug.Log("Dracula is now vulnerable!");

        // (Optional) If you want to log a countdown every second:
        float remaining = vulnerableDuration;
        while (remaining > 0f)
        {
            Debug.Log($"Dracula vulnerable for {remaining:F0} more seconds");
            yield return new WaitForSeconds(1f);
            remaining -= 1f;
        }

        // End vulnerability
        isVulnerable = false;
        Debug.Log("Dracula has recovered and is no longer vulnerable.");
        // (Optional) play a “get up” or idle animation here:
        if (animator != null)
            animator.Play("Recover");
    }

    // void RecoverFromVulnerability()
    // {
    //     isVulnerable = false;
    //     Debug.Log("Dracula recovered from vulnerability.");
    // }
}
