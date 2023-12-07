using UnityEngine;

namespace Game.Tasks.EnergyCore
{
    public class EnergyCoreCollision : MonoBehaviour
    {
        public StartEnergyCoreTask startEnergyCoreTaskScript;
        public GameObject energyCore;
        
        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag("EnergyCell")) return;

            if (!ManageEnergyCoreColors.GetColor(other.collider.gameObject)
                    .Equals(ManageEnergyCoreColors.GetColor(gameObject))) return;
            startEnergyCoreTaskScript.finishedEnergyCoreCounter++;
            
            Destroy(energyCore);
            Destroy(other.gameObject);

        }
    }
}
