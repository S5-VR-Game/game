using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Tasks.MedicalDisaster
{
    /// <summary>
    /// Medical Disaster task. Requires the player to rotate valves to stop the leak.
    /// </summary>
    public class MedicalDisaster : TimerTask
    {
        private const string TaskName = "Medical Disaster";
        private const string TaskDescription = "Medical Disaster Description";
        private const float InitialTimerTime = 60;

        // parameters for different difficulties
        private readonly MedicalDisasterParameters m_EasyParameters = new(2, 1, 2);
        private readonly MedicalDisasterParameters m_MediumParameters = new(4, 1, 3);
        private readonly MedicalDisasterParameters m_HardParameters = new(6, 2, 4);
        
        /// <summary>
        /// List of all available valves, which is used to pick random valves, that the user has to close. 
        /// </summary>
        [SerializeField] private List<Valve> openValves;
        
        public MedicalDisaster() : base(InitialTimerTime, TaskName, TaskDescription)
        {
        }

        public override void Initialize()
        {
            // shuffle for random valve order
            openValves.Shuffle();
            // obtain parameters based on difficulty
            MedicalDisasterParameters parameterSetup = difficulty.GetSeparatedDifficulty() switch
            {
                SeparatedDifficulty.Easy => m_EasyParameters,
                SeparatedDifficulty.Medium => m_MediumParameters,
                SeparatedDifficulty.Hard => m_HardParameters,
                _ => throw new Exception("Separated Difficulty state not implemented")
            };

            // remove valves to match valve count of parameter setup
            openValves.RemoveRange(0, Math.Min(openValves.Count - parameterSetup.valveCount, openValves.Count));
            
            // activate/register valves and set rotation count
            foreach (var openValve in openValves)
            {
                openValve.gameObject.SetActive(true);
                openValve.requiredRotationCount = Random.Range(parameterSetup.minValveRotation, parameterSetup.maxValveRotation + 1);
                openValve.OnValveRotationCompleted += () => openValves.Remove(openValve);
            }
        }

        protected override void BeforeStateCheck()
        {
        }

        protected override TaskState CheckTaskState()
        {
            // if all valves are closed, the task is completed
            if (openValves.Count == 0)
            {
                return TaskState.Successful;
            }

            return TaskState.Ongoing;
        }

        protected override void AfterStateCheck()
        {
            if (currentTaskState != TaskState.Ongoing)
            {
                DestroyTask();
            }
        }
    }
}