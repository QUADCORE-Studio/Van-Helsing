using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class angel : MonoBehaviour
{
    public float targetY = -.4f;
    public float smoothTime = 0.3f;
    private float velocityY = 0f;
    public bool toggle;
    public string nextScene;

    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public string message = "Take this sword, follower.";
    public float textSpeed = 0.05f;

    private bool messagePlaying = false;

    void Update()
    {
        if (toggle)
        {
            Vector2 currentPosition = transform.position;
            Debug.Log($"Moving down - CurrentY: {currentPosition.y}, TargetY: {targetY}");

            if (Mathf.Abs(currentPosition.y - targetY) > 0.01f)
            {
                float newY = Mathf.SmoothDamp(currentPosition.y, targetY, ref velocityY, smoothTime);
                transform.position = new Vector2(currentPosition.x, newY);
            }
            else if (!messagePlaying)
            {
                toggle = false;
                messagePlaying = true;
                StartCoroutine(PlayMessage());
            }
        }
    }


    IEnumerator PlayMessage()
    {
        dialoguePanel.SetActive(true);
        dialogueText.text = "";

        foreach (char c in message)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        yield return new WaitForSeconds(2f);
        dialoguePanel.SetActive(false);
        SceneManager.LoadScene(nextScene);
    }
}
