using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class video_manager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string nextSceneName;
    void Start()
    {
        // Start the video
        videoPlayer.Play();
    }

    void Update()
    {
        // Auto-load next scene when video finishes
        if (videoPlayer.isPrepared && !videoPlayer.isPlaying)
        {
            LoadNextScene();
        }
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
