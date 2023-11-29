using UnityEngine;

namespace GameObjects
{
    public class DelayedSelfDestruction : MonoBehaviour
    {
        // stores the value of the delay when the game-object gets destroyed
        public float delay;

        private void Update()
        {
            if (delay >= 0f)
            {
                delay -= Time.deltaTime;
            }
            else
            { 
                Destroy(gameObject);
            }
        }
    }
}