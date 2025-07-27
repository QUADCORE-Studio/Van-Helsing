using UnityEngine;

public class WinMusic : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] private AudioSource backgroundSource;

    [Header("Audio Clips")]
    public AudioClip background;

    private void Start()
    {
        // Make sure we don’t loop the first track
        backgroundSource.clip = background;
        backgroundSource.Play();
    }
}
