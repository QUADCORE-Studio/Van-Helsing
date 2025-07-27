using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class video_manager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string nextSceneName;
    private bool hasStarted = false;

    void Start()
    {
        videoPlayer.Prepare();
        videoPlayer.prepareCompleted += OnVideoPrepared;
    }
    void OnVideoPrepared(VideoPlayer vp)
    {
        videoPlayer.Play();
        hasStarted = true;
    }
    void Update()
    {
        if (hasStarted && !videoPlayer.isPlaying)
        {
            LoadNextScene();
        }
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
