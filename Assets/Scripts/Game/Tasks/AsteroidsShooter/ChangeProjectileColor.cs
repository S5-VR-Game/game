using UnityEngine;

namespace Game.Tasks.AsteroidsShooter
{
    // class used to change color of the projectile
    public class ChangeProjectileColor : MonoBehaviour
    {
        // stores the color of the projectile
        public Color newColor; 

        private void OnEnable()
        {
            // creates a new material
            var material = new Material(Shader.Find("Standard"))
            {
                // changes its color to the selected color-value
                color = newColor
            };

            // adds the new color to the sphere
            GetComponent<Renderer>().material = material;
        }
    }
}