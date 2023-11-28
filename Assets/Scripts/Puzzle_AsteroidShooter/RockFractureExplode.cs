using UnityEngine;

namespace Puzzle_AsteroidShooter
{
    public class RockFractureExplode : MonoBehaviour
    {
        
        private void Update()
        {
            // iterates through the fractured asteroid asset and applies an explosion-effect to all children
            // removes the collided asteroid
            foreach (Transform child in transform)
            {
                child.GetComponent<Rigidbody>().AddExplosionForce(Random.Range(20f, 100f), transform.position, Random.Range(8f,10f));
            }
            Destroy(gameObject,3.5f);
        }
    }
}
