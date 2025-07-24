using UnityEngine;

public class Pillars : MonoBehaviour
{
    public Sprite brokenSprite;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    private bool isBroken = false;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void BreakPillar()
    {
        if (isBroken) return; // Prevent breaking again if already broken
        isBroken = true;
        spriteRenderer.sprite = brokenSprite;
        boxCollider.enabled = false;
        Debug.Log("Pillar has been broken!");
    }
}
