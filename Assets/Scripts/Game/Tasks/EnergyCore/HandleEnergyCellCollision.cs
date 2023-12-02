using UnityEngine;

namespace Game.Tasks.EnergyCore
{
    public class HandleEnergyCellCollision : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.gameObject.CompareTag("EnergyCell"))
            {
                
            }
        }
    }
}
