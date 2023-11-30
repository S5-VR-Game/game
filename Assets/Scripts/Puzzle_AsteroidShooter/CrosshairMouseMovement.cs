using UnityEngine;

namespace Puzzle_AsteroidShooter
{
    // class used to track movement of the mouse for the crosshair
    public class CrosshairMouseMovement : MonoBehaviour
    {
        // objects of the canvas and the crosshair
        public RectTransform crosshairRectTransform;
        public RectTransform canvasRectTransform;
        
        // stores the objects of the cameras used for both profiles
        public Camera cameraKeyboard;
        public Camera cameraVR;
        
        // stores the used camera-object
        private Camera _usedCamera;
        
        private void OnEnable()
        {
            _usedCamera = GetUsedCamera();
        }

        private void Update()
        {
            Vector2 mousePosition = Input.mousePosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, mousePosition, _usedCamera, out mousePosition);

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
        
        // returns the used camera depending on the profile
        private Camera GetUsedCamera()
        {
            return PlayerPrefs.GetString("CurrentPlayer").Equals("Keyboard") ? cameraKeyboard : cameraVR;
        }
        
    }
}