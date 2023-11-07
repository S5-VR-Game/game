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
    
        // Start is called before the first frame update
        void Start()
        {
            animator = gameObject.GetComponent<Animator>();
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
                textControls.toggleState();
            }
        }

        public void displayText(string textToShow)
        {
            
            if (!animator.GetBool("open") && !active)
            {
                active = true;
                textControls.changeText(textToShow);
                toggleShow();
            }

            else if (!animator.GetBool("open"))
            {

            }
        }

        public void dismissText()
        {
            
            if (animator.GetBool("open"))
            {
                toggleShow();
            }
            active = false;
        }
    }
}
