using UnityEngine;

namespace Game.Tasks.AsteroidsShooter
{
    public class DelayedSelfDestructionCollision : MonoBehaviour
    {
        public float delay; // stores the value of the delay when the game-object gets destroyed

        private bool collided; // stores the value if the object is collided with something

        // times the self-desctruction
        private void Update()
        {
            if (!collided) return;
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
            collided = true;
        }
        
        
    }
}