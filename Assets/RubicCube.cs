using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubicCube : MonoBehaviour
{

    public GameObject[] CubeComponents;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void colorCubes(int cubeAmount, int[] CubeColors)
    {
        CubeComponents = new GameObject[cubeAmount];
        foreach (GameObject cube in CubeComponents)
                {
                    var materialCube = cube.GetComponent<Material>();
                }
    }
}
