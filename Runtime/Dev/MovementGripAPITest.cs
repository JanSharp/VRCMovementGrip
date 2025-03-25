using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace JanSharp
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class MovementGripAPITest : UdonSharpBehaviour
    {
        public MovementGrip movementGrip;

        public override void Interact()
        {
            movementGrip.SetLocalPositionOfToMove(movementGrip.toMove.localPosition + new Vector3(
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f)));
        }
    }
}
