using UnityEngine;

public class DoorController : MonoBehaviour {

    public GameObject player;
    public float distance = 10f;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update () {
        if (Vector3.Distance(player.transform.position, transform.position) <= distance)
        {
            animator.SetBool("character_nearby", true);
        }
        else
        {
            animator.SetBool("character_nearby", false);
        }
    }
}