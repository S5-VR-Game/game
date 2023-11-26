using System.Collections.Generic;
using UnityEngine;

namespace Game.Tasks.ExampleMultipleObjectsTask
{
    /// <summary>
    /// Example game task for proof-of-concept and demo purpose
    ///
    /// Completes and removes itself after 7 seconds
    /// </summary>
    public class ExampleMultipleObjectsTask : TimerTask
    {
        public List<GameObject> customSpawnedObjects = new List<GameObject>();

        public ExampleMultipleObjectsTask() : base(initialTimerTime:7f)
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
                foreach (var obj in customSpawnedObjects)
                {
                    Destroy(obj);
                }
                DestroyGameObject();
            }
        }
    }
}