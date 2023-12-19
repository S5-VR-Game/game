using Sound;
using UnityEngine;

namespace Game.Tasks.EnergyCore
{
    // class used to play sound if energy core collides with something
    public class EnergyCellCollision : MonoBehaviour
    {
        public SoundManager soundManager;
        
        private void OnCollisionEnter(Collision other)
        {
            soundManager.PlaySound(); // plays sound
        }
    }
}
