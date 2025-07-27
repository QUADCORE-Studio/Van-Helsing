using UnityEngine;
using UnityEngine.InputSystem;

public class Blood_Attacks : MonoBehaviour
{
    //prefabs
    public GameObject arrow_prefab;
    public GameObject beam_prefab;
    //config variables
    public int arrow_circle_count = 8;
    public float projectileSpeed = 5f;
    //attack target
    public GameObject target;

    // shouts out arrow_circle_count amount of arrows in 360 deg around parent(dracula)
    public void circle_attack()
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

            Destroy(arrow, 3f);
        }

    }
    public void beam_attack()
    {
        Vector2 direction = (target.transform.position - transform.position).normalized;
        GameObject beam = Instantiate(beam_prefab, transform.position, Quaternion.identity);
        beam.transform.right = direction.normalized;

        Animator anim = beam.GetComponent<Animator>();
        anim.SetTrigger("beam");
    }
    public void arrow_strike()
    {
        Vector2 direction = (target.transform.position - transform.position).normalized;


        GameObject arrow = Instantiate(arrow_prefab, transform.position, Quaternion.identity);
        arrow.transform.right = direction;

        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction * projectileSpeed;

        Destroy(arrow, 3f);
    }
  
}
