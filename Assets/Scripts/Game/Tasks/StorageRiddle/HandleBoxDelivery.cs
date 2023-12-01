using UnityEngine;

namespace Game.Tasks.StorageRiddle
{
    public class HandleBoxDelivery : MonoBehaviour
    {
        public int maxAmountDeliveryBoxes;
        
        private int _amountDeliveredBoxes;
        private bool _taskDone;

        private void OnCollisionEnter(Collision collision)
        {
            if (_taskDone) return;
            
            if (collision.gameObject.CompareTag("DeliveryBox"))
            {
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
