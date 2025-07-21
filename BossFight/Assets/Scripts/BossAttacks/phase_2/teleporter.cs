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


    // teleports inside radius from centerPoint

    void Start()
    {
        
    }

    // Update is called once per frame
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
        Debug.Log("Random point:" + randomPoint);
        Vector2 newPos = new Vector2(centerPoint.position.x, centerPoint.position.y) + new Vector2(randomPoint.x, randomPoint.y);
        Debug.Log("Teleporting to " + newPos);
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
