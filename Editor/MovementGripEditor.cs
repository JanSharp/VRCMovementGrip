using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.SDK3.Components;
using UnityEditor;
using UdonSharpEditor;

namespace JanSharp
{
    [InitializeOnLoad]
    public static class MovementGripOnBuild
    {
        static MovementGripOnBuild() => JanSharp.OnBuildUtil.RegisterType<MovementGrip>(OnBuild);

        private static bool OnBuild(MovementGrip movementGrip)
        {
            var updateManager = GameObject.Find("/UpdateManager")?.GetComponent<UpdateManager>();
            if (updateManager == null)
            {
                Debug.LogError("MovementGrip requires a GameObject that must be at the root of the scene "
                    + "with the exact name 'UpdateManager' which has the 'UpdateManager' UdonBehaviour.",
                    movementGrip);
                return false;
            }

            SerializedObject movementGripProxy = new SerializedObject(movementGrip);
            movementGripProxy.FindProperty(nameof(movementGrip.pickup)).objectReferenceValue = movementGrip.GetComponent<VRCPickup>();
            movementGripProxy.FindProperty(nameof(movementGrip.updateManager)).objectReferenceValue = updateManager;
            movementGripProxy.FindProperty(nameof(movementGrip.targetInitialLocalPosition)).vector3Value = movementGrip.toMove.localPosition;
            movementGripProxy.FindProperty(nameof(movementGrip.thisInitialLocalPosition)).vector3Value = movementGrip.transform.localPosition;
            movementGripProxy.FindProperty(nameof(movementGrip.syncedPosition)).vector3Value = movementGrip.targetInitialLocalPosition;
            Vector3 localOffset = movementGrip.toMove.InverseTransformVector(movementGrip.transform.position - movementGrip.toMove.position);
            movementGripProxy.FindProperty(nameof(movementGrip.positionOffsetFromTargetToThis)).vector3Value = localOffset;
            Quaternion rotationOffset = Quaternion.Inverse(movementGrip.toMove.rotation) * movementGrip.transform.rotation;
            movementGripProxy.FindProperty(nameof(movementGrip.rotationOffsetFromTargetToThis)).quaternionValue = rotationOffset;
            movementGripProxy.ApplyModifiedProperties();

            /*
            * SnapBack is not necessary, because if we add all the operations together that would happen to the rotation within
            * this OnBuild function we get this (assigning to local rotation since that would happen in when snapping back):
            * movementGrip.localRotation = Quaternion.Inverse(movementGrip.parent.rotation)
            * * (
            *     toMove.rotation
            *     * (
            *         Quaternion.Inverse(toMove.rotation)
            *         * movementGrip.rotation
            *     )
            * )
            * Which is actually just a no op... assuming that foo * (Inverse(foo) * bar) == bar...
            * I'm not sure if the order of operations messes that up, but that's how the system has been working and it appears correct.
            */

            // Editor scripting version of SnapBack():

            // // Must do this after applying changes to the movement grip because get snapped position/rotation
            // // use the "position/rotation offset from target to this" fields.
            // SerializedObject transformProxy = new SerializedObject(movementGrip.transform);
            // transformProxy.FindProperty("m_LocalPosition").vector3Value
            //     = EditorUtil.WorldToLocalPosition(movementGrip.transform, movementGrip.GetSnappedPosition());
            // transformProxy.FindProperty("m_LocalRotation").quaternionValue
            //     = EditorUtil.WorldToLocalRotation(movementGrip.transform, movementGrip.GetSnappedRotation());
            // transformProxy.ApplyModifiedProperties();

            return true;
        }
    }
}
