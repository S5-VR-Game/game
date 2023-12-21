using System;
using PlayerController;
using UnityEngine;
using UnityEngine.Serialization;
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
        [SerializeField] private ParticleSystem valveSmokeParticleSystem;
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
        private ParticleSystem.MinMaxCurve m_InitialParticleEmissionRate;

        /// <summary>
        /// Determines the transform, which is used to rotate the valve
        /// </summary>
        /// <returns>right hand controller by default to rotate the valve</returns>
        private Transform GetRotationController()
        {
            return playerProfileService.GetRightVrController();
        }
        
        private void Start()
        {
            valveMeshRenderer.material.color = m_GradientStart;
            var particleSystemMain = valveSmokeParticleSystem.main;
            particleSystemMain.startColor = m_GradientStart;
            
            m_LastControllerRotation = GetRotationController().rotation;
            m_InitialParticleEmissionRate = valveSmokeParticleSystem.emission.rateOverTime;
            
            // build gradient
            m_ValveGradient = MedicalDisaster.BuildSimpleGradient(m_GradientStart, m_GradientMiddle, m_GradientEnd);
        }

        private void LateUpdate()
        {
            if (valve.isSelected)
            {
                var currentRotation = GetRotationController().rotation;
                // get rotation difference from last controller rotation
                var rotationDifference = Quaternion.Inverse(m_LastControllerRotation * Quaternion.Inverse(currentRotation));
                // update valve transform
                valve.transform.Rotate(new Vector3(rotationDifference.eulerAngles.z * ControllerRotationFactor, 0, 0));
            }
            CheckValveRotation();

            m_LastControllerRotation = GetRotationController().rotation;
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

            // linear reduce particle emission and speed according to progress
            var emission = valveSmokeParticleSystem.emission;
            var particleSystemMain = valveSmokeParticleSystem.main;
            emission.rateOverTime = m_InitialParticleEmissionRate.constant-m_InitialParticleEmissionRate.constant * m_CompleteRotationProgress;
                
            // apply current gradient value to valve material and particle color
            var currentGradientValue = m_ValveGradient.Evaluate(m_CompleteRotationProgress);
            valveMeshRenderer.material.color = currentGradientValue;
            particleSystemMain.startColor = currentGradientValue;
        }
    }
}