using UnityEngine;

namespace Environment
{
    public class DoorController : MonoBehaviour {

        public GameObject player;
        public float distance = 2f;
        private Animator m_Animator;

        private void Start()
        {
            m_Animator = GetComponent<Animator>();
        }

        private void Update () {
            if (Vector3.Distance(player.transform.position, transform.position) <= distance)
            {
                m_Animator.SetBool("character_nearby", true);
            }
            else
            {
                m_Animator.SetBool("character_nearby", false);
            }
        }
    }
}