using UnityEngine;

public class Phase1 : IDraculaPhase
{
    private DraculaBoss boss;
    private float vulnerableTimer;
    private bool isVulnerable;
    private BoxCollider2D[] spawnZones;

    //bat stuff
    private GameObject batPrefab;
    private Transform player;
    private float teleportDelay = 5;
    private int batsPerTeleport = 2;

    public Phase1(DraculaBoss boss, GameObject batPrefab, Transform player)
    {
        this.boss = boss;
        this.spawnZones = boss.spawnZones;
        this.batPrefab = batPrefab;
        this.player = player;
    }

    public void Enter()
    {
        FireplaceManager.Instance.ExtinguishAll();
        if (boss.animator != null && boss.animator.isActiveAndEnabled)
        {
            boss.animator.Play("Appear");
        }
        isVulnerable = false;
    }

    public void Update()
    {
        if (!isVulnerable && FireplaceManager.Instance.AllLit())
        {
            TeleportToRandomSpawnZone();
            if (boss.animator != null && boss.animator.isActiveAndEnabled)
            {
                boss.animator.Play("Appear"); 
            }
            isVulnerable = true;
            vulnerableTimer = 5f;

        }
        else if (!isVulnerable && !FireplaceManager.Instance.AllLit())
        {
            teleportDelay -= Time.deltaTime;
            if (teleportDelay <= 0f)
            {
                TeleportToRandomSpawnZone();
                boss.animator.Play("Appear");
                boss.animator.Play("ShadowMouthOpen");
                SpawnBats();
                teleportDelay = 5f;
            }
        }

        if (isVulnerable)
        {
            vulnerableTimer -= Time.deltaTime;
            if (vulnerableTimer <= 0f)
            {
                Enter();
            }
        }

    }

    public void Exit()
    {
        // Cleanup if needed
    }

    public void TeleportToRandomSpawnZone()
    {
        if (spawnZones == null || spawnZones.Length == 0)
        {
            Debug.LogWarning("No spawn zones assigned.");
            return;
        }

        // Choose random zone
        BoxCollider2D zone = spawnZones[Random.Range(0, spawnZones.Length)];
        Bounds bounds = zone.bounds;

        // Choose random point within zone bounds
        Vector3 randomPos = new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            boss.transform.position.z // maintain Z
        );

        boss.transform.position = randomPos;

        Debug.Log("Dracula teleported to: " + randomPos);
    }

    private void SpawnBats()
    {
        for (int i = 0; i < batsPerTeleport; i++)
        {
            Vector3 spawnOffset = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
            GameObject bat = GameObject.Instantiate(batPrefab, boss.transform.position + spawnOffset, Quaternion.identity);

            BatAI batScript = bat.GetComponent<BatAI>();
            if (batScript != null && player != null)
            {
                batScript.SetTarget(player);
            }
        }
    }
}
