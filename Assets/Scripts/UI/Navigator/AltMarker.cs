
using PlayerController;
using Unity.XR.CoreUtils;
using UnityEngine;

public class AltMarker : MonoBehaviour
{
    public GameObject marker;

    [SerializeField] private GameObject _innerCircle;
    [SerializeField] private GameObject _outerCircle;
    [SerializeField] private Shader shader;
    
    private PlayerProfileService _playerProfileService;

    // distance when fading should start
    private float _fadeTriggerDistance = 6.0f;
    // distance when marker should be invisible
    private float _invisDistance = 3.0f;
    
    
    public void Start()
    {
        marker = gameObject.GetNamedChild("Canvas");
    }
    
    
    public void Update()
    {
        // make marker look at player camera
        marker.transform.LookAt(_playerProfileService.GetPlayerCamera().transform.position);

        float distance =
            Vector3.Distance(transform.position, _playerProfileService.GetPlayerCamera().transform.position);

        
        // fade marker if player is close
        if (distance < _fadeTriggerDistance)
        {
            float fadingDistance = _fadeTriggerDistance - _invisDistance;
            float distancePercent = Mathf.Clamp((distance - _invisDistance), 0.0f, fadingDistance) / fadingDistance;
            
            Material innermat = _innerCircle.GetComponent<MeshRenderer>().material;
            Material outermat = _outerCircle.GetComponent<MeshRenderer>().material;
            
            Color innerColor = new Color(innermat.color.r, innermat.color.g, innermat.color.b, distancePercent);
            Color outerColor = new Color(outermat.color.r, outermat.color.g, outermat.color.b, distancePercent);
            
            innermat.color = innerColor;
            innermat.SetColor("_EmissionColor", innerColor);
            
            outermat.color = outerColor;
            outermat.SetColor("_EmissionColor", outerColor);
        }
        
        // set marker alpha to 1.0 if below and outsider fading area
        else if (_innerCircle.GetComponent<MeshRenderer>().material.color.a < 1.0)
        {
            Material innermat = _innerCircle.GetComponent<MeshRenderer>().material;
            Material outermat = _outerCircle.GetComponent<MeshRenderer>().material;
            
            Color innerColor = new Color(innermat.color.r, innermat.color.g, innermat.color.b, 1.0f);
            Color outerColor = new Color(outermat.color.r, outermat.color.g, outermat.color.b, 1.0f);
            
            innermat.color = innerColor;
            innermat.SetColor("_EmissionColor", innerColor);
            
            outermat.color = outerColor;
            outermat.SetColor("_EmissionColor", outerColor);
        }
       
    }

    /// <summary>
    /// Lets the marker change its color according to the given task type
    /// Normal == Green
    /// Timed == Orange
    /// TimeCritical == Red
    /// </summary>
    /// <param name="type">Type to set color for</param>
    public void InitiateMarker(ObjectiveMarker.TaskType type)
    {
        Material innerMat = new Material(shader);
        Material outerMat = new Material(shader);

        outerMat.color = Color.white;
        outerMat.SetColor("_EmissionColor", Color.white);
        outerMat.renderQueue = 3001;

        Color innerColor = new Color();

        switch (type)
        {
            case ObjectiveMarker.TaskType.Normal:
                innerColor = new Color(0.0f, 0.75f, 0.0f);
                break;
            case ObjectiveMarker.TaskType.Timed:
                innerColor = new Color(1.0f, 0.6f, 0.0f);
                break;
            case ObjectiveMarker.TaskType.TimeCritical:
                innerColor = Color.red;
                break;
        }
        
        innerMat.color = innerColor;
        innerMat.SetColor("_EmissionColor", innerColor);
        innerMat.renderQueue = 3002;
        
        _innerCircle.GetComponent<MeshRenderer>().material = innerMat;
        _outerCircle.GetComponent<MeshRenderer>().material = outerMat;
    }

    public void setPlayerProfile(PlayerProfileService profileService)
    {
        _playerProfileService = profileService;
    }
}
