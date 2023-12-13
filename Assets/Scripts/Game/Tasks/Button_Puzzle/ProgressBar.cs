using UnityEngine;



public class ShaderGraphProgressBar : MonoBehaviour
{
    [Range(-1f, 1f)]
    public float _FillRate = -1f; //progress bar starts empty

    public GameObject capsule;
    public Material objectMaterial;
    
    // Start is called before the first frame update
    private void Start()
    {
        //new material is applied to the game object
        capsule.GetComponent<Renderer>().material = objectMaterial; 
        //initial value is set
        objectMaterial.SetFloat("_FillRate", _FillRate);  
    }

    //enables changing the value of progress bar
    public void ChangeValue(float value) 
    {
        _FillRate = value;
        //Update the value of the progress bar
        objectMaterial.SetFloat("_FillRate", _FillRate); 
    }
}