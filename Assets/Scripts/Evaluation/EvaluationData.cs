using System;
using System.Collections.Generic;
using Game.Tasks;
using Unity.Plastic.Newtonsoft.Json;

namespace Evaluation
{
    public class EvaluationData
    {
        private string name { get; }
        private int runNumber { get; }
        private DateTime timeStarted { get; }
        private float ranDistance { get; set; }
        private Dictionary<DateTime, string> _tasksStarted;
        private Dictionary<string, int> _gotInTouchTasks;
        private Dictionary<string, int> _wonTasks;
        private Dictionary<string, int> _failedTasks;


        public EvaluationData(string name, int runNumber)
        {
            this.name = name;
            this.runNumber = runNumber;
            timeStarted = DateTime.Now;
            ranDistance = 0.0f;
            _tasksStarted = new Dictionary<DateTime, string>();
            _gotInTouchTasks = new Dictionary<string, int>();
            _wonTasks = new Dictionary<string, int>();
            _failedTasks = new Dictionary<string, int>();
        }

        /// <summary>
        /// Updates the Distance
        /// </summary>
        /// <param name="distance">The new Distance.</param>
        public void UpdateDistance(float distance)
        {
            ranDistance = distance;
        }

        /// <summary>
        /// Adds a started Task to this Data-Object.
        ///
        /// This Method always needs to be called if a Task has been started.
        /// </summary>
        /// <param name="task">The Reference for the specific GameTask</param>
        public void AddTaskStarted(GameTask task)
        {
            _tasksStarted.Add(DateTime.Now, task.taskName);
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
                DictTypes.TaskFailed => _failedTasks,
                DictTypes.TaskWon => _wonTasks,
                DictTypes.TaskTouched => _gotInTouchTasks,
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