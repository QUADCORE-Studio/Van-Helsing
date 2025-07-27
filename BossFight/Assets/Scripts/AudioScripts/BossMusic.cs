using System.Collections;
using UnityEngine;

public class BossMusic : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] private AudioSource backgroundSource;

    [Header("Audio Clips")]
    public AudioClip background;
    public AudioClip background2;

    private void Start()
    {
        // Make sure we don’t loop the first track
        backgroundSource.loop = false;
        StartCoroutine(PlayBackgroundSequence());
    }

    private IEnumerator PlayBackgroundSequence()
    {
        // Play the first clip
        backgroundSource.clip = background;
        backgroundSource.Play();

        // Wait exactly for its length
        yield return new WaitForSeconds(background.length);

        backgroundSource.clip = background2;
        backgroundSource.loop = true; 
        backgroundSource.Play();
    }
}