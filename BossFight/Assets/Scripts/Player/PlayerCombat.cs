using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    private PlayerControls controls;
    public Transform attackPoint;
    public GameObject slashPrefab;

    void Awake()
    {
        animator = GetComponent<Animator>();
        controls = new PlayerControls();
    }

    public void Attack()
    {
        animator.Play("AttackPlayer");

        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        mouseWorldPos.z = 0;

        Vector2 direction = (mouseWorldPos - attackPoint.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Instantiate(slashPrefab, attackPoint.position, Quaternion.Euler(0, 0, angle));

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


    //dodge logic

}
