using System;
using System.IO;
using Game;
using Game.Tasks;
using UnityEngine;

namespace Evaluation
{
    public class EvaluationDataWrapper : MonoBehaviour
    {
        public string probandName;
        public int runNumber;
        private EvaluationData _evaluationData;
        [SerializeField] private GameInformation gameInformation;
        
        private const string ExportFileNameJson = "evaluation_data_{0}.json";

        private void Start()
        {
            _evaluationData = new EvaluationData(probandName, runNumber);
            // subscribe to game state change event, to write the data to file when the game is over
            gameInformation.OnGameStateChanged += state =>
            {
                if (state != GameState.Ongoing)
                {
                    // write json string to file
                    File.WriteAllText(string.Format(ExportFileNameJson, gameInformation.GetGameID()), _evaluationData.ToJson());
                }
            };
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