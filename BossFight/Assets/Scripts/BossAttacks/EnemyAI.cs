using UnityEngine;
using UnityEngine.Rendering;

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
    private Animator animator;

    void Awake()
    {
        this.enabled = false;
        currentHealth = maxHealth;

        animator = GetComponent<Animator>();

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
            Vector3 dir = target.position - transform.position;
            float distance = dir.magnitude;

            // Flip direction
            if (dir.x != 0)
            {
                Vector3 scale = transform.localScale;
                scale.x = -Mathf.Sign(dir.x) * Mathf.Abs(scale.x);
                transform.localScale = scale;
            }

            if (distance > 1.2f)
            {
                // Walk
                Vector3 moveDir = dir.normalized;
                transform.position += moveDir * speed * Time.deltaTime;

                animator?.SetFloat("Speed", 1f);
                animator?.SetFloat("Attack", 0f); // stop attacking
            }
            else
            {
                // Attack loop while close
                animator?.SetFloat("Speed", 0f);
                animator?.SetFloat("Attack", 1f); // keep attacking
            }
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

    [SerializeField] private float damageInterval = 1f;
    private float lastDamageTime = -999f;

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && playerHealth != null)
        {
            if (Time.time - lastDamageTime >= damageInterval)
            {
                playerHealth.TakeDamage(damage);
                lastDamageTime = Time.time;
            }
        }
    }

    private RoomTrigger myRoom;
    public void SetRoom(RoomTrigger room)
    {
        myRoom = room;
        if (room == null)
            this.enabled = true;

    }

    public bool IsInRoom(RoomTrigger room) => myRoom == room;
    public void Activate() => this.enabled = true;







}
