using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class IntegrityIndicator : MonoBehaviour
{

    public IntegrityBar iBar;

    private RectTransform barTransform;
    private float t;

    private float left_indent = -1.0f;
    private float right_indent;
    private float width;
    
    // Start is called before the first frame update
    void Start()
    {
        if (iBar != null)
        {
            print("Bar could be assigned.");
            barTransform = iBar.getTransform();
        }
        else
        {
            print("Bar could not be assigned.");
        }

        right_indent = GetComponentInParent<RectTransform>().sizeDelta.x;
        width = right_indent - left_indent;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeBar(float percentage)
    {
        if (percentage > 1.0f || percentage < 0.0f)
        {
            return;
        }

        float new_pos = (width - width * percentage) * -1.0f;
        // animate the position of the game object...
        barTransform.offsetMax = new Vector2(Mathf.Lerp(barTransform.offsetMax.x, new_pos, t), barTransform.offsetMax.y);

        // .. and increase the t interpolater
        t += 0.1f * Time.deltaTime;
        
    }

}
