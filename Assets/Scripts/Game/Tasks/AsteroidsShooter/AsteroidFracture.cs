using Sound;
using UnityEngine;

namespace Game.Tasks.AsteroidsShooter
{
    // class used to destroy the collided asteroid and spawn the fractured
    public class AsteroidFracture : MonoBehaviour
    {
        // this is the object the asteroid will break into
        public GameObject fractured;

        public SoundManager soundManager;
        
        // function replaces the collided asteroid with the fractured one
        private void FractureObject()
        {
            Instantiate(fractured, transform.position, transform.rotation); 
        
            Destroy(gameObject);
        }

        // is called when the projectile with the Tag "Bullet" collides with the asteroid
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Bullet"))
            {
                gameObject.GetComponent<AsteroidFracture>().FractureObject();
                soundManager.PlaySound();
            }
        }
    }
}