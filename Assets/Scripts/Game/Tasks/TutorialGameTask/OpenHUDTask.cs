using UnityEngine;

namespace Game.Tasks.TutorialGameTask
{
    /// <summary>
    /// This tasks represents just an task to open the HUD and closing it again.
    /// </summary>
    public class OpenHUDTask : GameTask
    {
        private const int CounterNeeded = 2;
        private const KeyCode TogglePanel = KeyCode.JoystickButton2; // vr controller button A
        private int _counter;

        public OpenHUDTask() : base("Open HUD", "Herzlichen Glückwunsch, du hast\n" +
                                                    "das HUD erfolgreich geöffnet. Du\n" +
                                                    "kannst es jetzt wieder schließen.", 20)
        {
            
        }

        public override void Initialize()
        {
            // no implementation needed
        }

        protected override void BeforeStateCheck()
        {
            if (Input.GetKeyUp(TogglePanel))
            {
                _counter++;
            }
        }

        protected override TaskState CheckTaskState()
        {
            return _counter == CounterNeeded ? TaskState.Successful : TaskState.Ongoing;
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