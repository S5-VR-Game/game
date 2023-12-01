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
        public GameObject storageRiddleGameObject;
        
        public StartStorageRiddle() : base(initialTimerTime: 60f, taskName: "Storage Riddle", 
            taskDescription: "Storage Riddle", integrityValue: 10)
        {
        }
        
        public override void Initialize()
        {
            SetupDifficulty();
            
            handleBoxDeliveryScript.maxAmountDeliveryBoxes = _maxAmountDeliveryBoxes;
            spawnBoxesScript.maxAmountDeliveryBoxes = _maxAmountDeliveryBoxes;
            
            storageRiddleGameObject.SetActive(false);
        }

        protected override void BeforeStateCheck()
        {
        }

        protected override TaskState CheckTaskState()
        {
            if (PlayerPrefs.GetString("CurrentPlayer").Equals("VR"))
            {
                return handleBoxDeliveryScript.IsTaskFinished() ? TaskState.Successful : TaskState.Ongoing;
            } 

            // keyboard-player wins directly without playing
            // complete VR-Task, useless to implement with keyboard
            return TaskState.Successful;
        }


        protected override void AfterStateCheck()
        {
            if (currentTaskState != TaskState.Ongoing)
            {
                DestroyTask();
            }
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

        public void StartTask()
        {
            storageRiddleGameObject.SetActive(true);
        }
    }
}
