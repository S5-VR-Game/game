using UnityEngine;

namespace PlayerController
{
    public class ChangePlayerHeight : MonoBehaviour
    {
        // Start is called before the first frame update
        private void Start()
        {
            // selected height of the player in the main menu settings
            var selectedPlayerHeight = PlayerPrefs.GetFloat("PlayerHeight", 1.6f);

            // stores the current transform component of the player
            var objectTransform = transform;

            // current scaling
            var currentScaling = objectTransform.localScale;

            // calculate new scaling
            var newScaling = new Vector3(
                currentScaling.x,
                currentScaling.y * selectedPlayerHeight,
                currentScaling.z
            ); 

            // sets the new scaling
            objectTransform.localScale = newScaling;
        }
    }
}
