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

    private readonly float leftIndent = -1.0f;
    private float rightIndent;
    private float width;
    
    private readonly KeyCode increase = KeyCode.UpArrow;
    private readonly KeyCode decrease = KeyCode.DownArrow;
    private float current_percentage = 1.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        if (iBar != null)
        {
            barTransform = iBar.getTransform();
        }

        rightIndent = GetComponentInParent<RectTransform>().sizeDelta.x;
        width = rightIndent - leftIndent;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(increase))
        {
            changeBar(current_percentage + 0.1f);
        }

        if (Input.GetKeyUp(decrease))
        {
            changeBar(current_percentage - 0.1f);
        }
        
        float newPos = (width - width * current_percentage) * -1.0f;
        // animate the position of the game object...
        barTransform.offsetMax = new Vector2(Mathf.Lerp(barTransform.offsetMax.x, newPos, t), barTransform.offsetMax.y);

        // .. and increase the t interpolater
        t += 0.001f * Time.deltaTime;
    }

    public void changeBar(float percentage)
    {
        if (percentage > 1.0001f || percentage < 0.0f)
        {
            return;
        }
        
        current_percentage = percentage;
        

        
    }

}
