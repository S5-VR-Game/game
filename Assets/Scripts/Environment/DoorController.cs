using UnityEngine;

public class DoorController : MonoBehaviour {

    public GameObject player;
    public float distance = 5f;
    public bool permission = true;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetPermission(bool permission)
    {
        this.permission = permission;
    }

    void Update () {
        if (Vector3.Distance(player.transform.position, transform.position) <= distance && permission)
        {
            animator.SetBool("character_nearby", true);
        }
        else
        {
            animator.SetBool("character_nearby", false);
        }
    }

}