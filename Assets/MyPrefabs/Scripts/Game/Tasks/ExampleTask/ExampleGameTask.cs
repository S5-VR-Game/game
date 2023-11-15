using UnityEngine;

namespace MyPrefabs.Scripts.Game.Tasks.ExampleTask
{
    /// <summary>
    /// Example game task for proof-of-concept and demo purpose
    ///
    /// Completes and removes itself after 5 seconds
    /// </summary>
    public class ExampleGameTask : GameTask
    {
        private float remainingTime = 5f;
        private void Update()
        {
            if (remainingTime < 0 && !isCompleted)
            {
                CompleteTask();
                DestroyGameObject();
            }
            else
            {
                remainingTime -= Time.deltaTime;
            }
        }

        public override void Initialize()
        {
            
        }
    }
}