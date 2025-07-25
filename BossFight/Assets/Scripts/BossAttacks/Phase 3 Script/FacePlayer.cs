using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    
    public Transform player;

    private Vector3 originalScale;

    private void Awake()
    {
        originalScale = transform.localScale;

        if (player == null)
        {
            var p = GameObject.FindWithTag("Player");
            if (p != null) player = p.transform;
        }
    }

    private void Update()
    {
        if (player == null) return;

        // Determine which direction to face
        bool faceLeft = player.position.x < transform.position.x;

        // Apply scale flip on X axis only
        var newScale = originalScale;
        newScale.x = Mathf.Abs(originalScale.x) * (faceLeft ? -1f : 1f);
        transform.localScale = newScale;
    }
}
