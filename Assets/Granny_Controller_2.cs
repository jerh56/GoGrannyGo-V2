using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
 

public class Granny_Controller_2 : MonoBehaviour
{
    // Start is called before the first frame update

    Collider2D m_Collider;
    public GameObject Granny;
    public Joystick Joystick;
    private float Horizontal;
    private float Vertical;
    private bool isGrounded;
    void Start()
    {
        m_Collider = GetComponent<Collider2D>();
        Debug.Log(Variables.Object(Granny).Get("isCrouched"));
    }

    // Update is called once per frame
    void Update()
    {
        Vertical = Joystick.Vertical;
        Horizontal = Joystick.Horizontal;
        Debug.Log(Horizontal);
        Debug.Log(Vertical);

        if ( Mathf.Abs(Vertical) < 0.9 ){
            Vertical = 0;
        }

        if ( Mathf.Abs(Horizontal) < 0.9 ){
            Horizontal = 0;
        }

        if (Horizontal == 0){
            Horizontal = Input.GetAxis("Horizontal");
        }
        Variables.Object(Granny).Set("Horizontal", Horizontal);


        if (Vertical == 0){
            Vertical = Input.GetAxis("Vertical");
        }

        if (Vertical < 0){
            isGrounded = (bool)Variables.Object(Granny).Get("Grounded"); 
            if (isGrounded){
                Variables.Object(Granny).Set("Crouched", true);
            }
        }
        else{
            Variables.Object(Granny).Set("Crouched", false);
        }
    }

    void OnTriggerExit2D(Collider2D other){
        Debug.Log("OnTriggerExit2D" + other.gameObject.name);
        if (other.gameObject.name == "Platform_2" && m_Collider.isTrigger == true){
            m_Collider.isTrigger = false;
        }
    }
}
