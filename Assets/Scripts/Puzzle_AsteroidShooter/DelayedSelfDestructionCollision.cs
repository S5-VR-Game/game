using UnityEngine;

namespace Puzzle_AsteroidShooter
{
    public class DelayedSelfDestructionCollision : MonoBehaviour
    {
        public float delay; // stores the value of the delay when the game-object gets destroyed

        private bool _collided; // stores the value if the object is collided with something

        // times the self-desctruction
        private void Update()
        {
            if (!_collided) return;
            if (delay >= 0f)
            {
                delay -= Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        // gets called at the start of the collision 
        private void OnCollisionEnter(Collision collision)
        {
            _collided = true;
        }
        
        
    }
}