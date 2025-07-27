using UnityEngine;

public class DraculaPhaseManager : MonoBehaviour
{
    [Header("Drag all Dracula phase GameObjects here")]
    public GameObject[] draculas;

    private int currentIndex = 0;

    void Start()
    {
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
            Debug.Log("🧛 All Dracula phases defeated!");
        }
        // add in scene transition
    }

    private void ActivateDracula(int index)
    {
        for (int i = 0; i < draculas.Length; i++)
        {
            draculas[i].SetActive(i == index); // Only one active at a time
        }

        Debug.Log($"🩸 Activated Dracula Phase {index + 1}");
    }
}
