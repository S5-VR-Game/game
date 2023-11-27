using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayerHeight : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // selected height of the player in the main menu settings
        float select_player_height = PlayerPrefs.GetFloat("PlayerHeight", 0.0f);

        // stores the current transform component of the player
        Transform object_transform = transform;

        // current scaling
        Vector3 current_scaling = object_transform.localScale;

        // calculate new scaling
        Vector3 new_scaling = new Vector3(
            current_scaling.x,
            current_scaling.y * select_player_height,
            current_scaling.z
        ); ;

        // Setzen der neuen Skalierung
        object_transform.localScale = new_scaling;
    }
}
