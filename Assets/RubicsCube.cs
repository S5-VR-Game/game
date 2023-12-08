using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using static UnityEngine.Color;

public class RubicsCube : MonoBehaviour
{

    public GameObject[] cubeComponents = new GameObject[27];
    public Material[] colors = new Material[4];

    // Start is called before the first frame update
    void Start(Difficulty difficulty)
    {
        if (difficulty.GetValue() == 0f)
        {
            EasyColor ();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private Color giveColor(int pos)
    {
        return ButtonSequenceLogic.colorSequence[pos] switch
        {
            0 => Color.blue,
            1 => Color.green,
            2 => Color.yellow,
            3 => Color.red,
            _ => black
        };
    }

    private void EasyColor()
    {
        for (var i = 0; i < 9; i++)
        {
            var materialCube = cubeComponents[i + 9].GetComponent<MeshRenderer>();
            materialCube.material.color = giveColor(i);


        }
        
    }

    private void MediumColor()
    {
        for (var i = 0; i < 18; i++)
        {
            var materialCube = cubeComponents[i].GetComponent<MeshRenderer>();
            materialCube.material.color = giveColor(i);
        }
    }

    private void HardColor()
    {
        // Color TopSide and Front-TopRow
        for (var i = 0; i < 9 + 3; i++)
        {
            var materialCube = cubeComponents[i].GetComponent<MeshRenderer>();
            materialCube.material.color = giveColor(i);
        }
        
        // Color Right-TopRow
        for (var i = 0; i < 3; i++)
        {
            var materialCube = cubeComponents[i + 18].GetComponent<MeshRenderer>();
            materialCube.material.color = giveColor(i + 12);
            
        }
        
        // Color Front-MiddleRow
        for (var i = 0; i < 3; i++)
        {
            var materialCube = cubeComponents[i + 12].GetComponent<MeshRenderer>();
            materialCube.material.color = giveColor(i + 15);
        }
        
        // Color Right-MiddleRow
        for (var i = 0; i < 3; i++)
        {
            var materialCube = cubeComponents[i + 21].GetComponent<MeshRenderer>();
            materialCube.material.color = giveColor(i + 18);
        }
        
        // Color Front-BotRow
        for (var i = 0; i < 3; i++)
        {
            var materialCube = cubeComponents[i + 15].GetComponent<MeshRenderer>();
            materialCube.material.color = giveColor(i + 21);
        }
        
        // Color Right-BotRow
        for (var i = 0; i < 3; i++)
        {
            var materialCube = cubeComponents[i + 24].GetComponent<MeshRenderer>();
            materialCube.material.color = giveColor(i + 24);
        }
    }

}
