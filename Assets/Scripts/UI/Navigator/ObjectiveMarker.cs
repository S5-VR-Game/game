using Game.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveMarker : MonoBehaviour
{
    private GameTask task;
    private Vector3 taskLocation;

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
    

    public void EnablePointer(Image pointer)
    {
        pointer.enabled = true;
    }

    public void DisablePointer(Image pointer)
    {
        pointer.enabled = false;
    }
    
    public Canvas GetCanvas()
    {
        return canvas;
    }
}
