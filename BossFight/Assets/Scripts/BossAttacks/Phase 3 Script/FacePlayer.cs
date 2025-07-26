using UnityEngine;

public class FacePlayer : MonoBehaviour
{

    public Transform player;
    private Vector3 initialScale;

    void Awake()
    {
        initialScale = transform.localScale;
    }

    void Update()
    {
        if (player == null) return;
        bool left = player.position.x < transform.position.x;
        Vector3 s = initialScale;
        s.x = left ? -Mathf.Abs(initialScale.x) : Mathf.Abs(initialScale.x);
        transform.localScale = s;
    }
}
