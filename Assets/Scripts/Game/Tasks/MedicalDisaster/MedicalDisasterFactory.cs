using UnityEngine;

namespace Game.Tasks.MedicalDisaster
{
    public class MedicalDisasterFactory : GameTaskFactory<TaskSpawnPoint>
    {
        [SerializeField] private MedicalDisaster medicalDisasterPrefab;
        protected override GameTask CreateTask(TaskSpawnPoint spawnPoint)
        {
            return Instantiate(medicalDisasterPrefab, spawnPoint.GetSpawnPosition(), Quaternion.identity);
        }
    }
}