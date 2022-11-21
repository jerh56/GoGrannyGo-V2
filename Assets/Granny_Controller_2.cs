using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
 

public class Granny_Controller_2 : MonoBehaviour
{
    // Start is called before the first frame update

    Collider2D m_Collider;
   // Variables m_Variables;
    private float timer = 0.0f;
    private bool timer_on = false;
    private bool isCrouched = false; 
    public GameObject Granny;
    void Start()
    {
        m_Collider = GetComponent<Collider2D>();
        //m_Variables = GetComponent<Variables>();
        Debug.Log(Variables.Object(Granny).Get("isCrouched"));


    }

    // Update is called once per frame
    void Update()
    {
        /*if (timer > 0.55){
            timer_on = false;
            timer = 0.0f;
            m_Collider.enabled = true;
        }
        
        if (timer_on){
            timer += Time.deltaTime;
            Debug.Log(timer);
        }*/
    
    }

    void OnCollisionExit2D(Collision2D collision)
    {
      /*  Debug.Log("OnCollisionExit2D" + collision.gameObject.name);
        if (!timer_on){
            isCrouched = (bool)Variables.Object(Granny).Get("isCrouched");
        }
        
        if (collision.gameObject.name == "Platform_2" && isCrouched && !timer_on){
            m_Collider.enabled = false;
            timer_on = true;
        }
        //collision.enable = false;
        //collision.GameObject.enable = false;

         //collision.enabled = false;
         */

    }

    void OnTriggerExit2D(Collider2D other){
        Debug.Log("OnTriggerExit2D" + other.gameObject.name);
        if (other.gameObject.name == "Platform_2" && m_Collider.isTrigger == true){
            m_Collider.isTrigger = false;
        }
        //Granny.GetComponent<Collider2D>().isTrigger = false;


    }
}
