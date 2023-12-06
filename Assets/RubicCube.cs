using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RubicCube : MonoBehaviour
{

    public GameObject[] cubeComponents = new GameObject[27];
    // Start is called before the first frame update
    void Start()
    {
        ColorCubes(9);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ColorCubes(int cubeAmount)
    {
        int counter = 0;
        for (int i = 0; i < cubeAmount; i++)
        {
            
        }
        foreach (GameObject cube in cubeComponents)
        {
            var materialCube = cube.GetComponent<Material>();
            materialCube.color = Color.blue;
            /*materialCube.color = ButtonSequenceLogic.colorSequence[counter] switch
            {
                0 => Color.blue,
                1 => Color.green,
                2 => Color.yellow,
                3 => Color.red,
                _ => materialCube.color
            };**/
        }
    }

    public void difficultySplitup(int difficulty)
    {
        if (difficulty == 0)
        {
            ColorCubes(9);
        }
        else if (difficulty == 1)
        {
            ColorCubes(18);
        }
        else
        {
            ColorCubes(27);
        }
    }
}
