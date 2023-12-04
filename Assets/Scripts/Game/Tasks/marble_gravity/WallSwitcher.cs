using UnityEngine;

namespace Game.Tasks.marble_gravity
{
    /// <summary>
    /// This Script destroys the wall
    /// and the switch if the Sphere is entering
    /// the given switch.
    ///
    /// NOTE: this script can only be assigned to switches.
    /// </summary>
    public class WallSwitcher : MonoBehaviour
    {
        public GameObject wall;
        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag("Sphere_Collider"))
            {
                return;
            }
            Destroy(wall);
            Destroy(gameObject);
        }
    }
}