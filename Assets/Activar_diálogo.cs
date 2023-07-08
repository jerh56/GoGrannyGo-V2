using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Activar_diálogo : MonoBehaviour
{

    //UI References
    [SerializeField]
    private TMP_Text dialogueText;

    [SerializeField]
    private Image portraitImage;

    //Dialogue Content
    [SerializeField]
    [TextArea]
    private string [] dialogueWords;

    [SerializeField]
    private Sprite [] portrait;


    

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Interact"))
        {
             dialogueText.text = dialogueWords[1];
             portraitImage.sprite = portrait[1];
        }
    }
}
