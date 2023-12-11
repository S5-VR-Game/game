using UnityEngine;
using PlayerController;

namespace Environment
{
    public class DoorController : MonoBehaviour {

        public PlayerProfileService playerService;
        public float distance = 5f;
        private Animator m_Animator;

        private void Start()
        {
            m_Animator = GetComponent<Animator>();
        }

        protected virtual void Update ()
        {
            m_Animator.SetBool("character_nearby",
                Vector3.Distance(playerService.GetPlayerGameObject().transform.position, transform.position) <=
                distance);
        }
    }
}