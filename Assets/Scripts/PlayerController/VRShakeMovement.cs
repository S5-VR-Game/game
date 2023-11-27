using System;
using System.Numerics;
using UnityEngine;
using UnityEngine.XR;
using Vector3 = UnityEngine.Vector3;

namespace PlayerController
{
    public class VRShakeMovement : MonoBehaviour
    {
        public XRNode inputSource;
        public float shakeThreshold = 0.2f;
        public float movementSpeed = 3.0f;

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

        private void MoveForward()
        {
            transform.Translate(Vector3.forward * (movementSpeed * Time.deltaTime));
        }
    }
}