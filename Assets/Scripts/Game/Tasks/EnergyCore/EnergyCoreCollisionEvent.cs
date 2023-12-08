using UnityEngine;

namespace Game.Tasks.EnergyCore
{
    public class EnergyCoreEvent : MonoBehaviour
    {
        public EnergyCoreManager energyCoreManagerScript;
        
        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.gameObject.CompareTag("EnergyCell")) return;
            
            Debug.Log("Collision Enter");
            
            energyCoreManagerScript.TriggerOnCollisionEnter(gameObject, collision.gameObject);
        }

        private void OnCollisionExit(Collision collision)
        {
            if (!collision.gameObject.CompareTag("EnergyCell")) return;
            
            Debug.Log("Collision Exit");
            energyCoreManagerScript.TriggerOnCollisionExit(gameObject);
        }
    }
}
