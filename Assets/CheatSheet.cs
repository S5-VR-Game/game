using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
public class CheatSheet : MonoBehaviour
{
    public GameObject[] pictures = new GameObject[3];
    // Start is called before the first frame update
    void Start(Difficulty difficulty)
    {
        
        if (difficulty.GetValue() == 0f)
        {
            pictures[0].GetComponent<Renderer>().enabled = true;
            pictures[1].GetComponent<Renderer>().enabled = false;
            pictures[2].GetComponent<Renderer>().enabled = false;
        } else if (difficulty.GetValue() == 0.5f)
        {
            pictures[0].GetComponent<Renderer>().enabled = false;
            pictures[1].GetComponent<Renderer>().enabled = true;
            pictures[2].GetComponent<Renderer>().enabled = false;
        } else if (difficulty.GetValue() == 1.0f)
        {
            pictures[0].GetComponent<Renderer>().enabled = false;
            pictures[1].GetComponent<Renderer>().enabled = false;
            pictures[2].GetComponent<Renderer>().enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
