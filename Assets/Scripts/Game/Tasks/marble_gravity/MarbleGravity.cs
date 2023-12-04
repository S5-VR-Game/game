using PlayerController;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR;

namespace Game.Tasks.marble_gravity
{
    /// <summary>
    /// This class represents the MarbleGravityRiddle which can
    /// be done in the Unity-Game
    /// </summary>
    public class MarbleGravity : TimerTask
    {
        public GameObject taskPrefab;
        public PlayerProfileService playerProfileService;
        private bool _isFinished;
        private const float RotationSpeed = 4.0f;

        public MarbleGravity() : base(120, "Marble Gravity :)", "solve the marble blyat", 10)
        {
            
        }

        public override void Initialize()
        {
            // no implementation required
        }

        protected override void BeforeStateCheck()
        {
            if (!InputDevices.GetDeviceAtXRNode(XRNode.RightHand).isValid)
            {
                return;
            }

            if (!(Vector3.Distance(playerProfileService.GetPlayerGameObject().transform.position,
                    transform.position) <= 1.0f)) return;
            if (Input.GetKeyDown(KeyCode.JoystickButton0))
            {
                transform.Rotate(Vector3.up, RotationSpeed * Time.deltaTime);
            }

            if (Input.GetKeyDown(KeyCode.JoystickButton1))
            {
                transform.Rotate(Vector3.up, RotationSpeed * Time.deltaTime);
            }
        }

        protected override TaskState CheckTaskState()
        {
            
            return _isFinished ? TaskState.Successful : TaskState.Ongoing;
        }

        protected override void AfterStateCheck()
        {
            if (currentTaskState != TaskState.Ongoing)
            {
                DestroyTask();
            }
        }

        public void SetFinished(bool newFinished)
        {
            _isFinished = newFinished;
        }
    }
}