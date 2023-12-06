using PlayerController;
using UnityEngine;

namespace Game.Tasks.MedicalDisaster
{
    public class MedicalDisasterFactory : GameTaskFactory<TaskSpawnPoint>
    {
        [SerializeField] private MedicalDisaster medicalDisasterPrefab;
        [SerializeField] private PlayerProfileService playerProfileService;
        
        protected override GameTask CreateTask(TaskSpawnPoint spawnPoint)
        {
            var task = Instantiate(medicalDisasterPrefab, spawnPoint.GetSpawnPosition(), Quaternion.identity);
            task.playerProfileService = playerProfileService;
            return task;
        }
    }
}