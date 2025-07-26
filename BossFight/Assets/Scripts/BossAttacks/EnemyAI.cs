using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("General Settings")]
    public float speed = 3f;
    public float maxHealth = 3f;
    public float damage = 1f;

    [Header("Optional")]
    public bool followPlayer = true;

    private float currentHealth;
    private Transform target;
    private PlayerHealth playerHealth;

    void Awake()
    {
        currentHealth = maxHealth;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform;
            playerHealth = player.GetComponent<PlayerHealth>();
        }
    }

    void Update()
    {
        if (followPlayer && target != null)
        {
            Vector3 dir = (target.position - transform.position).normalized;
            transform.position += dir * speed * Time.deltaTime;
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log(gameObject.name + " took damage: " + currentHealth);

        if (currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }
    }
}
