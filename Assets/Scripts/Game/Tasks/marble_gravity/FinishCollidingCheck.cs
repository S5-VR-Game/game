using System;
using UnityEngine;

namespace Game.Tasks.marble_gravity
{
    public class FinishCollidingCheck : MonoBehaviour
    {
        public MarbleGravity marbleGravity;
        
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Sphere_Collider"))
            {
                Debug.Log("Should be COllliding");
                marbleGravity.SetFinished(true);
            }
        }
    }
}