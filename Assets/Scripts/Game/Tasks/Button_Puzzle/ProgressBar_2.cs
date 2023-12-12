using UnityEngine;


//https://devsplorer.wordpress.com/2020/05/18/creating-a-3d-progress-bar-with-shader-graph-devlog-tutorial/
public class ShaderGraphProgressBar : MonoBehaviour
{
    [Range(-1f, 1f)]
    public float _FillRate = -1f; //progress bar starts empty

    public GameObject capsule;
    public Material objectMaterial;
    
    // Start is called before the first frame update
    void Start()
    {
        //objectMaterial = new Material(Shader.Find("Shader Graphs/testgraph")); //creating a material with the shader
        capsule.GetComponent<Renderer>().material = objectMaterial; //new material is applied to the game object
        objectMaterial.SetFloat("_FillRate", _FillRate); //initial value is set 
    }


    public void ChangeValue(float value) //enables changing the value of progress bar
    {
        _FillRate = value;
        //if increase param is true, the progress bar progresses otherwise it deprogresses
        objectMaterial.SetFloat("_FillRate", _FillRate); //Update the value of the progress bar
    }
}