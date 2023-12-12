using System.Collections;
using System.Collections.Generic;
using Game;
using Game.Tasks;
using Prefabs.Game.Tasks.Button_testing;
using UnityEngine;


public class ButtonSequenceLogic : GameTask
{
    
    public GameObject cube;
    
    public static int[] colorSequence;
    private bool done = false;
    private int level = 0;
    int sequenceLenght;
    public ShaderGraphProgressBar progressBar;
    private float stepSize;
    
    public override void Initialize()
    {
        //difficulty.SetValue(1);
        if (difficulty.GetValue() == 0f)
        {
            
            sequenceLenght = 9;
        } else if (difficulty.GetValue() == 0.5f)
        {
            sequenceLenght = 18;
        }
        else if (difficulty.GetValue() == 1.0f)
        {
            sequenceLenght = 27;
        }

        stepSize = 2.0f / (float)sequenceLenght;

        colorSequence = new int[sequenceLenght];
        //create Sequence
        for (var i = 0; i < sequenceLenght; i++)
        {
            colorSequence[i] = Random.Range(0, 4);
            print(colorSequence[i]);
        }

        progressBar.ChangeValue(0f);
    }

    protected override void BeforeStateCheck()
    {
        
    }

    protected override TaskState CheckTaskState()
    {
        if (done)
        {
            return TaskState.Successful;
        }
        else
        {
            return TaskState.Ongoing;
        }
    }

    protected override void AfterStateCheck()
    {
        if (currentTaskState != TaskState.Ongoing)
        {
            DestroyTask();
        }
    }
    
    //compares the pressed button with the next Color in the Sequence
    public void ButtonCheck(ColorCode color)
    {
        if (color == (ColorCode) colorSequence[level])
        {
            if (level == sequenceLenght - 1)
            {
                
                done = true;
                print("done");
            }
            level ++;
        }
        else
        {
            print("reset");
            level = 0;
        }
        //print("level: " + level);
        //print("steps: " + stepSize);
        //print("calc: " + ((float)level * stepSize - 1f));
        progressBar.ChangeValue((float)level * stepSize - 1f);
    }

    public ButtonSequenceLogic() : base("ButtonSequence", "description")
    {
    }
}
