using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderChange : MonoBehaviour
{

    public Slider size_slider;

    private TextMeshProUGUI size_text;

    public void OnEnable()
    {
        size_text = GameObject.Find("SizeText").GetComponent<TextMeshProUGUI>();
        //Adds a listener to the main slider and invokes a method when the value changes.
        size_slider.onValueChanged.AddListener(delegate {
            ValueChangeCheck();
        });
    }

    // Invoked when the value of the slider changes.
    public void ValueChangeCheck()
    {
        if (size_text != null )
        {
            size_text.text = "Your size: " + size_slider.value + "cm";
        } else
        {
            Debug.Log("No text found!");
        }
            
    }
}
