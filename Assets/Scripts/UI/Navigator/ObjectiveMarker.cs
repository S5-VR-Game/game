using Game.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveMarker : MonoBehaviour
{
    private GameTask task;
    private Vector3 taskLocation;

    public Image markerDot;
    public Image pointerUp;
    public Image pointerDown;

    private Vector3 location;
    public Canvas canvas;

    public enum TaskPriority
    {
        Normal,
        Timed,
        TimeCritical,
        
    }

    public void Initialize(GameTask newTask, Vector3 newLocation, TaskPriority priority)
    {
        task = newTask;
        taskLocation = newLocation;
        SetColor(priority);
    }

    private void SetColor(TaskPriority priority)
    {
        Color color = new Color();
        
        switch (priority)
        {
            case TaskPriority.Normal:
                color = new Color(0.0f, 0.5f, 0.0f);
                break;
            case TaskPriority.Timed:
                color = new Color(1.0f, 0.6f, 0.0f);
                break;
            case TaskPriority.TimeCritical:
                color = Color.red;
                break;
        }
        
        markerDot.color = color;
        pointerUp.color = color;
        pointerDown.color = color;
    }

    public GameTask GetTask()
    {
        return task;
    }

    public Vector3 GetTaskLocation()
    {
        return taskLocation;
    }
    

    public void EnableMarkerPart(Image part)
    {
        part.enabled = true;
    }

    public void DisableMarkerPart(Image pointer)
    {
        pointer.enabled = false;
    }
    
    public Canvas GetCanvas()
    {
        return canvas;
    }
}
