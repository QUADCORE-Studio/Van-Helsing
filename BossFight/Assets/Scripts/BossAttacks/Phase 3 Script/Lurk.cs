using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Lurk : MonoBehaviour
{
    public float lurkSpeed = 10f;
    public float lurkDuration = 5f;
    
    private bool isLurking = false;
    private float lurkEndTime;
    private Transform player;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetPlayer(Transform playerTransform)
    {
        player = playerTransform;
    }

    public void StartLurking()
    {
        if (player == null) return;

        isLurking = true;
        lurkEndTime = Time.time + lurkDuration;
    }

    public void StopLurking()
    {
        isLurking = false;
        rb.linearVelocity = Vector2.zero;
    }

    void FixedUpdate()
    {
        if (!isLurking || player == null) return;

        // Move slowly toward the player
        Vector2 direction = (player.position - transform.position).normalized;
        Vector2 targetPos = rb.position + direction * lurkSpeed * Time.fixedDeltaTime;

        rb.MovePosition(targetPos);
        // Stop lurking after duration
        if (Time.time >= lurkEndTime)
        {
            StopLurking();
        }
    }
}
