using System.Data.Common;
using UnityEngine;


public class DraculaDash : MonoBehaviour
{
    public Vector3 lastPlayerDirection;
    public float dashSpeed = 40.6f;
    public float dashCooldown = 3f;
    public bool isVulnerable = false;
    public float vulnerableDuration = 5f;// Cooldown duration in seconds
    private float dashDamage= 20f;
    public LayerMask pillarLayer;

    private Rigidbody2D rb;
    private PlayerHealth playerHealth;
    private bool isDashing = false;
    private float lastDashTime = -Mathf.Infinity;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) playerHealth = player.GetComponent<PlayerHealth>(); // Get the PlayerHealth component from the player)
    }
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
            isDashing = true;
            lastDashTime = Time.time;
            return true; // Dash performed
        }
        return false; // Dash not performed due to cooldown
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("COOOOLLIIIDDDS");
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

    void BecomeVulnerable()
    {
        isVulnerable = true;
        Debug.Log("Dracula is vulnerable!");
        Invoke(nameof(RecoverFromVulnerability), vulnerableDuration);
    }

    void RecoverFromVulnerability()
    {
        isVulnerable = false;
        Debug.Log("Dracula recovered from vulnerability.");
    }
}
