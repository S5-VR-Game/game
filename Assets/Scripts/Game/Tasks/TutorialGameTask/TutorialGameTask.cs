namespace Game.Tasks.TutorialGameTask
{
    /// <summary>
    /// This class represents the Tutorial Game Task
    /// the user has to perform once only.
    /// </summary>
    public class TutorialGameTask : GameTask
    {
        private const int CountNeeded = 5;
        private int _currentCount;
        
        public TutorialGameTask(string taskName, string taskDescription, int integrityValue = k_DefaultIntegrityValue) : base("Tutorial Aufgabe", "Jemand war anscheinend zu doof\n" +
            "und hat die Knöpfe nicht gedrückt. Drücke sie fünf Mal.", 20)
        {
            
        }

        public override void Initialize()
        {
            // no implementation needed.
        }

        protected override void BeforeStateCheck()
        {
            // no implementation needed.
        }

        protected override TaskState CheckTaskState()
        {
            return _currentCount != CountNeeded ? TaskState.Ongoing : TaskState.Successful;
        }

        // ReSharper disable Unity.PerformanceAnalysis
        protected override void AfterStateCheck()
        {
            if (currentTaskState != TaskState.Ongoing)
            {
                DestroyTask();
            }
        }

        /// <summary>
        /// Increments the count, you have to add this method to the button when it is pressed.
        /// </summary>
        public void IncrementCounter()
        {
            _currentCount++;
        }
    }
}