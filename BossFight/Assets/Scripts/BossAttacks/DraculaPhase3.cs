using System.Transactions;
using TMPro;
using UnityEngine;

public class DraculaPhase3 : MonoBehaviour
{
    public enum DraculaAttackState
    {
        Idle,
        Lurk,
        Dash,
        Hypno
    }

    public Transform playerTransform;
    // private DraculaBoss boss;
    private DraculaDash draculaDash;
    private DraculaHypnoBeam draculaHypnoBeam;
    private Lurk draculaLurk;


    public Animator animator;
    public DraculaAttackState currentState = DraculaAttackState.Idle;
    private readonly float attackCooldown = 1.5f;
    private float nextAttackTime;

    [Header("Health Settings")]
    public TextMeshProUGUI healthUI;
    public int maxHealth = 10;
    private int currentHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
        draculaDash = GetComponent<DraculaDash>();
        draculaLurk = GetComponent<Lurk>();
        draculaHypnoBeam = GetComponent<DraculaHypnoBeam>();
        nextAttackTime = Time.time + attackCooldown;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthUI.text = currentHealth.ToString("F0");
        if (Time.time < nextAttackTime || playerTransform == null|| draculaDash.isVulnerable) return;
        switch (currentState)
        {
            case DraculaAttackState.Idle:
                ChooseNextAttack();
                break;
            case DraculaAttackState.Lurk:
                PerformLurk();
                break;
            case DraculaAttackState.Dash:
                PerformDash();
                break;
            case DraculaAttackState.Hypno:
                PerformHypnoBeam();
                break;

        }
    }
    void ChooseNextAttack()
    {
        if (draculaDash.isVulnerable)
        {
            currentState = DraculaAttackState.Idle; // If vulnerable, just wait
            return;
        }
        currentState = (DraculaAttackState)Random.Range(1, 4); // Pick Dash, Slash, or Hypno
        if (currentState == DraculaAttackState.Idle) currentState = DraculaAttackState.Lurk;
    }
    void PerformDash()
    {
        draculaDash.UpdatePlayerDirection(playerTransform.position);
        if (draculaDash.IsDashReady() && draculaDash.Dash())
        {
            Debug.Log("Dracula used Dash!");
            Invoke(nameof(FinishAttack), 1.5f);

        }

    }

    void PerformHypnoBeam()
    {
        animator.Play("BeamCharge");
        draculaHypnoBeam.ActivateBeam(playerTransform);
        Invoke(nameof(FinishAttack), 1.5f); // or match beam duration
    }
    void PerformLurk()
    {
        draculaLurk.SetPlayer(playerTransform);
        draculaLurk.StartLurking();
        Invoke(nameof(FinishLurk), 1.5f);
    }
    void FinishLurk()
    {
        draculaLurk.StopLurking();
        FinishAttack();
    }
    void FinishAttack()
    {
        currentState = DraculaAttackState.Idle;
        nextAttackTime = Time.time + attackCooldown;
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
