using UnityEngine;

namespace Game.Tasks.MedicalDisaster
{
    public class Valve : MonoBehaviour
    {
        [SerializeField] private MeshRenderer valveMeshRenderer;
        [SerializeField] private int requiredRotationCount = 3;
        [SerializeField] private bool requireClockwiseRotation = true;

        private bool m_HalfRotation;
        private int m_RotationCount;
        private Vector3 m_OldForward;

        private void Start()
        {
            valveMeshRenderer.material.color = Color.red;
            m_OldForward = transform.forward;
        }

        private void Update()
        {
            Vector3 cross = Vector3.Cross(m_OldForward, transform.forward);
            m_OldForward = transform.forward;

            if (!m_HalfRotation && transform.rotation.x <= -0.9f && 
                (requireClockwiseRotation && cross.x < 0 || !requireClockwiseRotation && cross.x > 0))
            {
                m_HalfRotation = true;
            }
            else if (m_HalfRotation && transform.rotation.x >= 0)
            {
                print("full rotation");
                m_HalfRotation = false;
                m_RotationCount++;
                if (m_RotationCount >= requiredRotationCount)
                {
                    valveMeshRenderer.material.color = Color.green;
                }
            }
        }
        // private void LateUpdate()
        // {
        //     var currentTransform = transform;
        //     currentTransform.position = m_LastTransform.position;
        //     var lastRotation = m_LastTransform.rotation;
        //     transform.rotation.Set(currentTransform.rotation.x, lastRotation.y, lastRotation.z, lastRotation.w);
        //     m_LastTransform = currentTransform;
        // }
    }
}