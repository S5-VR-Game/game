using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Tasks.leaks_riddle
{
    public class LeaksRiddle : TimerTask
    {
        [SerializeField] private GameObject buttonNode;
        [SerializeField] private Material confirmedMaterial;
        
        private bool _canPressButtonToFinishTask;
        private bool _finished;
        
        
        public LeaksRiddle() : base(120, "Leaks Riddle", "Clear the leaks\n" +
                                                       "and reset the pressure.")
        {
            
        }

        /// <summary>
        /// This method listens to a button and can only be pressed if all leaks
        /// have disappeared successfully.
        /// </summary>
        public void ButtonListener()
        {
            if (!_canPressButtonToFinishTask) return;
            _finished = true;
            ShowButtonIsAbleToPress();
        }

        private void ShowButtonIsAbleToPress()
        {
            var buttonRenderer = buttonNode.GetComponent<Renderer>();
            var materials = buttonRenderer.materials;
            materials[0] = confirmedMaterial;
        }

        /// <summary>
        /// Retrieves all GameObjects as a List that are still tagged with leaks.
        /// </summary>
        /// <returns></returns>
        private List<GameObject> GetAllLeftLeaks()
        {
            var childTransforms = gameObject.GetComponentsInChildren<Transform>(true);
            return (from childTransform in childTransforms where childTransform.CompareTag($"leak") 
                select childTransform.gameObject).ToList();
        }

        private bool CanFinishTask()
        {
            return GetAllLeftLeaks().Count == 0;
        }

        public override void Initialize()
        {
            // no implementation needed
        }

        // ReSharper disable Unity.PerformanceAnalysis
        protected override void BeforeStateCheck()
        {
            _canPressButtonToFinishTask = CanFinishTask();
        }
        
        protected override TaskState CheckTaskState()
        {
            return _finished ? TaskState.Successful : TaskState.Failed;
        }

        
        // ReSharper disable Unity.PerformanceAnalysis
        protected override void AfterStateCheck()
        {
            if (currentTaskState != TaskState.Ongoing)
            {
                DestroyTask();
            }
        }
    }
}