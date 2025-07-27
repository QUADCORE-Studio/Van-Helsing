using System.Transactions;
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
    private float attackCooldown = 1.5f;
    private float nextAttackTime;

    [Header("Health Settings")]
    public int maxHealth = 5;
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
        if (Time.time < nextAttackTime || playerTransform == null) return;
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
            Invoke(nameof(FinishAttack), 3f);

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
        Invoke(nameof(FinishLurk), 3f);
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
        Debug.Log("Dracula Phase 3 took damage! Current HP: " + currentHealth);
        if (currentHealth <= 0)
        {
            Debug.Log("Phase 3 Dracula defeated!");
            FindFirstObjectByType<DraculaPhaseManager>()?.OnDraculaDeath();
            gameObject.SetActive(false);
        }
    }
}
