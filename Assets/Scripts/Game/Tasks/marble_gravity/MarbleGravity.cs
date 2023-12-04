using PlayerController;
using UnityEngine;
using UnityEngine.XR;

namespace Game.Tasks.marble_gravity
{
    /// <summary>
    /// This class represents the MarbleGravityRiddle which can
    /// be done in the Unity-Game
    /// </summary>
    public class MarbleGravity : TimerTask
    {
        public PlayerProfileService playerProfileService;
        public GameObject taskPrefab;
        private bool _isFinished = false;
        private const float RotationSpeed = 50.0f;
        private const float ControlDistance = 4.0f;

        public MarbleGravity() : base(120, "Marble Gravity :)", "solve the marble blyat", 10)
        {
            
        }

        public override void Initialize()
        {
            // no implementation required
        }

        protected override void BeforeStateCheck()
        {
            
            print(Vector3.Distance(playerProfileService.GetPlayerGameObject().transform.position, transform.position));
            if (Vector3.Distance(playerProfileService.GetPlayerGameObject().transform.position, transform.position) > ControlDistance)
            {
                print("greater");
                return;
            }
            if (!InputDevices.GetDeviceAtXRNode(XRNode.RightHand).isValid)
            {
                return;
            }
            if (Input.GetKey(KeyCode.JoystickButton0))
            {
                transform.Rotate(Vector3.up, RotationSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.JoystickButton1))
            {
                transform.Rotate(Vector3.up, -RotationSpeed * Time.deltaTime);
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