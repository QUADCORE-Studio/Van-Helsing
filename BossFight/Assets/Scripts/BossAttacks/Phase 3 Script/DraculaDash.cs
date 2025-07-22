using System.Data.Common;
using UnityEngine;


public class DraculaDash : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isDashing = false;
    public float dashSpeed = 10f;
    public float dashCooldown = 2f; // Cooldown duration in seconds
    private float lastDashTime = -Mathf.Infinity;
    private Vector3 lastPlayerDirection;

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

    // Optionally, expose cooldown status
    public bool IsDashReady()
    {
        return Time.time >= lastDashTime + dashCooldown;
    }
}
