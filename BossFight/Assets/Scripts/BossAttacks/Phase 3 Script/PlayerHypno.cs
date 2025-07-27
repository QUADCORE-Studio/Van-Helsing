using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHypno : MonoBehaviour
{
    private bool isHypnotized = false;
    private int mashCount = 0;
    private int requiredMashCount;
    private float endTime;
    private Transform draculaTarget;
    private PlayerControls controller;

    public float moveSpeed = 1.5f;
    private void Awake()
    {
        controller = new PlayerControls();
        controller.Player.Mash.performed += ctx => OnMash();
    }
    void OnEnable()
    {
        controller.Player.Enable();
    }

    void OnDisable()
    {
        controller.Player.Disable();
    }
    public void BeginHypnosis(float duration, float requiredMashes, Vector3 draculaPosition)
    {
        isHypnotized = true;
        mashCount = 0;

        requiredMashCount = Mathf.RoundToInt(requiredMashes);
        endTime = Time.time + duration;

        if (draculaTarget == null)
            draculaTarget = new GameObject("DraculaTarget").transform;
        draculaTarget.position = draculaPosition;
    }

    void Update()
    {
        if (!isHypnotized) return;

        // Player moves slowly toward Dracula
        Vector3 direction = (draculaTarget.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

        // Time runs out — fail
        if (Time.time >= endTime && isHypnotized)
        {
            EndHypnosis(false);
            Debug.Log("Player failed to escape. Dracula slashes!");
            // You could call player.GetComponent<PlayerHealth>().TakeDamage() here
        }
    }
    void OnMash()
    {
        if (!isHypnotized) return;

        mashCount++;
        if (mashCount >= requiredMashCount)
        {
            EndHypnosis(true); // Escaped
        }
    }
    void EndHypnosis(bool escaped)
    {
        isHypnotized = false;
        if (draculaTarget) Destroy(draculaTarget.gameObject);

        if (!escaped)
        {
            Debug.Log("Player failed to escape. Dracula slashes!");
            GetComponent<PlayerHealth>().TakeDamage(12); // or whatever value
        }
    }
}
