using UnityEngine;

public class DraculaPhase3 : MonoBehaviour
{
    public enum DraculaAttackState
    {
        Idle,
        Lurk,
        Dash
      //  Slash,
      //  Hypno
    }

    public Transform playerTransform;
    // private DraculaBoss boss;
    private DraculaDash draculaDash;
    private DraculaSlashAttack draculaSlash;
    private DraculaHypnoBeam draculaHypnoBeam;
    private Lurk draculaLurk;

    public DraculaAttackState currentState = DraculaAttackState.Idle;
    private float attackCooldown = 3f;
    private float nextAttackTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        draculaDash = GetComponent<DraculaDash>();
        draculaLurk = GetComponent<Lurk>();
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
            case DraculaAttackState.Lurk:
                PerformLurk();
                break;
            case DraculaAttackState.Dash:
                PerformDash();
                break;
        }
    }
    void ChooseNextAttack()
    {
        currentState = (DraculaAttackState)Random.Range(1, 3); // Pick Dash, Slash, or Hypno
    }
    void PerformDash()
    {
        draculaDash.UpdatePlayerDirection(playerTransform.position);
        if (draculaDash.IsDashReady() && draculaDash.Dash())
        {
            Debug.Log("Dracula used Dash!");
            Invoke(nameof(FinishAttack),3f);
        }
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
}
