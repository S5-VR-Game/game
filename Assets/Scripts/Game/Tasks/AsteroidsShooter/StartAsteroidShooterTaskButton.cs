using UnityEngine;

namespace Game.Tasks.AsteroidsShooter
{
    public class StartAsteroidShooterTaskButton : MonoBehaviour
    {
        public StartAsteroidShooter startAsteroidShooterScript;

        public void StartTaskAsteroidShooter()
        {
            startAsteroidShooterScript.StartTask();
        }
    }
}
