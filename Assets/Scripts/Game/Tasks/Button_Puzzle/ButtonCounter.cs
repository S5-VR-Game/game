using System.Collections;
using System.Collections.Generic;
using Prefabs.Game.Tasks.Button_testing;
using TMPro;
using UnityEngine;

public class ButtonCounter : MonoBehaviour
{
    [SerializeField] private TextMeshPro textField;

    [SerializeField] private ButtonSequenceLogic logic;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Test(0);
        } 
        else if (Input.GetKeyDown(KeyCode.X))
        {
            Test(1);
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            Test(2);
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            Test(3);
        }  
    }

    public void Test(int color)
    {
        textField.text = color.ToString();
        print(color.ToString());
        logic.ButtonCheck((ColorCode)color);
    }
}
