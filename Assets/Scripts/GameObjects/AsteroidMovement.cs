using UnityEngine;

namespace GameObjects
{
    public class AsteroidMovement : MonoBehaviour
    {
        // stores the values of the direction and the speed of the asteroid
        public float movementSpeed = 10.0f;
        public float xDirection;
        public float yDirection;
        public float zDirection;

        void Update()
        {
            // calculates the movement direction
            Vector3 movementDirection = new Vector3(xDirection, yDirection, zDirection);

            // moves the object depending on its direction and speed
            transform.Translate(movementDirection * movementSpeed * Time.deltaTime);
        }

 
    }
}