using UnityEngine;
using UnityEngine.InputSystem;

public class Blood_Attacks : MonoBehaviour
{
    public Animator animator;
    public InputAction inputTrig;

    private void OnEnable()
    {
        inputTrig.Enable();
    }

    private void OnDisable()
    {
        inputTrig.Disable();
    }

    void Start()
    {
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        if (inputTrig.WasPressedThisFrame())
        {
            animator.SetTrigger("beam_attack");
            Debug.Log(animator);
            Debug.Log("pressed e");
        }
    }
}
