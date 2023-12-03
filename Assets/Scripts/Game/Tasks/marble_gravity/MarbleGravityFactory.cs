namespace Game.Tasks.marble_gravity
{
    /// <summary>
    /// This class constructs a MarbleGravity instance
    /// only in given spawn points with transform and rotation.
    /// </summary>
    public class MarbleGravityFactory : GameTaskFactory<TaskSpawnPoint>
    {
        public MarbleGravity marbleGravity;
        
        protected override GameTask CreateTask(TaskSpawnPoint spawnPoint)
        {
            return null;
        }
    }
}