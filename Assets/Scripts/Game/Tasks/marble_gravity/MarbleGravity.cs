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
        private const string SphereName = "sphere";
        private const string GoalName = "goal";

        private Collider _sphereCollider;
        private Collider _goalCollider;
        
        
        public MarbleGravity(float initialTimerTime, string taskName, string taskDescription, int integrityValue = k_DefaultIntegrityValue) : base(initialTimerTime, taskName, taskDescription, integrityValue)
        {
            // no implementation required
        }

        public override void Initialize()
        {
            _sphereCollider = GetSphereCollider();
            _goalCollider = GetGoalCollider();
        }

        protected override void BeforeStateCheck()
        {
            // no implementation required
        }

        protected override TaskState CheckTaskState()
        {
            if (IsFinished())
            {
                return TaskState.Successful;
            }

            return TaskState.Ongoing;
        }

        protected override void AfterStateCheck()
        {
            if (currentTaskState != TaskState.Ongoing)
            {
                DestroyTask();
            }
        }

        /// <summary>
        /// Determines whether the Marble Gravity game is finished or not
        /// </summary>
        /// <returns>true, if that is the case, false otherwise</returns>
        private bool IsFinished()
        {
            return _sphereCollider.bounds.Intersects(_goalCollider.bounds);
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private Collider GetSphereCollider()
        {
            return taskPrefab.transform.Find(SphereName).gameObject.GetComponent<Collider>();
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private Collider GetGoalCollider()
        {
            return taskPrefab.transform.Find(GoalName).gameObject.GetComponent<Collider>();
        }
    }
}