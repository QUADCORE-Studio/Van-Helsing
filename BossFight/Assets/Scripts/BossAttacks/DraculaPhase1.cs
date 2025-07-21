// using UnityEngine;

// public class DraculaPhase1 : IDraculaPhase
// {
//     private DraculaBoss boss;
//     private bool isVulnerable;
//     private float damageTaken;
//     public float damageBeforeReset = 10f;

//     public DraculaPhase1(DraculaBoss b)
//     {
//         boss = b;
//         StartCycle();
//     }

//     void StartCycle()
//     {
//         isVulnerable = false;
//         damageTaken = 0;
//         boss.SetShadowForm(true);
//         FireplaceManager.Instance.ExtinguishAll();
//         BatSpawner.Instance.StartSpawning();
//     }

//     public void Tick()
//     {
//         if (!isVulnerable && FireplaceManager.Instance.AllLit())
//         {
//             EnterVulnerableState();
//         }
//     }

//     void EnterVulnerableState()
//     {
//         isVulnerable = true;
//         boss.SetShadowForm(false);
//         boss.TeleportToRandomPoint();
//         BatSpawner.Instance.StopSpawning();
//     }

//     public void OnBossDamaged(float dmg)
//     {
//         if (!isVulnerable) return;

//         damageTaken += dmg;
//         if (damageTaken >= damageBeforeReset)
//         {
//             StartCycle(); // Restart cycle
//         }
//     }
// }
