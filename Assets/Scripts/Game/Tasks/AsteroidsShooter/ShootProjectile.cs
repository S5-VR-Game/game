using UnityEngine;
using UnityEngine.XR;

namespace Game.Tasks.AsteroidsShooter
{
    // class used to shoot projectiles from the crosshair
    public class ShootProjectile : MonoBehaviour
    {
        public GameObject projectilePrefab; // stores the prefab of the projectile
        
        public Camera camera; // stores the object of the camera 
        
        public float projectileSpeed = 20f; // movement-speed of the bullet 

        public XRNode controller;

        private bool lastTrigger = false;
        
        private void Update()
        {
            // checks if current player is keyboard-profile and player uses left-click
            if (PlayerPrefs.GetString("CurrentPlayer").Equals("Keyboard"))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    CalculateProjectileDirection();
                    
                }
            } 
            // checks if current player is vr-profile and player uses trigger-button
            if (PlayerPrefs.GetString("CurrentPlayer").Equals("VR"))
            {
                bool triggered = false;

                var device = InputDevices.GetDeviceAtXRNode(controller);
                triggered = (device.TryGetFeatureValue(CommonUsages.triggerButton,
                    out var triggerState) && triggerState);
                 
                
                if (triggered != lastTrigger)
                {
                    if (triggered)
                    {
                        CalculateProjectileDirection();
                    }

                    lastTrigger = triggered;
                }
            } 
        }

        private void CalculateProjectileDirection()
        {
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