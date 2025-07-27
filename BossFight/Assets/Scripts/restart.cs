using UnityEngine;
using UnityEngine.SceneManagement;

public class restart : MonoBehaviour
{
     private PlayerControls controls;
    public string nextScene;

    void Awake()
    {
        controls = new PlayerControls();
        controls.Player.Interact.performed += ctx => SceneManager.LoadScene(nextScene);
    }

    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }

}
