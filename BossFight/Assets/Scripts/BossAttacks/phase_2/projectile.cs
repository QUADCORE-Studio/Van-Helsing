using UnityEngine;

public class projectile : MonoBehaviour
{
    public bool destroy_flag;
    public float damage;
    public void OnAnimationEnd()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
            if (destroy_flag)
            {
                Destroy(gameObject);
            }
        }
    }
   
}
