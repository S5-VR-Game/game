using System;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

namespace Puzzle_AsteroidShooter
{
    // class used to shoot projectiles from the crosshair
    public class ShootProjectile : MonoBehaviour
    {
        // stores the prefab of the projectile
        public GameObject projectilePrefab;
        
        // stores the objects of the cameras used for both profiles
        public Camera cameraKeyboard;
        public Camera cameraVR;
        
        private Camera _usedCamera; // stores the used camera-object
        
        public float projectileSpeed = 10f; // movement-speed of the bullet 

        // stores the object of the left and right vr-controller
        public XRNode leftController;
        public XRNode rightController;
        
        private void OnEnable()
        {
            _usedCamera = GetUsedCamera();
        }

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
                if (!Input.GetButton("Trigger" + (int)leftController) &&
                    !Input.GetButton("Trigger" + (int)rightController))
                {
                    CalculateProjectileDirection();
                }
            } 
        }
        
        // returns the used camera depending on the profile
        private Camera GetUsedCamera()
        {
            return PlayerPrefs.GetString("CurrentPlayer").Equals("Keyboard") ? cameraKeyboard : cameraVR;
        }

        private void CalculateProjectileDirection()
        {
            var crosshairPosition = transform.position; // stores the position of the crosshair-image

            var cameraPosition = _usedCamera.transform.position; // stores the position of the camera

            var direction = crosshairPosition - cameraPosition; // calculates the direction

            direction.Normalize();

            // instantiates a new projectile moving away from the crosshair position
            var projectile = Instantiate(projectilePrefab, crosshairPosition, Quaternion.identity);

            // moves the projectile in the calculated direction with given speed
            projectile.GetComponent<Rigidbody>().velocity = direction * projectileSpeed;
        }
    }
}