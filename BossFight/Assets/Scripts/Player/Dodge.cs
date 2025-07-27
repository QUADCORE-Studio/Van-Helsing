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

        // Apply an instantaneous impulse
        rb.AddForce(moveDir.normalized * dodgeForce, ForceMode2D.Impulse);
    }
}
