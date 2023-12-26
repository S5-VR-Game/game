using UnityEngine.Serialization;

namespace Game.Tasks.marble_gravity
{
    /// <summary>
    /// This class constructs a MarbleGravity instance
    /// only in given spawn points with transform and rotation.
    /// </summary>
    public class MarbleGravityFactory : GameTaskFactory<TaskSpawnPoint>
    {
        public MarbleGravity easyMarble;
        public MarbleGravity mediumMarble;
        public MarbleGravity hardMarble;
        
        // ReSharper disable Unity.PerformanceAnalysis
        protected override GameTask CreateTask(TaskSpawnPoint spawnPoint)
        {
            var transform1 = spawnPoint.transform;
            var correctMarble = CalculateMarbleAccordingToDifficulty();
            var riddle = Instantiate(correctMarble.gameObject, transform1.position, transform1.rotation);
            var task = riddle.GetComponent<MarbleGravity>();
            return task;
        }

        private MarbleGravity CalculateMarbleAccordingToDifficulty()
        {
            return mDifficulty.GetSeparatedDifficulty() switch
            {
                SeparatedDifficulty.Easy => easyMarble,
                SeparatedDifficulty.Medium => mediumMarble,
                SeparatedDifficulty.Hard => hardMarble,
                _ => easyMarble
            };
        }
    }
}