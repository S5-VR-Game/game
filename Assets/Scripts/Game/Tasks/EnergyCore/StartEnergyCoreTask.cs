using UnityEngine;

namespace Game.Tasks.EnergyCore
{
    public class StartEnergyCoreTask : TimerTask
    {
        public StartEnergyCoreTask(float initialTimerTime, string taskName, string taskDescription,
            int integrityValue = k_DefaultIntegrityValue) : base(initialTimerTime, taskName, taskDescription,
            integrityValue)
        {
        }

        public override void Initialize()
        {
        }

        protected override void BeforeStateCheck()
        {
        }

        protected override TaskState CheckTaskState()
        {
            return TaskState.Ongoing;
        }

        protected override void AfterStateCheck()
        {

        }
    }
}
