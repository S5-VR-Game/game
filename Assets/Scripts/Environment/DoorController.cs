using UnityEngine;
using PlayerController;
using Sound;

namespace Environment
{
    public class DoorController : MonoBehaviour {

        public PlayerProfileService playerService;
        public float distance = 2f;
        private Animator _mAnimator;

        private bool _isDoorOpen;
        private SoundManager _soundManager;
            
        private void Start()
        {
            _mAnimator = GetComponent<Animator>();

            _soundManager = GetComponent<SoundManager>();
        }

        private void Update ()
        {
            _mAnimator.SetBool("character_nearby",
                Vector3.Distance(playerService.GetPlayerGameObject().transform.position, transform.position) <=
                distance);

            var shouldOpen = _mAnimator.GetBool("character_nearby");

            if (shouldOpen == _isDoorOpen) return;
            
            _isDoorOpen = shouldOpen;
            _soundManager.PlaySound();
        }
    }
}