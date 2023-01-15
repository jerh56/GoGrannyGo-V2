using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alligator_Controller : MonoBehaviour
{
    private Rigidbody2D RigidBody;
    private float timeRemaining; 
    private bool isJumping;
    private Collider2D m_Collider;

    void Start()
    {
        m_Collider = GetComponent<Collider2D>();
        RigidBody = GetComponent<Rigidbody2D>();
        timeRemaining = 4;
    }

    // Update is called once per frame
    void Update()
    {
        timeRemaining -= Time.deltaTime;
        if (timeRemaining < 1){
            timeRemaining = Random.Range(4.0f, 6.0f);
            RigidBody.velocity = Vector2.up * 12;
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        Debug.Log("OnTriggerEnter2D " + other.gameObject.name);
        if (other.gameObject.name == "Square3"){
            m_Collider.isTrigger = true;
        }
    }
    
    void OnTriggerExit2D(Collider2D other){
        Debug.Log("OnTriggerExit2D " + other.gameObject.name);
        if (other.gameObject.name == "Square3"){
            m_Collider.isTrigger = false;
        }
    }
}
