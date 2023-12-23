using System;
using Game.Tasks;
using UnityEngine;

namespace Evaluation
{
    public class EvaluationDataWrapper : MonoBehaviour
    {
        public string probandName;
        public int runNumber;
        private EvaluationData _evaluationData;

        private void Start()
        {
            _evaluationData = new EvaluationData(probandName, runNumber);
        }

        private void Update()
        {
            Console.WriteLine(_evaluationData.ToJson());
        }

        /// <summary>
        /// Wrapper method that calls the Same method
        /// on the Data Object <see cref="EvaluationData"/>
        /// </summary>
        /// <param name="distance">The new Distance for the Data Object.</param>
        /// 
        public void UpdateDistance(float distance)
        {
            _evaluationData.UpdateDistance(distance);
        }

        /// <summary>
        /// Wrapper method that calls the Same method
        /// on the Data Object <see cref="EvaluationData"/>
        /// </summary>
        /// <param name="task">The Game Task that has just been started.</param>
        /// 
        public void AddTaskStarted(GameTask task)
        {
            _evaluationData.AddTaskStarted(task);
        }

        /// <summary>
        /// Wrapper method that calls the Same method
        /// on the Data Object <see cref="EvaluationData"/>
        /// </summary>
        /// <param name="task">The Game Task Object.</param>
        /// <param name="type">The Type that needs an increment</param>
        /// 
        public void IncrementMapEntry(GameTask task, DictTypes type)
        {
            _evaluationData.IncrementMapEntry(task, type);
        }
    }
}