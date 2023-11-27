using System;
using UnityEngine;
using UnityEngine.XR;
using Vector3 = UnityEngine.Vector3;

namespace PlayerController
{
    /// <summary>
    /// This class represents the Movement of the
    /// VR Player while shaking VR Controllers
    /// </summary>
    public class VRShakeMovement : MonoBehaviour
    {
        [SerializeField] public CharacterController vrCharacterController;
        
        [SerializeField] public XRNode inputSource;
        [SerializeField] public float shakeThreshold = 0.2f;
        [SerializeField] public float movementSpeed = 3.0f;

        private Vector3 _lastControllerPosition;

        [Obsolete("Obsolete")]
        private void Start()
        {
            _lastControllerPosition = InputTracking.GetLocalPosition(inputSource);
        }

        [Obsolete("Obsolete")]
        private void Update()
        {
            if (Vector3.Distance(_lastControllerPosition, InputTracking.GetLocalPosition(inputSource)) > shakeThreshold)
            {
                MoveForward();
            }
        }

        /// <summary>
        /// moves the vr player forward by one unit.
        /// </summary>
        private void MoveForward()
        {
            vrCharacterController.transform.Translate(Vector3.forward * (movementSpeed * Time.deltaTime));
        }
    }
}