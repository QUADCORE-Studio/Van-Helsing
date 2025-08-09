using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeTitle : MonoBehaviour
{
    public void ChangeScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("praying");
    }
}
