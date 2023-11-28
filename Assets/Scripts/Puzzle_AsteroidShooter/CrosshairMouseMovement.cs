using UnityEngine;

namespace Puzzle_AsteroidShooter
{
    // class to move the crosshair with the mouse
    public class CrosshairMouseMovement : MonoBehaviour
    {
        public RectTransform crosshairRectTransform; // stores the crosshair-object
        public RectTransform canvasRectTransform; // stores the canvas
        public new Camera camera; // stores the camera
        
        private void Update()
        {
            Vector2 mousePosition = Input.mousePosition; // gets the mouse-position in the coordinate-system of the canvas
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, mousePosition, camera, out mousePosition);
            
            var newPosition = mousePosition; // calculates the new position

            // limits the the position of the crosshair to the border of the canvas
            var sizeDelta = canvasRectTransform.sizeDelta;
            var delta = crosshairRectTransform.sizeDelta;
            var minX = -sizeDelta.x / 2.0f + delta.x / 2.0f;
            var maxX = sizeDelta.x / 2.0f - delta.x / 2.0f;
            var minY = -sizeDelta.y / 2.0f + delta.y / 2.0f;
            var maxY = sizeDelta.y / 2.0f - delta.y / 2.0f;

            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
            newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

           // sets the new position
            crosshairRectTransform.anchoredPosition = newPosition;
        }
    }
}