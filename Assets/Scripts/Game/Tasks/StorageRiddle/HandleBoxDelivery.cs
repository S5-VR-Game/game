using UnityEngine;

namespace Game.Tasks.StorageRiddle
{
    // class handles what is happening when the player puts a box onto the platform
    public class HandleBoxDelivery : MonoBehaviour
    {
        [HideInInspector]
        public int maxAmountDeliveryBoxes; // value how many boxes the player needs to bring to the game-object
        
        private int _amountDeliveredBoxes; // current value of the delivered boxes
        private bool _taskDone; // bool to check if the win-condition is reached 

        // collision event
        private void OnCollisionEnter(Collision collision)
        {
            if (_taskDone) return;
            
            if (collision.gameObject.CompareTag("DeliveryBox")) // when the game-object touches one of the boxes
            {
                _amountDeliveredBoxes++; 
                Destroy(collision.gameObject);
            }

            // sets the task to done when the condition is true
            if (_amountDeliveredBoxes >= maxAmountDeliveryBoxes)
            {
                _taskDone = true;
            }
        }

        // function to return if the player is done with the task
        public bool IsTaskFinished()
        {
            return _taskDone;
        }
    }
}
