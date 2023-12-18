using UnityEngine;
using PlayerController;
using Sound;

namespace Environment
{
    public class DoorController : MonoBehaviour {

        public PlayerProfileService playerService;
        public float distance = 5f;
        private Animator m_Animator;

        private bool _isDoorOpen;
        private SoundManager _soundManager;
            
        private void Start()
        {
            m_Animator = GetComponent<Animator>();

            _soundManager = GetComponent<SoundManager>();
        }

        protected virtual void Update ()
        {
            m_Animator.SetBool("character_nearby",
                Vector3.Distance(playerService.GetPlayerGameObject().transform.position, transform.position) <=
                distance);

            var shouldOpen = m_Animator.GetBool("character_nearby");

            if (shouldOpen == _isDoorOpen) return;
            
            _isDoorOpen = shouldOpen;
            _soundManager.PlaySound();
        }
    }
}