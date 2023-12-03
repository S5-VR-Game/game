using UnityEngine;

namespace Game.Tasks.marble_gravity
{
    /// <summary>
    /// This class represents the MarbleGravityRiddle which can
    /// be done in the Unity-Game
    /// </summary>
    public class MarbleGravity : TimerTask
    {
        public GameObject taskPrefab;
        private bool _isFinished = false;
        
        
        public MarbleGravity() : base(120, "Marble Gravity :)", "solve the marble blyat", 10)
        {
            
        }

        public override void Initialize()
        {
            // no implementation required
        }

        protected override void BeforeStateCheck()
        {
            // no implementation required
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