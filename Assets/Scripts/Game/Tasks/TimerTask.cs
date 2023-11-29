using UnityEngine;

namespace Game.Tasks
{
    public abstract class TimerTask : GameTask
    {
        protected float remainingTime;

        protected TimerTask(
            float initialTimerTime,
            string taskName,
            string taskDescription,
            int integrityValue = k_DefaultIntegrityValue
        ) : base(taskName: taskName, taskDescription: taskDescription, integrityValue: integrityValue)
        {
            remainingTime = initialTimerTime;
        }

        /// <summary>
        /// Overriden update method for time limited tasks.
        /// </summary>
        protected sealed override void Update()
        {
            if (remainingTime < 0 && currentTaskState == TaskState.Ongoing)
            {                    
                // IMPORTANT NOTE: If the time is up, there needs to be Failed in Future!!!
                UpdateTaskState(TaskState.Failed);
            }
            else
            {
                BeforeStateCheck();
                // checking task in the future on failure, ongoing or just success
                UpdateTaskState(CheckTaskState());
                remainingTime -= Time.deltaTime;
            }
            AfterStateCheck();
        }
    }
}