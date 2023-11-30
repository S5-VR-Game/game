using PlayerController;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR;

namespace Game.Tasks.AsteroidsShooter
{
    // class used to track movement of the mouse for the crosshair
    public class CrosshairMouseMovement : MonoBehaviour
    {
        // objects of the canvas and the crosshair
        public RectTransform crosshairRectTransform;
        public RectTransform canvasRectTransform;

        public XRNode pointingNode = XRNode.RightHand;
        public PlayerProfileService playerProfileService;

        
        public XRController controller; // Weise den Controller im Unity-Editor zu
        public float pointerLength = 10f; // Länge des Pointer-Strahls
        private LineRenderer lineRenderer;
        
        private void Update()
        {
            
            if (playerProfileService.GetIsVrPlayerActive())
            {
                

            }
            else
            {
                Vector2 mousePosition = Input.mousePosition;
            
                RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, mousePosition, playerProfileService.GetPlayerCamera(), out mousePosition);

                var newPosition = mousePosition;

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
}