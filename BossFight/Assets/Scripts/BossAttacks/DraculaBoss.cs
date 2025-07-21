// using UnityEngine;

// public enum BossPhase
// {
//     Phase1,
//     Phase2,
//     Phase3
// }

// public class DraculaBoss : MonoBehaviour
// {
//     public BossPhase currentPhase;
//     private IDraculaPhase currentBehavior;

//     void Start()
//     {
//         SwitchPhase(BossPhase.Phase1);
//     }

//     void SwitchPhase(BossPhase phase)
//     {
//         currentPhase = phase;
//         switch (phase)
//         {
//             case BossPhase.Phase1:
//                 currentBehavior = new DraculaPhase1(this);
//                 break;
//             case BossPhase.Phase2:
//                 currentBehavior = new DraculaPhase2(this);
//                 break;
//             case BossPhase.Phase3:
//                 currentBehavior = new DraculaPhase3(this);
//                 break;
//         }
//     }

//     void Update()
//     {
//         currentBehavior?.Tick();
//     }

//     #if UNITY_EDITOR
// public void ForcePhaseSwitch(Phase phase)
// {
//     SwitchToPhase(phase);
// }
// #endif
// }
