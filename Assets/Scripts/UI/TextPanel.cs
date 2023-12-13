using UnityEngine;

namespace UI
{
    public class TextPanel : MonoBehaviour
    {
        private Animator animator;
    
        // block for defining used inputs
        private KeyCode togglePanel = KeyCode.I;
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
            // TODO assign controller buttons to toggle and dismiss ui text
            if (Input.GetKeyUp(togglePanel))
            {
                ToggleShow();
            }

            if (Input.GetKeyUp(dismiss))
            {
                DismissText();
            }
        }
        
        /// <summary>
        /// This method toggles the text panel visibility
        /// </summary>
        private void ToggleShow()
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

        /// <summary>
        /// Set a text to show on the text panel.
        /// Can be a single string, text will be formatted by the panel.
        /// </summary>
        /// <param name="textToShow">string you want to show on the panel</param>
        public void DisplayText(string textToShow)
        {
            
            if (!animator.GetBool("open"))
            {
                // toggleShow();
                animator.SetBool("blink", true);
                alert_dismissed = false;
            }
            active = true;
            textControls.changeText(textToShow);
        }

        /// <summary>
        /// Dismisses the current text and the text panel will be hidden.
        /// <para />
        /// Panel can't be opened again until new text is received.
        /// </summary>
        public void DismissText()
        {
            
            if (animator.GetBool("open"))
            {
                ToggleShow();
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
