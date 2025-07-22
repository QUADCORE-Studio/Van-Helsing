using UnityEngine;

public class beam_hitbox_controller : MonoBehaviour
{
    public Collider2D[] hitboxes;

    public void EnableCollider(int index)
    {
        for (int i = 0; i < hitboxes.Length; i++)
        {
            hitboxes[i].enabled = (i == index);
        }
    }

    public void DisableAllColliders()
    {
        foreach (var col in hitboxes)
            col.enabled = false;
    }
}
