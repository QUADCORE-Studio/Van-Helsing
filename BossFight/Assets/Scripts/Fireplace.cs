using UnityEngine;

public class Fireplace : MonoBehaviour
{
    public GameObject flameVisual;
    private bool isLit = false;

    void Start()
    {
        SetLitState(false);
        FireplaceManager.Instance.Register(this);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            LightFire();
        }
    }

    public void LightFire()
    {
        if (!isLit)
        {
            SetLitState(true);
            // Optional: play sound, VFX
        }
    }

    public void Extinguish()
    {
        SetLitState(false);
    }

    void SetLitState(bool state)
    {
        isLit = state;
        if (flameVisual != null)
            flameVisual.SetActive(state);
    }

    public bool IsLit() => isLit;
}
