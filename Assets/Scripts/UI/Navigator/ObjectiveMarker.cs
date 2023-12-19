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

    public void Initialize(GameTask newTask, Vector3 newLocation)
    {
        task = newTask;
        taskLocation = newLocation;
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
