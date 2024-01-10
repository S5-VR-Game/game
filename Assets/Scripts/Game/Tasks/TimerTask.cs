using UnityEngine;

namespace Game.Tasks
{
    public abstract class TimerTask : GameTask
    {
        protected float remainingTime;
        private bool _timerStarted;

        protected TimerTask(
            float initialTimerTime,
            string taskName,
            string taskDescription,
            GameTaskType taskType,
            int integrityValue = k_DefaultIntegrityValue
        ) : base(taskName: taskName, taskDescription: taskDescription, taskType: taskType, integrityValue: integrityValue)
        {
            remainingTime = initialTimerTime;
        }

        /// <summary>
        /// Determines to a certain condition whether the Timer
        /// Task should start or not
        ///
        /// It can deliver true by default, or be implemented by
        /// a subclass.
        /// </summary>
        /// <returns>true, if that's the case, false otherwise</returns>
        protected virtual bool TimerStartCondition()
        {
            return true;
        }

        /// <summary>
        /// Overriden update method for time limited tasks.
        /// </summary>
        protected sealed override void Update()
        {
            if (!_timerStarted)
            {
                _timerStarted = TimerStartCondition();
                return;
            }
            if (remainingTime < 0 && currentTaskState == TaskState.Ongoing)
            {                    
                // IMPORTANT NOTE: If the time is up, there needs to be Failed in Future!!!
                remainingTime = 0;
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
        
        public float GetRemainingTime()
        {
            return remainingTime;
        }
    }
}