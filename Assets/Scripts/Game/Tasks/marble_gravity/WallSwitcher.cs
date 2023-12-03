using Unity.VisualScripting;
using UnityEngine;

namespace Game.Tasks.marble_gravity
{
    /// <summary>
    /// This Script just turns on and off the
    /// walls in the difficult task
    /// </summary>
    public class WallSwitcher : MonoBehaviour
    {
        private void OnCollisionExit(Collision other)
        {
            Destroy(gameObject);
        }
    }
}