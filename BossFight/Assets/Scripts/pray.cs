using UnityEngine;

public class pray : MonoBehaviour
{
    public GameObject praySign;
    private PlayerControls controls;

    void Awake()
    {
        controls = new PlayerControls();
        controls.Player.Interact.performed += ctx => prayed();
    }

    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }

    void prayed()
    {
        SpriteRenderer sr = praySign.GetComponent<SpriteRenderer>();
        Color newColor;
        if (ColorUtility.TryParseHtmlString("#BABABA", out newColor))
        {
            sr.color = newColor;
        }
        Destroy(praySign, 3f);
    }
}
