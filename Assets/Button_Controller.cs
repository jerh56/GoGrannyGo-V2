using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;


public class Button_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Granny;
    private bool isGrounded;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = (bool)Variables.Object(Granny).Get("Grounded"); 

    }
    public void Jump(){
        isGrounded = (bool)Variables.Object(Granny).Get("Grounded"); 

        if (isGrounded){
            Variables.Object(Granny).Set("ButtonJumpPushed", true);
        }
        else{
            Variables.Object(Granny).Set("ButtonJumpPushed", false);
        }
    
    }
}
