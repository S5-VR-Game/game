using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class TimerPanel : MonoBehaviour
{
    public TextMeshProUGUI textField;
    private HUD_Text_Controls textControls;
    
    private float _timerSecond = 0;
    
    private bool firstStart = true;
    private bool textMeshValid = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        // set timer to 20 mins
        UpdateTime(1200.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (firstStart)
        {
            if (textField != null)
            {
                textMeshValid = true;
            }
            firstStart = false;
        }
        
        if (textMeshValid)
        {
            var minutes = (int)_timerSecond / 60;
            var seconds = (int)_timerSecond % 60;
            var millis = (int)((_timerSecond - Math.Truncate(_timerSecond)) * 100.0f);

            var textToShow =  $"{minutes}:{seconds}.{millis:00}";
            textField.text = textToShow;
        }
    }

    void UpdateTime(float newTime)
    {
        _timerSecond = newTime;
    }
}
