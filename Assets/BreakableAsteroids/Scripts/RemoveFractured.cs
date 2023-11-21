using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class RemoveFractured : MonoBehaviour
{
    private float timerRemaining = 5f;

    private void Update()
    {
        if (timerRemaining >= 0f)
        {
            timerRemaining -= Time.deltaTime;
        }
        else
        { 
            Destroy(gameObject);
        }
    }
}