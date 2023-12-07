using System;
using PlayerController;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

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
        [SerializeField] private XRGrabInteractable valve;
        [NonSerialized] public PlayerProfileService playerProfileService;
        public int requiredRotationCount = 3;
        public event Action OnValveRotationCompleted;

        private const float ControllerRotationFactor = 2f;
        private const float RotationThreshold = 0.7f;

        private int m_RotationCount;
        private bool m_HalfRotation;
        private bool m_ValveRotationCompleted;
        private float m_CompleteRotationProgress;
        private Quaternion m_LastControllerRotation;
        
        private readonly Color m_GradientStart = Color.red;
        private readonly Color m_GradientMiddle = Color.yellow;
        private readonly Color m_GradientEnd = Color.green;
        private readonly Color m_FinishedColor = Color.white;
        private Gradient m_ValveGradient;

        private void Start()
        {
            valveMeshRenderer.material.color = m_GradientStart;
            m_LastControllerRotation = playerProfileService.GetRightVrController().localRotation;
            
            // build gradient
            m_ValveGradient = new Gradient();
            
            var colors = new GradientColorKey[3];
            colors[0] = new GradientColorKey(m_GradientStart, 0.0f);
            colors[1] = new GradientColorKey(m_GradientMiddle, 0.5f);
            colors[2] = new GradientColorKey(m_GradientEnd, 1.0f);

            var alphas = new GradientAlphaKey[2];
            alphas[0] = new GradientAlphaKey(1.0f, 0.0f);
            alphas[1] = new GradientAlphaKey(1.0f, 1.0f);

            m_ValveGradient.SetKeys(colors, alphas);
        }

        private void LateUpdate()
        {
            if (valve.isSelected)
            {
                var currentRotation = playerProfileService.GetRightVrController().localRotation;
                // get rotation difference from last controller rotation
                var rotationDifference = Quaternion.Inverse(m_LastControllerRotation * Quaternion.Inverse(currentRotation));
                // update valve transform
                valve.transform.Rotate(new Vector3(rotationDifference.eulerAngles.z * ControllerRotationFactor, 0, 0));

                CheckValveRotation();
            }

            m_LastControllerRotation = playerProfileService.GetRightVrController().localRotation;
        }

        /// <summary>
        /// Checks is the valve is rotated and invokes the <see cref="OnValveRotationCompleted"/> event, when rotation
        /// goal is reached.
        /// </summary>
        private void CheckValveRotation()
        {
            var valveTransform = valve.transform;
            // check if half rotation is completed
            if (!m_HalfRotation && valveTransform.rotation.z <= -RotationThreshold)
            {
                m_HalfRotation = true;
                OnHalfRotated();
            }
            else if (m_HalfRotation && valveTransform.rotation.z >= 0)
            {
                // full rotation completed
                m_HalfRotation = false;
                m_RotationCount++;
                OnHalfRotated();
                if (!m_ValveRotationCompleted && m_RotationCount >= requiredRotationCount)
                {
                    // valve rotation goal reached
                    m_ValveRotationCompleted = true;
                    OnValveRotationCompleted?.Invoke();
                    valveMeshRenderer.material.color = m_FinishedColor;
                    valve.enabled = false;
                }
            }
        }

        /// <summary>
        /// Called, when the valve is turned half a circle. Updates the internal progress value and applies
        /// current gradient value to valve color. 
        /// </summary>
        private void OnHalfRotated()
        {
            if (m_CompleteRotationProgress > 1)
            {
                return;
            }
            
            // add half circle to progress
            m_CompleteRotationProgress += 1f/(requiredRotationCount*2);

            // apply current gradient value
            valveMeshRenderer.material.color = m_ValveGradient.Evaluate(m_CompleteRotationProgress);
        }
    }
}