using System;
using Logging;
using UnityEngine;

namespace Game.Tasks.StorageRiddle
{
    public class StartStorageRiddle : TimerTask
    {
        private Logger m_LOG = new (new LogHandler());
        private const string LOGTag = "StorageRiddle";
        
        private int _maxAmountDeliveryBoxes;
        
        public new Difficulty difficulty;
        
        public HandleBoxDelivery handleBoxDeliveryScript;
        public SpawnBoxes spawnBoxesScript;
        
        public StartStorageRiddle(float initialTimerTime, string taskName, string taskDescription, int integrityValue = k_DefaultIntegrityValue) : base(initialTimerTime, taskName, taskDescription, integrityValue)
        {
        }

        // Update is called once per frame
        public override void Initialize()
        {
            SetupDifficulty();
            
            handleBoxDeliveryScript.maxAmountDeliveryBoxes = _maxAmountDeliveryBoxes;
            handleBoxDeliveryScript.m_LOG = m_LOG;
            handleBoxDeliveryScript.LOGTag = LOGTag;

            spawnBoxesScript.maxAmountDeliveryBoxes = _maxAmountDeliveryBoxes;
        }

        protected override void BeforeStateCheck()
        {
        }

        protected override TaskState CheckTaskState()
        {
            if (handleBoxDeliveryScript.IsTaskFinished()) return TaskState.Successful;
            return TaskState.Ongoing;
        }

        protected override void AfterStateCheck()
        {
        }

        void Update()
        {
        
        }
        
        private void SetupDifficulty()
        {
            switch (difficulty.GetSeparatedDifficulty())
            {
                case SeparatedDifficulty.Easy:
                    _maxAmountDeliveryBoxes = 3;
                    m_LOG.Log(LOGTag,"Amount of boxes: " + _maxAmountDeliveryBoxes);
                    break;
                case SeparatedDifficulty.Medium:
                    _maxAmountDeliveryBoxes = 5;
                    m_LOG.Log(LOGTag,"Amount of boxes: " + _maxAmountDeliveryBoxes);
                    break;
                case SeparatedDifficulty.Hard:
                    _maxAmountDeliveryBoxes = 7;
                    m_LOG.Log(LOGTag,"Amount of boxes: " + _maxAmountDeliveryBoxes);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        }
    }
}
