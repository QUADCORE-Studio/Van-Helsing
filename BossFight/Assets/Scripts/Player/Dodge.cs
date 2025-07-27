using UnityEngine;

public class Dodge : MonoBehaviour
{
    [Tooltip("How hard the burst is")]
    public float dodgeForce = 15f;

    private Rigidbody2D rb;
    public Animator animator;
    private PlayerControls controls;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        controls = new PlayerControls();
    }

    private void OnEnable()
    {
        controls.Player.Enable();

        // When Dodge is pressed, call DoDodge
        controls.Player.Dodge.performed += _ => DoDodge();
    }

    private void OnDisable()
    {
        controls.Player.Dodge.performed -= _ => DoDodge();
        controls.Player.Disable();
    }

    private void DoDodge()
    {
        animator.Play("PlayerRoll");
        // Read your move vector at the instant of dodge
        Vector2 moveDir = controls.Player.Move.ReadValue<Vector2>();
        if (moveDir.sqrMagnitude < 0.01f) return;  // no dodge if you're not moving
        Collider2D collider = GetComponent<Collider2D>();
        collider.enabled = false; // Disable collider during dodge to avoid collisions
        // Ensure the collider is not null before using it
        // Apply an instantaneous impulse
        rb.AddForce(moveDir.normalized * dodgeForce, ForceMode2D.Impulse);
        Invoke(nameof(ReEnableCollider), 0.2f); // Re-enable collider after dodge duration
    }
    private void ReEnableCollider()
    {
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = true; // Re-enable the collider after dodge
        }
    }
}
