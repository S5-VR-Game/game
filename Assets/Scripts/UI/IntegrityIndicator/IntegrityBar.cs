using Logging;
using UnityEngine;

public class IntegrityBar : MonoBehaviour
{
    private readonly Logger m_LOG = new Logger(new LogHandler());
    private const string LOGTag = "IntegrityBar";
    
    private RectTransform transform;
    // Start is called before the first frame update
    void Start()
    {
        transform = gameObject.GetComponent<RectTransform>();
        if (transform != null)
        {
            m_LOG.Log(LOGTag, "transform object ok!");
        }
        else
        {
            m_LOG.Log(LOGTag, "transform not ok.");
        }
    }

    public RectTransform getTransform()
    {
        return transform;
    }
}
