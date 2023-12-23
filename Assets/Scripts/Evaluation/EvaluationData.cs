using System;
using System.Collections.Generic;
using Game.Tasks;
using Newtonsoft.Json;
using Unity.VisualScripting;

namespace Evaluation
{
    public class EvaluationData
    {
        public string Name { get; }
        public int RunNumber { get; }
        public DateTime TimeStarted { get; }
        public float RanDistance { get; set; }
        public Dictionary<DateTime, string> TasksStarted { get; }
        public Dictionary<string, int> GotInTouchTasks { get; }
        public Dictionary<string, int> HUDInteractions { get; }
        public Dictionary<string, int> WonTasks { get; }
        public Dictionary<string, int> FailedTasks { get; }


        public EvaluationData(string name, int runNumber)
        {
            Name = name;
            RunNumber = runNumber;
            TimeStarted = DateTime.Now;
            RanDistance = 0.0f;
            TasksStarted = new Dictionary<DateTime, string>();
            GotInTouchTasks = new Dictionary<string, int>();
            WonTasks = new Dictionary<string, int>();
            FailedTasks = new Dictionary<string, int>();
            HUDInteractions = new FlexibleDictionary<string, int>();
        }

        /// <summary>
        /// Updates the Distance
        /// </summary>
        /// <param name="distance">The new Distance.</param>
        public void UpdateDistance(float distance)
        {
            RanDistance = distance;
        }

        /// <summary>
        /// Adds a started Task to this Data-Object.
        ///
        /// This Method always needs to be called if a Task has been started.
        /// </summary>
        /// <param name="task">The Reference for the specific GameTask</param>
        public void AddTaskStarted(GameTask task)
        {
            TasksStarted.Add(DateTime.Now, task.taskName);
        }

        /// <summary>
        /// Increments the Map Entry of one of the specific Maps.
        /// </summary>
        /// <param name="task">The specific GameTask to provide its taskName</param>
        /// <param name="dictType">one of three Maps that can be incremented</param>
        public void IncrementMapEntry(GameTask task, DictTypes dictType)
        {
            var mapWithEntries = GetCorrectDictionary(dictType);
            var registered = mapWithEntries.ContainsKey(task.taskName);
            if (!registered)
            {
                mapWithEntries.Add(task.taskName, 1);
            }
            else
            {
                mapWithEntries[task.taskName]++;
            }
        }

        /// <summary>
        /// Returns one of our data Dictionaries according to the DictType Enum.
        /// </summary>
        /// <param name="dictType">The DictType that needs to be represented</param>
        /// <returns>The Right Dictionary acc. to the dicttype</returns>
        /// <exception cref="ArgumentOutOfRangeException">cannot be reached</exception>
        private Dictionary<string, int> GetCorrectDictionary(DictTypes dictType)
        {
            return dictType switch
            {
                DictTypes.TaskFailed => FailedTasks,
                DictTypes.TaskWon => WonTasks,
                DictTypes.TaskTouched => GotInTouchTasks,
                DictTypes.HUDUsed => HUDInteractions,
                _ => throw new ArgumentOutOfRangeException(nameof(dictType), dictType, null)
            };
        }
        
        /// <summary>
        /// Converts this Data Object into a beautifully formatted JSON-String
        /// </summary>
        /// <returns>This Object as JSON-Representation.</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}