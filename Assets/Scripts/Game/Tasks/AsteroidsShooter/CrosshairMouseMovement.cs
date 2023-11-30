using PlayerController;
using UnityEngine;

namespace Game.Tasks.AsteroidsShooter
{
    // class used to track movement of the mouse for the crosshair
    public class CrosshairMouseMovement : MonoBehaviour
    {
        // objects of the canvas and the crosshair
        public RectTransform crosshairRectTransform;
        public RectTransform canvasRectTransform;
        
        public PlayerProfileService playerProfileService;
        
        public Transform controllerTransform; // transform of the controller
        public LayerMask canvasLayer; // layer-mask of the canvas
        
        private void Update()
        {
            var newPosition = new Vector2();
            
            if (playerProfileService.GetIsVrPlayerActive())
            {
                if (Physics.Raycast(controllerTransform.position, controllerTransform.forward, out var hit,
                        Mathf.Infinity, canvasLayer))
                {
                    // hit.point contains colliding point between canvas and raycast
                    newPosition = new Vector2(hit.point.x, hit.point.y);
                }
            }
            else
            {
                Vector2 mousePosition = Input.mousePosition;
            
                RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, mousePosition, playerProfileService.GetPlayerCamera(), out mousePosition);

                newPosition = mousePosition;
            }
            
            // calculates the screen-size and the ratio
            var sizeDelta = canvasRectTransform.sizeDelta;
            var delta = crosshairRectTransform.sizeDelta;

            // limits the movement of the crosshair to the canvas-size
            var minX = -sizeDelta.x / 2.0f + delta.x / 2.0f;
            var maxX = sizeDelta.x / 2.0f - delta.x / 2.0f;
            var minY = -sizeDelta.y / 2.0f + delta.y / 2.0f;
            var maxY = sizeDelta.y / 2.0f - delta.y / 2.0f;

            // calculates and sets the new position
            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
            newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

            crosshairRectTransform.anchoredPosition = newPosition;   
        }
    }
}