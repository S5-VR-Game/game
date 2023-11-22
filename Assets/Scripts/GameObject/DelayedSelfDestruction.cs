using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DelayedSelfDestruction : MonoBehaviour
{
    public float delay;

    private void Update()
    {
        if (delay >= 0f)
        {
            delay -= Time.deltaTime;
        }
        else
        { 
            Destroy(gameObject);
        }
    }
}