using UnityEngine;
using UnityEngine.InputSystem;

public class Blood_Attacks : MonoBehaviour
{
    public Animator animator;
    public InputAction circle_input;
    public InputAction arrow_input;
    public InputAction beam_input;
    public GameObject arrow_prefab;
    public GameObject beam_prefab;
    public int arrow_circle_count = 8;
    public float projectileSpeed;
    public GameObject target;
    private void OnEnable()
    {
        circle_input.Enable();
        arrow_input.Enable();
        beam_input.Enable();

        circle_input.performed += circle_attack;
        arrow_input.performed += arrow_strike;
        beam_input.performed += beam_attack;
    }

    private void OnDisable()
    {
        circle_input.Disable();
        arrow_input.Disable();
        beam_input.Disable();

        circle_input.performed -= circle_attack;
        arrow_input.performed -= arrow_strike;
        beam_input.performed -= beam_attack;
    }

    public void OnAnimationEnd()
    {
        Destroy(gameObject);
        Debug.Log("destroying.", gameObject);
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
       
    }
    // shouts out arrow_circle_count amount of arrows in 360 deg around parent(dracula)
    void circle_attack(InputAction.CallbackContext context)
    {
        float angleStep = 360f / arrow_circle_count;

        for (int i = 0; i < arrow_circle_count; i++)
        {
            float angle = i * angleStep;
            float rad = angle * Mathf.Deg2Rad;

            Vector2 direction = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;

            GameObject arrow = Instantiate(arrow_prefab, transform.position, Quaternion.identity);
            arrow.transform.right = direction;

            Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
            rb.linearVelocity = direction * projectileSpeed;

            Destroy(arrow, 2f);
        }

    }
    void beam_attack(InputAction.CallbackContext context)
    {
        Vector2 direction = (target.transform.position - transform.position).normalized;
        GameObject beam = Instantiate(beam_prefab, transform.position, Quaternion.identity);
        beam.transform.right = direction.normalized;
        
        Animator anim = beam.GetComponent<Animator>();
        anim.SetTrigger("beam");
        // Destroy(beam, 3f);
    }
    void arrow_strike(InputAction.CallbackContext context)
    {
        Vector2 direction = (target.transform.position - transform.position).normalized;

       
        GameObject arrow = Instantiate(arrow_prefab, transform.position, Quaternion.identity);
        arrow.transform.right = direction;

        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>(); 
        rb.linearVelocity = direction * projectileSpeed;
        Debug.Log(rb.linearVelocity);

        Destroy(arrow, 3f);


        
    }
}
