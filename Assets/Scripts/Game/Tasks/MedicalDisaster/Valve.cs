using System;
using PlayerController;
using Unity.VRTemplate;
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
        public int requiredRotationCount = 3;
        public event Action OnValveRotationCompleted;

        private bool m_HalfRotation;
        private bool m_ValveRotationCompleted;
        private int m_RotationCount;
        [NonSerialized] public PlayerProfileService playerProfileService;

        private Quaternion lastControllerRotation;
        
        private readonly Color m_ClosedColor = Color.red;
        private readonly Color m_OpenedColor = Color.green;

        private const float ControllerRotationFactor = 2f;
        private const float RotationThreshold = 0.7f;

        private void Start()
        {
            valveMeshRenderer.material.color = m_ClosedColor;
            lastControllerRotation = playerProfileService.GetRightVrController().localRotation;
        }

        private void Update()
        {
            if (valve.isSelected)
            {
                // get rotations
                //var newRotation = playerProfileService.GetRightVrController().localRotation.eulerAngles;
                var rotationDifference = lastControllerRotation *
                                         Quaternion.Inverse(playerProfileService.GetRightVrController().localRotation);
                // update valve transform to match controller rotation
                valve.transform.Rotate(new Vector3(rotationDifference.eulerAngles.z * ControllerRotationFactor, 0, 0));

                CheckValveRotation();
            }

            lastControllerRotation = playerProfileService.GetRightVrController().localRotation;
        }

        private void CheckValveRotation()
        {
            var valveTransform = valve.transform;
            // check if half rotation is completed
            if (!m_HalfRotation && valveTransform.rotation.z <= -RotationThreshold)
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

        public void OnRotationChanged()
        {
            // TODO test if screw mechanic can be implemented using xr knob
            // print(valve.value);
            valveMeshRenderer.material.color = m_OpenedColor;
        }
    }
}