using TMPro;
using UnityEngine;

public class HUD_Text_Controls : MonoBehaviour
{

    // block for used game objects
    private TextMeshProUGUI textMesh;
    private TextMeshProUGUI pageCountText;

    // block for defining used inputs
    private KeyCode nextPage = KeyCode.U;
    private KeyCode prevPage = KeyCode.Z;

    private int currentPage = 1;
    private bool shown = false;
    

    void Start()
    {
        textMesh = gameObject.GetComponent<TextMeshProUGUI>();
        pageCountText = GameObject.Find("HUD_Canvas/TextPanel/PageCount").GetComponent<TextMeshProUGUI>();
        pageCountText.text = currentPage + " / " + textMesh.textInfo.pageCount;
    }

    private void turnPage(int turnamount)
    {
        if (turnamount < 0)
        {
            if (currentPage > 1)
            {
                currentPage -= 1;
            }
        }

        else if (turnamount > 0)
        {
            if (currentPage < textMesh.textInfo.pageCount)
            {
                currentPage += 1;
            }
        }
        
    }

    private void updatePageCountText()
    {

        if (textMesh.textInfo.pageCount > 1)
        {
            textMesh.pageToDisplay = currentPage;
            pageCountText.text = currentPage + " / " + textMesh.textInfo.pageCount; 
        }
        else
        {
            pageCountText.text = " "; 
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (shown)
        {
            if (Input.GetKeyUp(prevPage))
            {
                turnPage(-1);
            }

            if (Input.GetKeyUp(nextPage))
            {
                turnPage(1);
            }
        }

        
        updatePageCountText();
    }

    public void toggleState()
    {
        shown = !shown;
    }

    public void changeText(string textToShow)
    {
        textMesh.text = textToShow;
    }
    
    
}
