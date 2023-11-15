using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class TextPanel : MonoBehaviour
    {
        private Animator animator;
    
        // block for defining used inputs
        private KeyCode togglePanel = KeyCode.I;
        private KeyCode test = KeyCode.C;
        private KeyCode dismiss = KeyCode.D;

        public GameObject textField;
        private HUD_Text_Controls textControls;

        private bool active = false;
        private bool alert_dismissed = false;
    
        // Start is called before the first frame update
        void Start()
        {
            animator = gameObject.GetComponentInParent<Animator>();
            textControls = textField.GetComponent<HUD_Text_Controls>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyUp(togglePanel))
            {
                toggleShow();
            }
        
            if (Input.GetKeyUp(test))
            {
                print("Testbutton pressed!");
                displayText("testtest");
            }

            if (Input.GetKeyUp(dismiss))
            {
                dismissText();
            }
        }

        private void toggleShow()
        {
            if (animator != null && active)
            {
                animator.SetBool("open", !animator.GetBool("open"));
                if (animator.GetBool("blink") == true)
                {
                    animator.SetBool("blink", false);
                }
                textControls.toggleState();
            }
        }

        public void displayText(string textToShow)
        {
            
            if (!animator.GetBool("open"))
            {
                // toggleShow();
                animator.SetBool("blink", true);
                print(animator.layerCount);
                alert_dismissed = false;
            }
            active = true;
            textControls.changeText(textToShow);
        }

        public void dismissText()
        {
            
            if (animator.GetBool("open"))
            {
                toggleShow();
            }

            if (animator.GetBool("blink"))
            {
                alert_dismissed = true;
                animator.SetBool("blink", false);
            }
            active = false;
        }
    }
}
