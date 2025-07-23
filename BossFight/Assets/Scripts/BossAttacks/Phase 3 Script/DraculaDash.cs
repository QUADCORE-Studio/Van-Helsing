using System.Data.Common;
using UnityEngine;


public class DraculaDash : MonoBehaviour
{
    public Vector3 lastPlayerDirection;
    public float dashSpeed = 10f;
    public float dashCooldown = 2f;
    public bool isVulnerable = false;
    public float vulnerableDuration = 3f;// Cooldown duration in seconds
    public LayerMask pillarLayer;

    private Rigidbody2D rb;
    private bool isDashing = false;
    private float lastDashTime = -Mathf.Infinity;

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
        Debug.Log("CLOOIIIDS");
        if (((1 << collision.gameObject.layer) & pillarLayer) != 0 && isDashing)
        {
            isDashing = false;
            rb.linearVelocity = Vector2.zero;

            collision.gameObject.SetActive(false);

            BecomeVulnerable();
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
