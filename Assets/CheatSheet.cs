using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
public class CheatSheet : MonoBehaviour
{
    public ButtonSequenceLogic logic;
    public GameObject[] pictures = new GameObject[3];
    // Start is called before the first frame update
    void Start()
    {
        var seperatedDifficulty = logic.difficulty.GetSeparatedDifficulty();
        pictures[0].SetActive(seperatedDifficulty == SeparatedDifficulty.Easy);
        pictures[1].SetActive(seperatedDifficulty == SeparatedDifficulty.Medium);
        pictures[2].SetActive(seperatedDifficulty == SeparatedDifficulty.Hard);
  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
