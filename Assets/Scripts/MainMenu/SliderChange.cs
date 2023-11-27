using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenu
{
    public class SliderChange : MonoBehaviour
    {
        // stores the object of the slider and the textbox
        public Slider playerHeightSlider;
        private TextMeshProUGUI _playerHeightText;

        public void OnEnable()
        {
            _playerHeightText = GameObject.Find("PlayerHeightText").GetComponent<TextMeshProUGUI>();
            // adds a listener to the main slider and invokes a method when the value changes.
            playerHeightSlider.onValueChanged.AddListener(delegate {
                ValueChangeCheck();
            });
        }

        // invoked when the value of the slider changes.
        private void ValueChangeCheck()
        {
            _playerHeightText.text = "Your height: " + playerHeightSlider.value + "cm";
            var playerHeight = (((playerHeightSlider.value - 140) / 150) + 1);
            PlayerPrefs.SetFloat("PlayerHeight", playerHeight);
        }
    }
}
