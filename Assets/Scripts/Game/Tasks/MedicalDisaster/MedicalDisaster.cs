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
        private const float InitialTimerTimeEasy = 60;
        private const float InitialTimerTimeMedium = 90;
        private const float InitialTimerTimeHard = 150;
        private const float IntegrityEasy = 10;
        private const float IntegrityMedium = 15;
        private const float IntegrityHard = 25;
        private const float TaskStartPlayerDistance = 3;
        
        private float _initialTimerTime;

        // parameters for different difficulties
        private readonly MedicalDisasterParameters m_EasyParameters = new(2, 1, 2);
        private readonly MedicalDisasterParameters m_MediumParameters = new(4, 1, 3);
        private readonly MedicalDisasterParameters m_HardParameters = new(6, 2, 4);
        
        /// <summary>
        /// List of all available valves, which is used to pick random valves, that the user has to close. 
        /// </summary>
        [SerializeField] private List<Valve> openValves;

        [SerializeField] private GameObject leftPipeTop;
        [SerializeField] private GameObject leftPipeBottom;
        [SerializeField] private GameObject rightPipeTop;
        [SerializeField] private GameObject rightPipeBottom;
        
        public MedicalDisaster() : base(40, TaskName, TaskDescription, GameTaskType.MedicalDisaster)
        {
            taskDescription =   "Leitungen zur Krankenstation sind undicht!\n" +
                                "Die Substanzen sind sicher nicht genießbar!\n" +
                                "Du musst die Ventile sofort schließen, um das Leck abzudichten!\n";
        }

        public override void Initialize()
        {
            // sets the remaining time base on difficulty
            remainingTime = difficulty.GetSeparatedDifficulty() switch
            {
                SeparatedDifficulty.Easy => InitialTimerTimeEasy,
                SeparatedDifficulty.Medium => InitialTimerTimeMedium,
                SeparatedDifficulty.Hard => InitialTimerTimeHard,
                _ => throw new ArgumentOutOfRangeException()
            };
            
            _initialTimerTime = remainingTime;

            // sets the integrity based on difficulty
            integrityValue = difficulty.GetSeparatedDifficulty() switch
            {
                SeparatedDifficulty.Easy => IntegrityEasy,
                SeparatedDifficulty.Medium => IntegrityMedium,
                SeparatedDifficulty.Hard => IntegrityHard,
                _ => throw new ArgumentOutOfRangeException()
            };
            
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
                openValve.playerProfileService = playerProfileService;
                openValve.OnValveRotationCompleted += () => openValves.Remove(openValve);
            }
        }

        protected override void BeforeStateCheck()
        {
            // update time indicator
            var remainingTimePercent = remainingTime / _initialTimerTime;
            if (remainingTimePercent is >= 0 and <= 1)
            {
                leftPipeBottom.transform.localScale = new Vector3(remainingTimePercent, 1, 1);
                rightPipeBottom.transform.localScale = new Vector3(remainingTimePercent, 1, 1);
                rightPipeTop.transform.localScale = new Vector3(-1+remainingTimePercent, 1, 1);
                leftPipeTop.transform.localScale = new Vector3(-1+remainingTimePercent, 1, 1);
            }
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

        protected override bool TimerStartCondition()
        {
            // start time, when player is near the task
            return Vector3.Distance(
                playerProfileService.GetPlayerGameObject().transform.position,
                transform.position
                ) <= TaskStartPlayerDistance;
        }

        /// <summary>
        /// Builds a gradient using the given colors. The gradient is linear and evenly distributed.
        /// The gradient does not contain any changes in alpha value.
        /// </summary>
        /// <param name="colors">Colors to build the gradient. The order effects the order of the colors in the gradient</param>
        /// <returns>the created gradient object</returns>
        public static Gradient BuildSimpleGradient(params Color[] colors)
        {
            var gradient = new Gradient();
            
            var gradientColorKeys = new GradientColorKey[colors.Length];
            // add color to gradient color keys
            for (var i = 0; i < colors.Length; i++)
            {
                gradientColorKeys[i] = new GradientColorKey(colors[i], i / (float) (colors.Length - 1));
            }

            // add alpha values
            var alphas = new GradientAlphaKey[2];
            alphas[0] = new GradientAlphaKey(1.0f, 0.0f);
            alphas[1] = new GradientAlphaKey(1.0f, 1.0f);

            // assign to gradient
            gradient.SetKeys(gradientColorKeys, alphas);
            return gradient;
        }
    }
}