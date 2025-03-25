using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.SDK3.Components;
using UnityEngine.Serialization;

namespace JanSharp
{
    [RequireComponent(typeof(VRCPickup))]
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class MovementGrip : UdonSharpBehaviour
    {
        public Transform toMove;
        public bool allowMovementOnX;
        public bool allowMovementOnY;
        public bool allowMovementOnZ;
        [FormerlySerializedAs("maxRightDeviation")]
        public float maxPositiveXDeviation = float.PositiveInfinity;
        [FormerlySerializedAs("maxLeftDeviation")]
        public float maxNegativeXDeviation = float.PositiveInfinity;
        [FormerlySerializedAs("maxUpDeviation")]
        public float maxPositiveYDeviation = float.PositiveInfinity;
        [FormerlySerializedAs("maxDownDeviation")]
        public float maxNegativeYDeviation = float.PositiveInfinity;
        [FormerlySerializedAs("maxForwardDeviation")]
        public float maxPositiveZDeviation = float.PositiveInfinity;
        [FormerlySerializedAs("maxBackDeviation")]
        public float maxNegativeZDeviation = float.PositiveInfinity;

        [HideInInspector] [SingletonReference] public UpdateManager updateManager;
        [HideInInspector] public VRC_Pickup pickup;
        [HideInInspector] public Vector3 targetInitialLocalPosition;
        [HideInInspector] public Vector3 thisInitialLocalPosition;
        [HideInInspector] public Quaternion rotationOffsetFromTargetToThis;
        [HideInInspector] public Vector3 positionOffsetFromTargetToThis;
        // for UpdateManager
        private int customUpdateInternalIndex;

        private bool isMovingAccordingToAPI = false;

        public UdonSharpBehaviour[] listeners;

        private float nextSyncTime;
        private const float SyncInterval = 0.2f;
        private const float LerpDuration = SyncInterval + 0.1f;
        [UdonSynced] [HideInInspector] public Vector3 syncedPosition;
        private float lastReceivedTime;
        private Vector3 lerpStartPosition;
        private bool receiving;
        private bool Receiving
        {
            get => receiving;
            set
            {
                receiving = value;
                pickup.pickupable = !value;
                if (value)
                    pickup.Drop();
            }
        }
        private bool currentlyHeld;
        private bool currentlyHeldInVR;
        private VRCPlayerApi.TrackingDataType trackingDataType;
        private Vector3 originPositionFromHand;
        private bool CurrentlyHeld
        {
            get => currentlyHeld;
            set
            {
                currentlyHeld = value;
                if (value)
                {
                    currentlyHeldInVR = Networking.LocalPlayer.IsUserInVR();
                    if (currentlyHeldInVR)
                    {
                        trackingDataType = pickup.currentHand == VRC_Pickup.PickupHand.Left
                            ? VRCPlayerApi.TrackingDataType.LeftHand
                            : VRCPlayerApi.TrackingDataType.RightHand;
                        originPositionFromHand = Networking.LocalPlayer.GetTrackingData(trackingDataType).position
                            - this.transform.parent.TransformVector(this.transform.localPosition - thisInitialLocalPosition);
                    }
                }
            }
        }

        /// <summary>
        /// Returns a position in world space.
        /// </summary>
        private Vector3 GetSnappedPosition() => toMove.position + toMove.TransformVector(positionOffsetFromTargetToThis);

        /// <summary>
        /// Returns a rotation in world space.
        /// </summary>
        private Quaternion GetSnappedRotation() => toMove.rotation * rotationOffsetFromTargetToThis;

        private void SnapBack() => this.transform.SetPositionAndRotation(GetSnappedPosition(), GetSnappedRotation());

        public override void OnPickup()
        {
            Networking.SetOwner(Networking.LocalPlayer, this.gameObject);
            nextSyncTime = 0f;
            Receiving = false;
            CurrentlyHeld = true;
            updateManager.Register(this);
            DispatchOnBeginMovement();
        }

        public override void OnDrop()
        {
            CurrentlyHeld = false;
            RequestSerialization();
            SnapBack();
            updateManager.Deregister(this);
            DispatchOnEndMovement();
        }

        public void CustomUpdate()
        {
            if (Receiving)
            {
                float percent = (Time.time - lastReceivedTime) / LerpDuration;
                if (percent >= 1f)
                {
                    toMove.localPosition = syncedPosition;
                    Receiving = false;
                    updateManager.Deregister(this);
                    SnapBack();
                    DispatchOnEndMovement();
                    return;
                }
                toMove.localPosition = Vector3.Lerp(lerpStartPosition, syncedPosition, percent);
                return;
            }

            var worldVector = currentlyHeldInVR
                ? Networking.LocalPlayer.GetTrackingData(trackingDataType).position - originPositionFromHand
                : this.transform.parent.TransformVector(this.transform.localPosition - thisInitialLocalPosition);
            var localVector = toMove.parent.InverseTransformVector(worldVector);

            localVector = ClampLocalVector(localVector);

            toMove.localPosition = targetInitialLocalPosition + localVector;

            syncedPosition = targetInitialLocalPosition + localVector;
            if (Time.time >= nextSyncTime)
            {
                RequestSerialization();
                nextSyncTime = Time.time + SyncInterval;
            }
        }

        private Vector3 ClampLocalVector(Vector3 localVector)
        {
            if (allowMovementOnX)
                localVector.x = Mathf.Clamp(localVector.x, -maxNegativeXDeviation, maxPositiveXDeviation);
            else
                localVector.x = 0;

            if (allowMovementOnY)
                localVector.y = Mathf.Clamp(localVector.y, -maxNegativeYDeviation, maxPositiveYDeviation);
            else
                localVector.y = 0;

            if (allowMovementOnZ)
                localVector.z = Mathf.Clamp(localVector.z, -maxNegativeZDeviation, maxPositiveZDeviation);
            else
                localVector.z = 0;

            return localVector;
        }

        public override void OnDeserialization()
        {
            Receiving = true;
            lastReceivedTime = Time.time;
            lerpStartPosition = toMove.localPosition;
            updateManager.Register(this);
            DispatchOnBeginMovement();
        }

        public void DispatchOnBeginMovement()
        {
            if (isMovingAccordingToAPI)
                return;
            isMovingAccordingToAPI = true;
            foreach (UdonSharpBehaviour listener in listeners)
                if (listener != null)
                    listener.SendCustomEvent("OnBeginMovement");
        }

        public void DispatchOnEndMovement()
        {
            if (!isMovingAccordingToAPI)
                return;
            isMovingAccordingToAPI = false;
            foreach (UdonSharpBehaviour listener in listeners)
                if (listener != null)
                    listener.SendCustomEvent("OnEndMovement");
        }

        /// <summary>
        /// <para>Begin a lerp from the <see cref="Transform.localPosition"/> of <see cref="toMove"/> to the
        /// given <paramref name="localPosition"/> and syncs it to all players.</para>
        /// <para>The given <paramref name="localPosition"/> gets clamped the same way movement is restricted
        /// when a player is moving the object using the pickup.</para>
        /// <para>If any player is currently holding the pickup, it gets dropped.</para>
        /// </summary>
        /// <param name="localPosition"></param>
        public void SetLocalPositionOfToMove(Vector3 localPosition)
        {
            Vector3 localVector = localPosition - targetInitialLocalPosition;
            localVector = ClampLocalVector(localVector);
            syncedPosition = targetInitialLocalPosition + localVector;
            Networking.SetOwner(Networking.LocalPlayer, this.gameObject);
            RequestSerialization();
            nextSyncTime = Time.time + SyncInterval;
            OnDeserialization(); // Make it lerp locally too.
        }
    }
}
