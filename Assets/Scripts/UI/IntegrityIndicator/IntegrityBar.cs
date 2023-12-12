using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntegrityBar : MonoBehaviour
{
    private RectTransform transform;
    // Start is called before the first frame update
    void Start()
    {
        transform = gameObject.GetComponent<RectTransform>();
        if (transform != null)
        {
            print("transform object ok!");
        }
        else
        {
            print("transform not ok.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public RectTransform getTransform()
    {
        return transform;
    }
}
