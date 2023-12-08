using UnityEngine;

namespace Game.Tasks.EnergyCore
{
    public class EnergyCoreEvent : MonoBehaviour
    {
        public EnergyCoreManager energyCoreManagerScript;
    
        // event triggered when energycell collides with core
        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.gameObject.CompareTag("EnergyCell")) return;
            
            energyCoreManagerScript.TriggerOnCollisionEnter(gameObject, collision.gameObject);
        }
    }
}
