﻿using System;

namespace Game.Tasks.EnergyCore
{
    public class StartEnergyCoreTask : TimerTask
    {
        public int finishedEnergyCoreCounter;
        public EnergyCoreManager energyCoreCollision1;
        public EnergyCoreManager energyCoreCollision2;
        public EnergyCoreManager energyCoreCollision3;
        public EnergyCoreManager energyCoreCollision4;
        public EnergyCoreManager energyCoreCollision5;
        public EnergyCoreManager energyCoreCollision6;
        public EnergyCoreManager energyCoreCollision7;
        
        public StartEnergyCoreTask() : base(initialTimerTime : 70f, taskName: "Energy Core", taskDescription: "Energy Core",
            integrityValue : 10)
        {
        }

        public override void Initialize()
        {
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
