using UnityEngine;

public class DraculaPhase3 : MonoBehaviour
{
    public enum DraculaAttackState
    {
        Idle,
        Dash
      //  Slash,
      //  Hypno
    }

    public Transform playerTransform;
    // private DraculaBoss boss;
    private DraculaDash draculaDash;
    private DraculaSlashAttack draculaSlash;
    private DraculaHypnoBeam draculaHypnoBeam;

    public DraculaAttackState currentState = DraculaAttackState.Idle;
    private float attackCooldown = 3f;
    private float nextAttackTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        draculaDash = GetComponent<DraculaDash>();

        nextAttackTime = Time.time + attackCooldown;
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
            case DraculaAttackState.Dash:
                PerformDash();
                break;
        }
    }
    void ChooseNextAttack()
    {
        currentState = (DraculaAttackState)Random.Range(1, 2); // Pick Dash, Slash, or Hypno
    }
    void PerformDash()
    {
        draculaDash.UpdatePlayerDirection(playerTransform.position);
        if (draculaDash.IsDashReady() && draculaDash.Dash())
        {
            Debug.Log("Dracula used Dash!");
            FinishAttack();
        }
    }
    void FinishAttack()
    {
        currentState = DraculaAttackState.Idle;
        nextAttackTime = Time.time + attackCooldown;
    }
}
