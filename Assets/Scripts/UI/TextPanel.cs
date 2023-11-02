using UnityEngine;

public class TextPanel : MonoBehaviour
{
    private Animator animator;
    
    // block for defining used inputs
    private KeyCode togglePanel = KeyCode.I;


    private const double defaultPosY = -120;
    private const double hiddenPosY = 130;

    public GameObject textField;
    private HUD_Text_Controls textControls;
    
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
    }

    private void toggleShow()
    {
        if (animator != null)
        {
            animator.SetBool("open", !animator.GetBool("open"));
            textControls.toggleState();
            print(animator.GetBool("open"));
        }
    }
    
}
