using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public Animator animator;
    public float moveSpeed = 5f;
    private Vector2 moveInput;
    private Rigidbody2D rb;
    private PlayerControls controls;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        controls = new PlayerControls();
    }

    private void OnEnable()
    {
        controls.Player.Enable();
        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;
    }

    private void OnDisable()
    {
        controls.Player.Move.performed -= ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled -= ctx => moveInput = Vector2.zero;

        controls.Player.Disable();
    }

    private void FixedUpdate()
    {
        animator.SetBool("isWalking", true);
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
        if (moveInput.sqrMagnitude < 0.01f)
        {
            animator.SetBool("isWalking", false);
        }
    }
}
