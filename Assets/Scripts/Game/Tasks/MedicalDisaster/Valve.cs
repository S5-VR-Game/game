using System;
using UnityEngine;

namespace Game.Tasks.MedicalDisaster
{
    /// <summary>
    /// Represents a valve, which can be rotated by the player. If rotated enough times, the valve is closed and the event
    /// <see cref="OnValveRotationCompleted"/> will be invoked and the valve will change its color.
    /// The rotation amount is determined by the field <see cref="requiredRotationCount"/>.
    /// </summary>
    public class Valve : MonoBehaviour
    {
        [SerializeField] private MeshRenderer valveMeshRenderer;
        [SerializeField] private Transform valveTransform;
        public int requiredRotationCount = 3;
        public event Action OnValveRotationCompleted;

        private bool m_HalfRotation;
        private bool m_ValveRotationCompleted;
        private int m_RotationCount;
        private Vector3 m_OldForward;

        private readonly Color m_ClosedColor = Color.red;
        private readonly Color m_OpenedColor = Color.green;
        private const float RotationThreshold = 0.7f;

        private void Start()
        {
            valveMeshRenderer.material.color = m_ClosedColor;
            m_OldForward = valveTransform.forward;
        }

        private void Update()
        {
            var forward = valveTransform.forward;
            Vector3 cross = Vector3.Cross(m_OldForward, forward);
            m_OldForward = forward;

            // check if half rotation is completed
            if (!m_HalfRotation && valveTransform.rotation.z <= -RotationThreshold && cross.z >= 0)
            {
                m_HalfRotation = true;
            }
            else if (m_HalfRotation && valveTransform.rotation.z >= 0)
            {
                // full rotation completed
                m_HalfRotation = false;
                m_RotationCount++;
                if (!m_ValveRotationCompleted && m_RotationCount >= requiredRotationCount)
                {
                    // valve rotation goal reached
                    m_ValveRotationCompleted = true;
                    OnValveRotationCompleted?.Invoke();
                    valveMeshRenderer.material.color = m_OpenedColor;
                }
            }
        }
        
        // private void LateUpdate()
        // {
        //     var currentTransform = valveTransform;
        //     currentTransform.position = m_LastTransform.position;
        //     var lastRotation = m_LastTransform.rotation;
        //     valveTransform.rotation.Set(currentTransform.rotation.x, lastRotation.y, lastRotation.z, lastRotation.w);
        //     m_LastTransform = currentTransform;
        // }
    }
}