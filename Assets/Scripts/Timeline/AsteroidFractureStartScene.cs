using UnityEngine;

namespace Timeline 
{
    // class used to destroy the collided asteroid and spawn the fractured
    public class AsteroidFractureStartScene : MonoBehaviour
    {
        // this is the object the asteroid will break into
        public GameObject fractured; 

        // is called when the projectile with the Tag "Bullet" collides with the asteroid
        private void OnCollisionEnter(Collision collision)
        {
            Instantiate(fractured, transform.position, transform.rotation); 
        
            Destroy(gameObject);
        }
    }
}