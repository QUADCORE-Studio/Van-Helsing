using UnityEngine;
using UnityEngine.InputSystem;
public class teleporter : MonoBehaviour
{
    public Transform centerPoint;
    public float radius = 5f;
    public GameObject boss;

    public InputAction teleportAction;

    private void OnEnable()
    {
        teleportAction.Enable();
    }

    private void OnDisable()
    {
        teleportAction.Disable();
    }


    void Start()
    {
        centerPoint = transform;
    }

    void Update()
    {
        if (teleportAction.WasPressedThisFrame())
        {
            Teleport();
        }
    }

    void Teleport()
    {
        // random point inside the radius
        Vector2 randomPoint = Random.insideUnitCircle * radius;
        Vector2 newPos = new Vector2(centerPoint.position.x, centerPoint.position.y) + new Vector2(randomPoint.x, randomPoint.y);
        boss.transform.position = newPos;
    }
    void OnDrawGizmosSelected()
    {
        if (centerPoint != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(centerPoint.position, radius);
        }
    }
}
