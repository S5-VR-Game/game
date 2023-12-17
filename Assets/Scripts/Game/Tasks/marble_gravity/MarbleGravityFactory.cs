using UnityEngine;

namespace Game.Tasks.marble_gravity
{
    /// <summary>
    /// This class constructs a MarbleGravity instance
    /// only in given spawn points with transform and rotation.
    /// </summary>
    public class MarbleGravityFactory : GameTaskFactory<TaskSpawnPoint>
    {
        public MarbleGravity marbleGravityEasy;
        public MarbleGravity marbleGravityMedium;
        public MarbleGravity marbleGravityHard;
        
        // ReSharper disable Unity.PerformanceAnalysis
        protected override GameTask CreateTask(TaskSpawnPoint spawnPoint)
        {
            var transform1 = spawnPoint.transform;
            var taskPrefab = GetRightMarbleGravityAccordingToDifficulty();
            var riddle = Instantiate(taskPrefab, transform1.position, transform1.rotation);
            var task = riddle.GetComponent<MarbleGravity>();
            return task;
        }

        /// <summary>
        /// Calculates the Prefab that needs to be spawned and returns it.
        /// </summary>
        /// <returns>The Marble-Prefab that needs to be spawned</returns>
        private GameObject GetRightMarbleGravityAccordingToDifficulty()
        {
            var o = marbleGravityEasy.gameObject;
            return mDifficulty.GetSeparatedDifficulty() switch
            {
                SeparatedDifficulty.Easy => marbleGravityEasy.gameObject,
                SeparatedDifficulty.Medium => marbleGravityMedium.gameObject,
                SeparatedDifficulty.Hard => marbleGravityHard.gameObject,
                _ => o
            };
        }
    }
}