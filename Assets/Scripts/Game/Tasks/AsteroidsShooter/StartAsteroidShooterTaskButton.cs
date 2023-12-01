using UnityEngine;

namespace Game.Tasks.AsteroidsShooter
{
    // class used to start the task if the button in the prefab is pressed
    public class StartAsteroidShooterTaskButton : MonoBehaviour
    {
        public StartAsteroidShooter startAsteroidShooterScript;

        public void StartTaskAsteroidShooter()
        {
            startAsteroidShooterScript.StartTask();
        }
    }
}
