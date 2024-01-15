using UnityEngine;

namespace Tutorial
{
    public class TutorialDoorController : MonoBehaviour
    {
        private Animator m_Animator;
            
        private void Start()
        {
            m_Animator = GetComponent<Animator>();
            m_Animator.SetBool("character_nearby", false);
        }

        /// <summary>
        /// Opens the door
        /// </summary>
        public void OpenDoor()
        {
            m_Animator.SetBool("character_nearby", true);
        }
    }
}