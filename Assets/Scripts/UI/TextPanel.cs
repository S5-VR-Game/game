using Evaluation;
using Game.Tasks;
using System;
using UnityEngine;

namespace UI
{
    public class TextPanel : MonoBehaviour
    {
        private Animator animator;
    
        // block for defining used inputs
        private KeyCode togglePanel = KeyCode.JoystickButton2; // vr controller button A
        private KeyCode dismiss = KeyCode.D;

        public GameObject textField;
        private HUD_Text_Controls textControls;

        private bool active = false;
        private bool alert_dismissed = false;

        public event Action OnTextOpened;

        private GameTask _gameTask;
    
        // Start is called before the first frame update
        void Start()
        {
            animator = gameObject.GetComponentInParent<Animator>();
            textControls = textField.GetComponent<HUD_Text_Controls>();
            _gameTask = null;
        }

        // Update is called once per frame
        void Update()
        {
            // TODO assign controller buttons to toggle and dismiss ui text
            if (Input.GetKeyUp(togglePanel))
            {
                ToggleShow();
                IncrementHUDUsage();
            }

            if (Input.GetKeyUp(dismiss))
            {
                DismissText();
            }
        }

        /// <summary>
        /// Increments the HUD-Usage for a specific Task Type
        /// </summary>
        private void IncrementHUDUsage()
        {
            if (_gameTask == null)
            {
                return;
            }
            _gameTask.GetEvaluationDataWrapper().IncrementMapEntry(_gameTask, DictTypes.TaskFailed);
        }

        /// <summary>
        /// This method toggles the text panel visibility
        /// </summary>
        public void ToggleShow()
        {
            if (animator != null && active)
            {
                animator.SetBool("open", !animator.GetBool("open"));
                
                // notify listeners that text panel was opened
                if (animator.GetBool("open"))
                {
                    OnTextOpened?.Invoke();
                }
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

        /// <summary>
        /// Registers the Current Task by Collision
        /// </summary>
        /// <param name="task">the new Task for this text panel.</param>
        public void RegisterCurrentTask(GameTask task)
        {
            _gameTask = task;
        }

        /// <summary>
        /// Removes that specific task when the Player leaves the collision.
        /// </summary>
        public void DeregisterCurrentTask()
        {
            _gameTask = null;
        }
    }
}
