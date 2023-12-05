using System;

namespace Game.Tasks.EnergyCore
{
    public class StartEnergyCoreTask : TimerTask
    {
        public int finishedEnergyCoreCounter;
        public EnergyCoreCollision energyCoreCollision1;
        public EnergyCoreCollision energyCoreCollision2;
        public EnergyCoreCollision energyCoreCollision3;
        public EnergyCoreCollision energyCoreCollision4;
        public EnergyCoreCollision energyCoreCollision5;
        public EnergyCoreCollision energyCoreCollision6;
        public EnergyCoreCollision energyCoreCollision7;
        
        public StartEnergyCoreTask() : base(initialTimerTime : 70f, taskName: "Energy Core", taskDescription: "Energy Core",
            integrityValue : 10)
        {
        }

        public override void Initialize()
        {
            energyCoreCollision1.startEnergyCoreTaskScript = this;
            energyCoreCollision2.startEnergyCoreTaskScript = this;
            energyCoreCollision3.startEnergyCoreTaskScript = this;
            energyCoreCollision4.startEnergyCoreTaskScript = this;
            energyCoreCollision5.startEnergyCoreTaskScript = this;
            energyCoreCollision6.startEnergyCoreTaskScript = this;
            energyCoreCollision7.startEnergyCoreTaskScript = this;
            
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
            return finishedEnergyCoreCounter >= 7 ? TaskState.Successful : TaskState.Ongoing;
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
