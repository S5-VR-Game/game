using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using static UnityEngine.Color;

public class RubicsCube : MonoBehaviour
{
    public ButtonSequenceLogic logic;
    public GameObject[] cubeComponents = new GameObject[27];
    public Material[] colors = new Material[4];

    // Start is called before the first frame update
    private void Start()
    {
        switch (logic.difficulty.GetSeparatedDifficulty())
        {
            //calls method based on difficulty
            case SeparatedDifficulty.Easy:
                Easy_MediumColor(0);
                break;
            case SeparatedDifficulty.Medium:
                Easy_MediumColor(1);
                break;
            case SeparatedDifficulty.Hard:
                HardColor();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    //returns a color based on given int
    private static Color GiveColor(int pos)
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
    // colors the Cube for easy and medium mode
    private void Easy_MediumColor(int difficulty)
    {
        var amount = difficulty == 0 ? 9 : 18;
        {
            
        }
        for (var i = 0; i < amount; i++)
        {
            var materialCube = cubeComponents[i + 9].GetComponent<MeshRenderer>();
            materialCube.material.color = GiveColor(i);
        }
        
    }

    // colors the Cube for hard mode
    private void HardColor()
    {
        // Color TopSide and Front-TopRow
        for (var i = 0; i < 9 + 3; i++)
        {
            var materialCube = cubeComponents[i].GetComponent<MeshRenderer>();
            materialCube.material.color = GiveColor(i);
        }
        
        // Color Right-TopRow
        for (var i = 0; i < 3; i++)
        {
            var materialCube = cubeComponents[i + 18].GetComponent<MeshRenderer>();
            materialCube.material.color = GiveColor(i + 12);
            
        }
        
        // Color Front-MiddleRow
        for (var i = 0; i < 3; i++)
        {
            var materialCube = cubeComponents[i + 12].GetComponent<MeshRenderer>();
            materialCube.material.color = GiveColor(i + 15);
        }
        
        // Color Right-MiddleRow
        for (var i = 0; i < 3; i++)
        {
            var materialCube = cubeComponents[i + 21].GetComponent<MeshRenderer>();
            materialCube.material.color = GiveColor(i + 18);
        }
        
        // Color Front-BotRow
        for (var i = 0; i < 3; i++)
        {
            var materialCube = cubeComponents[i + 15].GetComponent<MeshRenderer>();
            materialCube.material.color = GiveColor(i + 21);
        }
        
        // Color Right-BotRow
        for (var i = 0; i < 3; i++)
        {
            var materialCube = cubeComponents[i + 24].GetComponent<MeshRenderer>();
            materialCube.material.color = GiveColor(i + 24);
        }
    }

}
