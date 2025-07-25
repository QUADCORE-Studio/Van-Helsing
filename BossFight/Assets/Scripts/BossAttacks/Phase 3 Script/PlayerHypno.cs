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
    public void BeginHypnosis(float duration, float requiredMashes, Vector3 draculaPosition)
    {
        if (isHypnotized) return;

        isHypnotized = true;
        mashCount = 0;
        requiredMashCount = Mathf.RoundToInt(requiredMashes);
        endTime = Time.time + duration;
        draculaTarget = new GameObject("DraculaTarget").transform;
        draculaTarget.position = draculaPosition;
        Debug.Log("Player is hypnotized!");
    }

    void Update()
    {
        if (!isHypnotized) return;

        // Player moves slowly toward Dracula
        Vector3 direction = (draculaTarget.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

        // Escape input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            mashCount++;
            if (mashCount >= requiredMashCount)
            {
                EndHypnosis();
                Debug.Log("Player broke free!");
            }
        }

        // Time runs out — fail
        if (Time.time >= endTime && isHypnotized)
        {
            EndHypnosis();
            Debug.Log("Player failed to escape. Dracula slashes!");
            // You could call player.GetComponent<PlayerHealth>().TakeDamage() here
        }
    }

    void EndHypnosis()
    {
        isHypnotized = false;
        Destroy(draculaTarget.gameObject);
    }
}
