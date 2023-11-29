using System;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Serialization;
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
        [SerializeField] public Transform direction;
        
        [SerializeField] public XRNode rightController;
        [SerializeField] public XRNode leftController;
        [SerializeField] public float shakeThreshold = 0.2f;
        [SerializeField] public float movementSpeed = 3.0f;

        private Vector3 _lastControllerLeftPosition;
        private Vector3 _lastControllerRightPosition;

        [SerializeField] public float timer = 0.2f;
        private float _defaultTimer;

        private void Start()
        {
            _defaultTimer = timer;
        }

        [Obsolete]
        private void Update()
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                if (Vector3.Distance(_lastControllerRightPosition, InputTracking.GetLocalPosition(rightController)) > shakeThreshold
                    || Vector3.Distance(_lastControllerLeftPosition, InputTracking.GetLocalPosition(leftController)) > shakeThreshold)
                {
                    MoveForward();
                }
            }
            else
            {
                _lastControllerRightPosition = InputTracking.GetLocalPosition(rightController);
                _lastControllerLeftPosition = InputTracking.GetLocalPosition(leftController);
                timer = _defaultTimer;
            }

            
        }

        /// <summary>
        /// moves the vr player forward by one unit of time and speed
        /// </summary>
        private void MoveForward()
        {
            vrCharacterController.Move(direction.forward * (movementSpeed * Time.deltaTime));
        }
    }
}