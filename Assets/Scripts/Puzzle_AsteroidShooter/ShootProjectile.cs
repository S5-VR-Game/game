using UnityEngine;

namespace Puzzle_AsteroidShooter
{
    // class used to shoot projectiles from the crosshair
    public class ShootProjectile : MonoBehaviour
    {
        // stores the prefab of the projectile
        public GameObject projectilePrefab;
        
       
        public new GameObject camera;  // stores the camera-object
        
        public float projectileSpeed = 10f; // movement-speed of the bullet 
        
        private void Update()
        {
            if (!Input.GetMouseButtonDown(0)) return; // checks if input is left-click with mouse
            
            var crosshairPosition = transform.position; // stores the position of the crosshair-image
            
            var cameraPosition = camera.transform.position; // stores the position of the camera
            
            var direction = crosshairPosition - cameraPosition; // calculates the direction
            
            direction.Normalize();

            // instantiates a new projectile moving away from the crosshair position
            var projectile = Instantiate(projectilePrefab, crosshairPosition, Quaternion.identity);

            // moves the projectile in the calculated direction with given speed
            projectile.GetComponent<Rigidbody>().velocity = direction * projectileSpeed;
        }
    }
}