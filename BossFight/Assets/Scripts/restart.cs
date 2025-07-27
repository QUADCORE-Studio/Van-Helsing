using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class restart : MonoBehaviour
{
     private PlayerControls controls;
    private float inputDelay = 1.5f;
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= inputDelay &&
            (Mouse.current.leftButton.wasPressedThisFrame ||
             (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)))
        {
            SceneManager.LoadScene("MapGeneration - Backup");
        }
    }
}
