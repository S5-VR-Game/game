using System;
using Unity.VRTemplate;
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
        [SerializeField] private XRKnob valve;
        public int requiredRotationCount = 3;
        public event Action OnValveRotationCompleted;

        private bool m_ValveRotationCompleted;
        private int m_RotationCount;

        private readonly Color m_ClosedColor = Color.red;
        private readonly Color m_OpenedColor = Color.green;

        private void Start()
        {
            valveMeshRenderer.material.color = m_ClosedColor;
        }

        public void OnRotationChanged()
        {
            // TODO test if screw mechanic can be implemented using xr knob
            print(valve.value);
            valveMeshRenderer.material.color = m_OpenedColor;
        }
    }
}