using System.Collections;
using TMPro;
using UnityEngine;

public class phase_2_manager : MonoBehaviour
{

    Blood_Attacks manager;
    teleporter tele;
    public bool attackLoop;
    public float cooldown;
    private Coroutine loopCourtine;

    [Header("Health Settings")]
    public int maxHealth = 10;
    private int currentHealth;
    public TextMeshProUGUI healthUI;

    void Start()
    {
        manager = GetComponent<Blood_Attacks>();
        tele = GetComponent<teleporter>();
        tele.toggleTP = true;
        currentHealth = maxHealth;
        if (loopCourtine == null)
        {
            loopCourtine = StartCoroutine(RepeatAction());
        }
    }

    void Update()
    {
        if (!attackLoop) StopCoroutine(loopCourtine);
    }
    void random_attack()
    {
        int randInt = Random.Range(0, 3);
        if (randInt == 0)
        {
            StartCoroutine(arrow_attack());
        }
        else if (randInt == 1) manager.circle_attack();
        else manager.beam_attack();
    }
    IEnumerator RepeatAction()
    {
        while (true)
        {
            random_attack();
            yield return new WaitForSeconds(cooldown);
        }
    }

    IEnumerator arrow_attack()
    {
        int arrowCount = 0;
        while (arrowCount < 3)
        {
            manager.arrow_strike();
            arrowCount++;
            yield return new WaitForSeconds(0.1f);
        }
        yield break;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthUI.text = currentHealth.ToString("F0");
        if (currentHealth <= 0)
        {
            FindFirstObjectByType<DraculaPhaseManager>()?.OnDraculaDeath();
            gameObject.SetActive(false);
        }
    }

    void OnEnable()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }
    
    void UpdateHealthUI()
    {
        healthUI.text = currentHealth.ToString("F0");
    }

}
