using UnityEngine;

public class Pillars : MonoBehaviour
{
    public Sprite brokenSprite;
    private SpriteRenderer spriteRenderer;

    private bool isBroken = false;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void BreakPillar()
    {
        if (isBroken) return; // Prevent breaking again if already broken
        isBroken = true;
        spriteRenderer.sprite = brokenSprite;
        this.gameObject.layer = LayerMask.NameToLayer("Default");
        Debug.Log("Pillar has been broken!");
    }
}
