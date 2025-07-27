using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    private PlayerControls controls;
    public Transform attackPoint;
    public GameObject slashPrefab;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        animator = GetComponent<Animator>();
        controls = new PlayerControls();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private float lastAttackTime = 0f;
    public float attackCooldown = 1f;
    public void Attack()
    {
        if (Time.time - lastAttackTime < attackCooldown)
            return;

        lastAttackTime = Time.time;
        animator.Play("AttackPlayer");

        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        mouseWorldPos.z = 0;

        Vector2 direction = (mouseWorldPos - attackPoint.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Flip player depending on direction
        if (direction.x < 0)
        {
            Debug.Log("Flipping player to face left");
            spriteRenderer.flipX = true;
        }
        else
        {
            Debug.Log("Flipping player to face right");
            spriteRenderer.flipX = false;
        }
        float offsetDistance = 1.0f;

        // Calculate offset position in attack direction
        Vector3 offset = direction * offsetDistance;
        Vector3 spawnPosition = attackPoint.position + offset;

        GameObject slash = Instantiate(slashPrefab, spawnPosition, Quaternion.Euler(0, 0, angle));
        Destroy(slash, 0.5f);

    }

    void OnEnable()
    {
        controls.Enable();
        controls.Player.Attack.performed += ctx => Attack();
    }

    void OnDisable()
    {
        controls.Player.Attack.performed -= ctx => Attack();
        controls.Disable();
    }
}
