using System;
using UnityEngine;
using UnityEngine.XR;

namespace Game.Tasks.BookRiddle
{
    /// <summary>
    /// This class represents the book controller, which is there to
    /// open and close the book as soon as the player is near to it.
    /// </summary>
    public class BookController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        private bool _currentState;
        private const float HandDistance = 0.001f;
        private static readonly int OpenBook = Animator.StringToHash("openBook");

        [Obsolete("Obsolete")]
        private void Update()
        {
            CheckHandInput(XRNode.RightHand, KeyCode.JoystickButton0);
            CheckHandInput(XRNode.LeftHand, KeyCode.JoystickButton2);
        }

        /// <summary>
        /// Checks the hand input and does an book animation if necessary
        /// </summary>
        /// <param name="handNode">The XRNode of the hand you want to open the book with.</param>
        /// <param name="button">The Button you want to open the book with.</param>
        [Obsolete("Obsolete")]
        private void CheckHandInput(XRNode handNode, KeyCode button)
        {
            var handDevice = InputDevices.GetDeviceAtXRNode(handNode);

            if (!handDevice.isValid || !IsHandNear(handNode))
            {
                return;
            }
            if (Input.GetKeyUp(button))
            {
                SwitchAnimation();
            }
        }

        [Obsolete("Obsolete")]
        private bool IsHandNear(XRNode handNode)
        {
            var handPosition = InputTracking.GetLocalPosition(handNode);
            return Vector3.Distance(transform.position, handPosition) <= HandDistance;
        }

        /// <summary>
        /// Switches the Animation
        /// </summary>
        public void SwitchAnimation()
        {
            _currentState = !_currentState;
            animator.SetBool(OpenBook, _currentState);
        }
    }
}