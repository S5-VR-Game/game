using UnityEngine;

namespace Game.Tasks.MedicalDisaster
{
    /// <summary>
    /// Test class to rotate the valve game object based on the rotation of the rotator game object
    /// </summary>
    public class ValveRotator : MonoBehaviour
    {
        [SerializeField] private Transform rotator;
        [SerializeField] private Valve valve;
        
        private Quaternion m_LastRotation;

        private void Update()
        {
            var currentRotation = rotator.rotation;
            var delta = currentRotation * Quaternion.Inverse(m_LastRotation);
            
            // rotate valve game object by delta rotation   
            valve.transform.Rotate(new Vector3(delta.eulerAngles.x, 0, 0));
            
            m_LastRotation = currentRotation;
        }
    }
}