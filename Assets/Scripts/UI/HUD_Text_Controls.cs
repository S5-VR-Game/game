using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HUD_Text_Controls : MonoBehaviour
{

    private TextMeshProUGUI textMesh;
    private KeyCode test = KeyCode.C;

    private KeyCode nextPage = KeyCode.U;
    private KeyCode prevPage = KeyCode.Z;

    private int currentPage = 1;

    void Start()
    {
        textMesh = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(test))
        {
            print("Testbutton pressed!");
        }

        if (Input.GetKeyUp(prevPage))
        {
            if (currentPage > 1)
            {
                currentPage -= 1;
                textMesh.pageToDisplay = currentPage;
            }
            
        }

        if (Input.GetKeyUp(nextPage))
        {
            if (currentPage < textMesh.textInfo.pageCount)
            {
                currentPage += 1;
                textMesh.pageToDisplay = currentPage;
            }
            
        }
    }
}
