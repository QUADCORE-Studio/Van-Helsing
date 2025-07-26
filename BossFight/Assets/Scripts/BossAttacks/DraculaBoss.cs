using UnityEngine;

public class DraculaBoss : MonoBehaviour
{
    public Animator animator;
    public Transform[] vulnerablePositions;
    public int maxHealth = 9;

    private IDraculaPhase[] phases;
    private int currentPhaseIndex = 0;
    private int health;
    private IDraculaPhase currentPhase;
    public BoxCollider2D[] spawnZones;
    public GameObject batPrefab;
    public Transform player;

    void Awake()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
            if (animator == null)
            {
                animator = GetComponentInChildren<Animator>();
                Debug.LogWarning("DraculaBoss: Animator found on child.");
            }
        }
    }
    void Start()
    {
        Debug.Log("Animator layers: " + animator.layerCount);
        health = maxHealth;
        phases = new IDraculaPhase[]
        {
            new Phase1(this, batPrefab, player),
            //new Phase2(this),
            //new Phase3(this)
        };

        SwitchToPhase(0);
    }

    void Update()
    {
        currentPhase?.Update();
    }

    public void TakeDamage()
    {
        health--;
        if (health <= 0)
        {
            animator.Play("ShadowSlash");
            // Death logic
            return;
        }

        // Example: advance phase every 3 hits
        if (health % 3 == 0)
        {
            SwitchToPhase(currentPhaseIndex + 1);
        }
    }

    void SwitchToPhase(int index)
    {
        if (currentPhase != null)
            currentPhase.Exit();

        currentPhaseIndex = Mathf.Clamp(index, 0, phases.Length - 1);
        currentPhase = phases[currentPhaseIndex];
        currentPhase.Enter();
    }

    public bool IsVulnerable()
    {
        if (currentPhase is Phase1 phase1)
        {
            return phase1.IsVulnerable();
        }
        return true;
    }
}