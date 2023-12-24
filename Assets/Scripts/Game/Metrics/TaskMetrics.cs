using System;
using Game.Tasks;

namespace Game.Metrics
{
    /// <summary>
    /// Organizes task specific metrics. Provides csv export helper functions.
    /// </summary>
    public class TaskMetrics : IMetricFormat<int>
    {
        private const string TaskSpawnCountName = "{0}_spawnCount";
        private const string TaskSuccessCountName = "{0}_successCount";
        private const string TaskFailedCountName = "{0}_failedCount";

        private readonly GameTaskType m_TaskType;
        public int spawnCount;
        public int successCount;
        public int failedCount;
            
        public TaskMetrics(GameTaskType taskType)
        {
            m_TaskType = taskType;
        }

        public string[] GetDescriptionEntries()
        {
            return new[]
            {
                String.Format(TaskSpawnCountName, m_TaskType),
                String.Format(TaskSuccessCountName, m_TaskType),
                String.Format(TaskFailedCountName, m_TaskType)
            };
        }

        public int[] GetDataEntries()
        {
            return new[] {
                spawnCount,
                successCount,
                failedCount
            };
        }
    }
}