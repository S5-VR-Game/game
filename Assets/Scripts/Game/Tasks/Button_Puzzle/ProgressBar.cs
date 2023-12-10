using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    
    private Slider slider;
    public float FillSpeed = 0.5f;
    private int maxVal = 18;

    private float targetProgress = 0;


    private void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        trackProgress(true);
    }

    // Update is called once per frame
    void Update()
    {
        trackProgress(true);
        if (slider.value < targetProgress)
        {
            slider.value += FillSpeed * Time.deltaTime;
        }
        else if (targetProgress == 0)
        {
            slider.value += FillSpeed * Time.deltaTime;
        }
        
    }

    public void trackProgress(bool rightColor)
    {
        int percentage = 100 / maxVal;
        if (rightColor)
        {
            targetProgress = slider.value + percentage;
        }
        else
        {
            targetProgress = 0;
        }
    }
}
