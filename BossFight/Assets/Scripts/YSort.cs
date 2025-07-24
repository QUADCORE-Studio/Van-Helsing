using UnityEngine;

[System.Serializable]
public class SortingOffset
{
    public SpriteRenderer renderer;
    public int offset = 1; // how much higher/lower it sorts relative to parent
}

public class YSort : MonoBehaviour
{
    [Header("Main Renderer (base of object)")]
    public SpriteRenderer mainRenderer;

    [Header("Additional Renderers (children or layers)")]
    public SortingOffset[] additionalRenderers;

    void LateUpdate()
    {
        //calculate base sorting order from world Y
        int baseOrder = Mathf.RoundToInt(-transform.position.y * 100);

        if (mainRenderer != null)
            mainRenderer.sortingOrder = baseOrder;

        //apply sorting offsets for other children or items
        foreach (var item in additionalRenderers)
        {
            if (item.renderer != null)
                item.renderer.sortingOrder = baseOrder + item.offset;
        }
    }
}
