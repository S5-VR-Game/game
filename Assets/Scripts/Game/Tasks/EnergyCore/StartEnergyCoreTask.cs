using System;

namespace Game.Tasks.EnergyCore
{
    // control-script of the energycore-task
    public class StartEnergyCoreTask : TimerTask
    {
        public int finishedEnergyCoreCounter;
        
        public StartEnergyCoreTask() : base(initialTimerTime : 70f, taskName: "Energy Core", taskDescription: "Energy Core",
            integrityValue : 10)
        {
        }

        public override void Initialize()
        {
            // sets the starting time depending on the used difficulty
            remainingTime = difficulty.GetSeparatedDifficulty() switch
            {
                SeparatedDifficulty.Easy => 90f,
                SeparatedDifficulty.Medium => 70f,
                SeparatedDifficulty.Hard => 50f,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        protected override void BeforeStateCheck()
        {
        }

        protected override TaskState CheckTaskState()
        {
            // 6 = all cells are placed in the right core
            return finishedEnergyCoreCounter >= 6 ? TaskState.Successful : TaskState.Ongoing;
        }

        protected override void AfterStateCheck()
        {
            if (currentTaskState != TaskState.Ongoing)
            {
                DestroyTask();
            }
        }
    }
}
