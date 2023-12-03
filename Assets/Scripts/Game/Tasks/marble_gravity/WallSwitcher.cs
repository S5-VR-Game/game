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
        private const string Name = "sphere";
        public GameObject wall;
        private void OnCollisionExit(Collision other)
        {
            if (!other.gameObject.name.Equals(Name)) return;
            Destroy(wall);
            Destroy(gameObject);
        }
    }
}