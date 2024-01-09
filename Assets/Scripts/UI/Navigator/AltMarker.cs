
using PlayerController;
using Unity.XR.CoreUtils;
using UnityEngine;

public class AltMarker : MonoBehaviour
{
    public GameObject marker;
    
    public Material innerMaterialRed;
    public Material innerMaterialOrange;
    public Material innerMaterialGreen;

    [SerializeField] private GameObject _innerCircle;
    
    private PlayerProfileService _playerProfileService;


    public void Start()
    {
        marker = gameObject.GetNamedChild("Canvas");
    }
    
    
    public void Update()
    {
        // make marker look at player camera
        marker.transform.LookAt(_playerProfileService.GetPlayerCamera().transform.position);
    }

    public void SetColor(ObjectiveMarker.TaskType type)
    {
        Material innerMat = innerMaterialGreen;
        
        switch (type)
        {
            case ObjectiveMarker.TaskType.Timed:
                innerMat = innerMaterialOrange;
                break;
            case ObjectiveMarker.TaskType.TimeCritical:
                innerMat = innerMaterialRed;
                break;
        }
        
        _innerCircle.GetComponent<MeshRenderer>().material = innerMat;
    }

    public void setPlayerProfile(PlayerProfileService profileService)
    {
        _playerProfileService = profileService;
    }
}
