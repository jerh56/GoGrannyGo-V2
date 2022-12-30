using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog_Controller_2 : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D RigidBody;
    private Animator Animator;
    private float timeRemaining; 
    private int Direction;
    private float NumRand;


    void Start()
    {
        RigidBody = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        timeRemaining = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timeRemaining -= Time.deltaTime;
        if (timeRemaining < 1){
            timeRemaining = Random.Range(3.0f, 5.0f);
            NumRand = Random.Range(-1.0f, 1.0f);
           if (NumRand > -0.3 && NumRand < 0.3){
                Animator.SetBool("run", false);
                Direction = 0;
            }
             else if (NumRand < 0){
                Direction = -1;
                transform.localScale = new Vector3(0.5f,0.5f,0.5f);
                Animator.SetBool("run", true);
            }
            else{
                Direction = 1;
                transform.localScale = new Vector3(-0.5f,0.5f,0.5f);
                Animator.SetBool("run", true);
            }

        }
        StartCoroutine(MoveAround());
    }

    void OnCollisionEnter2D(Collision2D other){
        if (other.gameObject.name == "Dog" || other.gameObject.name == "BordeIzq" || other.gameObject.name == "BordeDer" || other.gameObject.name == "Granny"  ){
            Direction = Direction * -1;
            if (Direction == -1){
                transform.localScale = new Vector3(0.5f,0.5f,0.5f);
            }else{
                transform.localScale = new Vector3(-0.5f,0.5f,0.5f);
            }
        }   
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.name == "BordeInt"){
            Direction = Direction * -1;
            if (Direction == -1){
                transform.localScale = new Vector3(0.5f,0.5f,0.5f);
            }else{
                transform.localScale = new Vector3(-0.5f,0.5f,0.5f);
            }
        }
    }
   IEnumerator MoveAround(){
        yield return new WaitForSeconds(0.05f);
        RigidBody.velocity = new Vector2(0.2f * 15 * Direction, RigidBody.velocity.y);
   }
}