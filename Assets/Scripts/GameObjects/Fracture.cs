using UnityEngine;

namespace GameObjects
{
    public class Fracture : MonoBehaviour
    {
        // stores the game-object of the asset of the fractured asteroid
        public GameObject fractured;

        private void FractureObject()
        {
            Instantiate(fractured, transform.position, transform.rotation); //Spawn in the broken version
            Destroy(gameObject);
        }

        // gets called at the start of the collision 
        private void OnCollisionEnter(Collision collision)
        {
            FractureObject();
        }
    }
}