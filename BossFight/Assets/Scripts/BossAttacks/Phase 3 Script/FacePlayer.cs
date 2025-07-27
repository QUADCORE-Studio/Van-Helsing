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
        if (player.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(0.7f, 0.7f,0.7f);
        }
        else
        {
            transform.localScale = new Vector3(-0.7f, 0.7f,0.7f);
        }
    }
}
