using System;
using UnityEngine;

namespace Game.Tasks.StorageRiddle
{
    /// <summary>
    /// manage the process of the task Storage Riddle
    /// </summary>
    public class StartStorageRiddle : GameTask
    {
        // amount how many boxes should be spawned in
        private int _maxAmountDeliveryBoxes; 
        
        // references to the needed scripts to setup the amount of boxes in the task
        [SerializeField] private HandleBoxDelivery handleBoxDeliveryScript;
        [SerializeField] private SpawnBoxes spawnBoxesScript;
        
        public StartStorageRiddle() : base(taskName: "Storage Riddle", 
            taskDescription: "", taskType: GameTaskType.StorageRiddle, integrityValue: 53.1f)
        {
            taskDescription = "Jemand hat die Lagerkisten verlegt!\n" +
                              "Du musst alle auf die grün leuchtende Lagerfläche legen.\n" +
                              "Eine davon enthält das Handtuch, das du zum Überleben brauchst!";
        }
        
        public override void Initialize()
        {
            // sets the variables depending on the difficulty
            SetupDifficulty(); 
            
            // sets the variable in the subclasses
            handleBoxDeliveryScript.SetMaxAmountDeliveryBoxes(_maxAmountDeliveryBoxes);
            spawnBoxesScript.SetMaxAmountDeliveryBoxes(_maxAmountDeliveryBoxes);
        }

        protected override void BeforeStateCheck()
        {
            // no implementation needed
        }
        
        protected override TaskState CheckTaskState()
        {
            if (PlayerPrefs.GetString("CurrentPlayer").Equals("VR"))
            {
                return handleBoxDeliveryScript.IsTaskFinished() ? TaskState.Successful : TaskState.Ongoing;
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
        
        /// <summary>
        /// sets the amount how many boxes used in this task depending on the selected difficulty
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private void SetupDifficulty()
        {
            _maxAmountDeliveryBoxes = difficulty.GetSeparatedDifficulty() switch
            {
                SeparatedDifficulty.Easy => 3,
                SeparatedDifficulty.Medium => 5,
                SeparatedDifficulty.Hard => 7,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
