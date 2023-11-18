namespace MyPrefabs.Scripts.Game.Tasks.ExampleTimerTask
{
    /// <summary>
    /// Example game task for proof-of-concept and demo purpose
    ///
    /// Completes and removes itself after 5 seconds
    /// </summary>
    public class ExampleTimerGameTask : TimerTask
    {

        public ExampleTimerGameTask() : base(initialTimerTime:5f)
        {
        }
        
        public override void Initialize()
        {
            
        }

        protected override void BeforeStateCheck()
        {
            
        }
        
        /// <summary>
        /// Checks the Task state by specific Conditions from the Task
        ///
        /// IMPORTANT NOTE: Returns ONGOING, but it needs to be implemented
        /// </summary>
        /// <returns>The State whether it is ONGOING, FAILED or SUCCESSFUL</returns>
        protected override TaskState CheckTaskState()
        {
            return TaskState.Ongoing;
        }

        protected override void AfterStateCheck()
        {
            if (currentTaskState != TaskState.Ongoing)
            {
                DestroyGameObject();
            }
        }
    }
}