using UnityEngine;
using PlayerController;

namespace Environment
{
    public class DoorController : MonoBehaviour {

        public PlayerProfileService playerService;
        public float distance = 2f;
        private Animator _mAnimator;

        private void Start()
        {
            _mAnimator = GetComponent<Animator>();
        }

        private void Update ()
        {
            _mAnimator.SetBool("character_nearby",
                Vector3.Distance(playerService.getPlayerGameObject().transform.position, transform.position) <=
                distance);
        }
    }
}