using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class AltMarkerBehaviour : MonoBehaviour
{
    public GameObject marker;


    public void Start()
    {
        marker = gameObject.GetNamedChild("Canvas");
        marker.SetActive(false);
    }
}
