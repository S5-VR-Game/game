using UnityEngine;


//https://devsplorer.wordpress.com/2020/05/18/creating-a-3d-progress-bar-with-shader-graph-devlog-tutorial/
public class ShaderGraphProgressBar : MonoBehaviour
{
    [Range(-0.51f, 0.51f)]
    public float _FillRate = 0.51f; //progress bar starts empty
    Material objectMaterial;

    float stepSize = 0.1f; //progress is done by this value
    
    // Start is called before the first frame update
    void Start()
    {
        objectMaterial = new Material(Shader.Find("Shader Graphs/testgraph")); //creating a material with the shader
        gameObject.GetComponent<Renderer>().material = objectMaterial; //new material is applied to the game object
        objectMaterial.SetFloat("_FillRate", _FillRate); //initial value is set 
    }

                                        
    public void ChangeValue(bool increase) //enables changing the value of progress bar
    {                                   //if increase param is true, the progress bar progresses otherwise it deprogresses
        if (increase)
        {
            _FillRate += stepSize; //progress increased
        }
        else
        {
            _FillRate -= stepSize; //progress decreased
        }
        objectMaterial.SetFloat("_FillRate", _FillRate); //Update the value of the progress bar
    }
}