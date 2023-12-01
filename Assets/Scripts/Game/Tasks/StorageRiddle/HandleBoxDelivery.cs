using UnityEngine;

namespace Game.Tasks.StorageRiddle
{
    public class HandleBoxDelivery : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("DeliveryBox"))
            {
                Debug.Log("Box found on platform!");
                Destroy(collision.gameObject);
            }
        }
    }
}
