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
    public void Initialize(int[] sequence)
    {
        switch (logic.difficulty.GetSeparatedDifficulty())
        {
            //calls method based on difficulty
            case SeparatedDifficulty.Easy:
                EasyColor(sequence);
                break;
            case SeparatedDifficulty.Medium:
                MediumColor(sequence);
                break;
            case SeparatedDifficulty.Hard:
                HardColor(sequence);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    
    /// <summary>
    /// returns a color based on given int 
    /// </summary>
    /// <param name="pos"> number of the given color </param>
    /// <returns></returns>
    private static Color GiveColor(int pos, int[] sequence)
    {
        return sequence[pos] switch
        {
            0 => Color.blue,
            1 => Color.green,
            2 => Color.yellow,
            3 => Color.red,
            _ => black
        };
    }
    
    /// <summary>
    /// colors the Cube for easy mode 
    /// </summary>
    private void EasyColor(int[] sequence)
    {
        for (var i = 0; i < 9; i++)
        {
            var materialCube = cubeComponents[i + 9].GetComponent<MeshRenderer>();
            materialCube.material.color = GiveColor(i, sequence);
        }
    }
    
    /// <summary>
    /// colors the Cube for medium mode
    /// </summary>
    private void MediumColor(int[] sequence)
    {
        for (var i = 0; i < 18; i++)
        {
            var materialCube = cubeComponents[i].GetComponent<MeshRenderer>();
            materialCube.material.color = GiveColor(i, sequence);
        }
    }

    
    /// <summary>
    /// colors the Cube for hard mode 
    /// </summary>
    private void HardColor(int[] sequence)
    {
        // Color TopSide and Front-TopRow
        for (var i = 0; i < 9 + 3; i++)
        {
            var materialCube = cubeComponents[i].GetComponent<MeshRenderer>();
            materialCube.material.color = GiveColor(i, sequence);
        }
        
        // Color Right-TopRow
        for (var i = 0; i < 3; i++)
        {
            var materialCube = cubeComponents[i + 18].GetComponent<MeshRenderer>();
            materialCube.material.color = GiveColor(i + 12, sequence);
            
        }
        
        // Color Front-MiddleRow
        for (var i = 0; i < 3; i++)
        {
            var materialCube = cubeComponents[i + 12].GetComponent<MeshRenderer>();
            materialCube.material.color = GiveColor(i + 15, sequence);
        }
        
        // Color Right-MiddleRow
        for (var i = 0; i < 3; i++)
        {
            var materialCube = cubeComponents[i + 21].GetComponent<MeshRenderer>();
            materialCube.material.color = GiveColor(i + 18, sequence);
        }
        
        // Color Front-BotRow
        for (var i = 0; i < 3; i++)
        {
            var materialCube = cubeComponents[i + 15].GetComponent<MeshRenderer>();
            materialCube.material.color = GiveColor(i + 21, sequence);
        }
        
        // Color Right-BotRow
        for (var i = 0; i < 3; i++)
        {
            var materialCube = cubeComponents[i + 24].GetComponent<MeshRenderer>();
            materialCube.material.color = GiveColor(i + 24, sequence);
        }
    }
}
