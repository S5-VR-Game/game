using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderChange : MonoBehaviour
{

    public Slider player_height_slider;

    private TextMeshProUGUI player_height_text;

    public void OnEnable()
    {
        player_height_text = GameObject.Find("PlayerHeightText").GetComponent<TextMeshProUGUI>();
        //Adds a listener to the main slider and invokes a method when the value changes.
        player_height_slider.onValueChanged.AddListener(delegate {
            ValueChangeCheck();
        });
    }

    // Invoked when the value of the slider changes.
    public void ValueChangeCheck()
    {
        player_height_text.text = "Your size: " + player_height_slider.value + "cm";
        float player_height = (((player_height_slider.value - 140) / 150) + 1);
        PlayerPrefs.SetFloat("PlayerHeight", player_height);
    }
}
