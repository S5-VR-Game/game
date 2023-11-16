using UnityEngine;

namespace MyPrefabs.Scripts.Game.Tasks
{
    public abstract class TimerTask : GameTask
    {
        protected float remainingTime;

        protected TimerTask(float initialTimerTime)
        {
            remainingTime = initialTimerTime;
        }

        private void Update()
        {
            if (remainingTime < 0 && currentTaskState == TaskState.Ongoing)
            {                    
                // IMPORTANT NOTE: If the time is up, there needs to be Failed in Future!!!

                UpdateTaskState(TaskState.Failed);
                DestroyGameObject();
            }
            else
            {
                // checking task in the future on failure, ongoing or just success
                UpdateTaskState(CheckTaskState());
                remainingTime -= Time.deltaTime;
            }
        }
    }
}