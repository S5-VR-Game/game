using UnityEngine;

namespace Game.Tasks.StorageRiddle
{
    public class HandleBoxDelivery : MonoBehaviour
    {
        public Logger m_LOG;
        
        [HideInInspector]
        public string LOGTag;
        
        [HideInInspector]
        public int maxAmountDeliveryBoxes;
        
        private int _amountDeliveredBoxes;
        private bool _taskDone;

        private void OnCollisionEnter(Collision collision)
        {
            if (_taskDone) return;
            
            if (collision.gameObject.CompareTag("DeliveryBox"))
            {
                m_LOG.Log(LOGTag, "Box delivered to storage room.");
                _amountDeliveredBoxes++;
                Destroy(collision.gameObject);
            }

            if (_amountDeliveredBoxes >= maxAmountDeliveryBoxes)
            {
                _taskDone = true;
            }
        }

        public bool IsTaskFinished()
        {
            return _taskDone;
        }
    }
}
