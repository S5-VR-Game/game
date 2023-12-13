using System;
using Game;
using Game.Tasks;
using Prefabs.Game.Tasks.Button_testing;
using UnityEngine;
using Random = UnityEngine.Random;


public class ButtonSequenceLogic : GameTask
{
    
    public GameObject cube;
    
    public static int[] colorSequence;
    private bool _done;
    private int _level;
    private int _sequenceLenght;
    public ShaderGraphProgressBar progressBar;
    private float _stepSize;
    
    public override void Initialize()
    {
        // crates sequence lenght based on difficulty
        _sequenceLenght = difficulty.GetSeparatedDifficulty() switch
        {
            SeparatedDifficulty.Easy => 9,
            SeparatedDifficulty.Medium => 18,
            SeparatedDifficulty.Hard => 27,
            _ => throw new ArgumentOutOfRangeException()
        };

        //calculates step size for the progress bar
        _stepSize = 2.0f / _sequenceLenght;
        //create Sequence
        colorSequence = new int[_sequenceLenght];
        for (var i = 0; i < _sequenceLenght; i++)
        {
            colorSequence[i] = Random.Range(0, 4);
        }

        progressBar.ChangeValue(0f);
    }

    protected override void BeforeStateCheck()
    {
        
    }

    protected override TaskState CheckTaskState()
    {
        return _done ? TaskState.Successful : TaskState.Ongoing;
    }

    protected override void AfterStateCheck()
    {
        if (currentTaskState != TaskState.Ongoing)
        {
            DestroyTask();
        }
    }
    
    /// <summary>
    /// compares the pressed button with the next Color in the Sequence
    /// </summary>
    /// <param name="color"> the color of the pressed button</param>
    public void ButtonCheck(ColorCode color)
    {
        if (color == (ColorCode) colorSequence[_level])
        {
            if (_level == _sequenceLenght - 1)
            {
                _done = true;
            }
            _level ++;
        }
        else
        {
            _level = 0;
        }
        progressBar.ChangeValue(_level * _stepSize - 1f);
    }
    
    /// <summary>
    /// is triggered if a button is pressed
    /// </summary>
    /// <param name="color"></param>
    public void ButtonPress(int color)
    {
        ButtonCheck((ColorCode)color);
    }

    public ButtonSequenceLogic() : base("ButtonSequence", "description")
    {
    }
}
