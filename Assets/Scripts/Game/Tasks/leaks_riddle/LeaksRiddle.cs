using System.Collections.Generic;
using UnityEngine;

namespace Game.Tasks.leaks_riddle
{
    public class LeaksRiddle : TimerTask
    {
        [SerializeField] private GameObject buttonNode;
        [SerializeField] private Material confirmedMaterial;
        private readonly List<GameObject> _leaks = new();
        
        
        private bool _canPressButtonToFinishTask;
        private bool _finished;
        
        
        public LeaksRiddle() : base(120, "Leaks Riddle", "Clear the leaks\n" +
                                                       "and reset the pressure.")
        {
            
        }

        public void RegisterLeak(GameObject leak)
        {
            _leaks.Add(leak);
        }

        public void UnregisterLeak(GameObject leak)
        {
            _leaks.Remove(leak);
        }
        
        

        /// <summary>
        /// This method listens to a button and can only be pressed if all leaks
        /// have disappeared successfully.
        /// </summary>
        public void ButtonListener()
        {
            if (!_canPressButtonToFinishTask) return;
            _finished = true;
        }

        private void ShowButtonIsAbleToPress()
        {
            var bookRenderer = buttonNode.GetComponent<Renderer>();
            var currentBookMaterials = bookRenderer.materials;
            currentBookMaterials[0] = confirmedMaterial;
            bookRenderer.materials = currentBookMaterials;
        }

        private bool CanFinishTask()
        {
            return _leaks.Count == 0;
        }

        public override void Initialize()
        {
            // no implementation needed
        }

        // ReSharper disable Unity.PerformanceAnalysis
        protected override void BeforeStateCheck()
        {
            _canPressButtonToFinishTask = CanFinishTask();
            if (CanFinishTask())
            {
                ShowButtonIsAbleToPress();
            }
        }
        
        protected override TaskState CheckTaskState()
        {
            return _finished ? TaskState.Successful : TaskState.Ongoing;
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