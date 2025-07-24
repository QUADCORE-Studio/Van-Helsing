using UnityEngine;
using UnityEngine.InputSystem;

public class Blood_Attacks : MonoBehaviour
{
    public Animator animator;
    public InputAction inputTrig;
    public GameObject arrow;
    public GameObject beam;
    public int projectileCount = 8;
    public float projectileSpeed = 5f;
    private Transform target;
    private void OnEnable()
    {
        inputTrig.Enable();
    }

    private void OnDisable()
    {
        inputTrig.Disable();
    }

    void Start()
    {
        
        animator = GetComponent<Animator>();
        target = transform;
    }

    void Update()
    {
        if (inputTrig.WasPressedThisFrame())
        {
            circle_attack();
        }
        if (inputTrig.WasPressedThisFrame())
        {
            beam_attack();
        }
    }

    void circle_attack()
    {
        float angleStep = 360f / projectileCount;

        for (int i = 0; i < projectileCount; i++)
        {
            float angle = i * angleStep;
            float rad = angle * Mathf.Deg2Rad;

            Vector2 direction = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;

            GameObject projectile = Instantiate(arrow, target.position, Quaternion.identity);
            projectile.transform.right = direction;

            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.linearVelocity = direction * projectileSpeed;

            Destroy(projectile, 2f);
        }

    }
    void beam_attack()
    {
        Vector2 direction = (target.transform.position - transform.position).normalized;
        GameObject projectile = Instantiate(beam, target.position, Quaternion.identity);
        projectile.transform.right = direction;

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction * projectileSpeed;

        Destroy(projectile, 3f);
    }
}
