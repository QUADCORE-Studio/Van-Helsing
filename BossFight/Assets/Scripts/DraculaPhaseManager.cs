using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DraculaPhaseManager : MonoBehaviour
{
    [Header("Drag all Dracula phase GameObjects here")]
    public GameObject[] draculas;
    public TextMeshProUGUI phaseText;

    private int currentIndex = 0;

    void Start()
    {
        phaseText.text = "Phase " + currentIndex + 1;
        ActivateDracula(currentIndex);
    }

    public void OnDraculaDeath()
    {
        draculas[currentIndex].SetActive(false);
        currentIndex++;

        if (currentIndex < draculas.Length)
        {
            ActivateDracula(currentIndex);
        }
        else
        {
        }
        if (currentIndex == draculas.Length)
        {
            SceneManager.LoadScene("win");
        }
    }

    private void ActivateDracula(int index)
    {
        for (int i = 0; i < draculas.Length; i++)
        {
            draculas[i].SetActive(i == index); 
            phaseText.text = "Phase " + (currentIndex + 1);
        }

    }
}
