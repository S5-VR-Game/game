using UnityEngine;

namespace Game.Tasks.Button_Puzzle
{
    public class ButtonSequenceFactory : GameTaskFactory<TaskSpawnPoint>
    {

        [SerializeField] private ButtonSequenceLogic prefab;
        protected override GameTask CreateTask(TaskSpawnPoint spawnPoint)
        {
            return Instantiate(prefab, spawnPoint.GetSpawnPosition(), spawnPoint.GetRotation());
        }
    }
}