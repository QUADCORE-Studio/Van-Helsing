using UnityEngine;
using UnityEngine.InputSystem;

public class Fireplace : MonoBehaviour
{
    public GameObject flameVisual;
    private bool isLit = false;
    private bool playerInRange = false;
    private PlayerControls controls;

    void Awake()
    {
        controls = new PlayerControls();
        controls.Player.Interact.performed += ctx => TryToggleFire();
    }

    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }

    void Start()
    {
        FireplaceManager.Instance.Register(this);
        SetLitState(false);
    }

    public void TryToggleFire()
    {
         Debug.Log("E was pressed – Interact action performed!");
        if (playerInRange)
        {
            if (isLit)
                Extinguish();
            else
                LightFire();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Player entered fireplace zone");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("Player left fireplace zone");
        }
    }

    public void LightFire() => SetLitState(true);
    public void Extinguish() => SetLitState(false);

    void SetLitState(bool state)
    {
        isLit = state;
        if (flameVisual != null)
            flameVisual.SetActive(state);
    }

    public bool IsLit() => isLit;
}
