using UnityEngine;

public class BatAI : MonoBehaviour
{
    private Transform target;
    public float speed = 3f;

    public void SetTarget(Transform t)
    {
        target = t;
    }

    void Update()
    {
        if (target == null) return;

        Vector3 dir = (target.position - transform.position).normalized;
        transform.position += dir * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //damage player
        }
    }
}
