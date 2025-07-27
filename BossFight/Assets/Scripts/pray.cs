using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pray : MonoBehaviour
{
    public GameObject praySign;
    private PlayerControls controls;
    private Coroutine bounceCoroutine;
    public string nextSceneName;
    public float duration = 5f;
    public GameObject angelGO;

    void Awake()
    {
        controls = new PlayerControls();
        controls.Player.Interact.performed += ctx => prayed();
    }

    void OnEnable()
    {
        controls.Enable();
        // Start the bounce animation when enabled
        bounceCoroutine = StartCoroutine(BounceLoop());
    }

    void OnDisable()
    {
        controls.Disable();
    }

    void prayed()
    {
        // Stop the bounce coroutine
        if (bounceCoroutine != null)
        {
            StopCoroutine(bounceCoroutine);
        }

        // Final color change before destroy
        SpriteRenderer sr = praySign.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            Color finalColor;
            ColorUtility.TryParseHtmlString("#BABABA", out finalColor);
            sr.color = finalColor;
        }

        Destroy(praySign, 0.1f);
        // LoadNextScene();
        angel angelRef = angelGO.GetComponent<angel>();
        angelRef.toggle = true;
        
    }
    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
    IEnumerator BounceLoop()
    {
        SpriteRenderer sr = praySign.GetComponent<SpriteRenderer>();

        if (sr == null)
        {
            Debug.LogWarning("praySign has no SpriteRenderer.");
            yield break;
        }

        Vector3 originalPos = praySign.transform.localPosition;
        Vector3 downPos = originalPos + new Vector3(0, -0.1f, 0);

        Color originalColor = sr.color;
        Color bounceColor;
        ColorUtility.TryParseHtmlString("#BABABA", out bounceColor);


        while (true)
        {
            float elapsed = 0f;

            // Move down and change color
            while (elapsed < duration)
            {
                float t = elapsed / duration;
                praySign.transform.localPosition = Vector3.Lerp(originalPos, downPos, t);
                sr.color = Color.Lerp(originalColor, bounceColor, t);
                elapsed += Time.deltaTime;
                yield return null;
            }

            praySign.transform.localPosition = downPos;
            sr.color = bounceColor;

            elapsed = 0f;

            // Move up and restore color
            while (elapsed < duration)
            {
                float t = elapsed / duration;
                praySign.transform.localPosition = Vector3.Lerp(downPos, originalPos, t);
                sr.color = Color.Lerp(bounceColor, originalColor, t);
                elapsed += Time.deltaTime;
                yield return null;
            }

            praySign.transform.localPosition = originalPos;
            sr.color = originalColor;

            yield return new WaitForSeconds(0.2f); // Pause between bounces
        }
    }
}
