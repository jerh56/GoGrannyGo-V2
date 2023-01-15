using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skater_Controller : MonoBehaviour
{
  // Start is called before the first frame update
    private Rigidbody2D RigidBody;
    private Animator Animator;
    private int Direction;
    private float NumRand;


    void Start()
    {
        RigidBody = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        NumRand = Random.Range(-1.0f, 1.0f);
        if (NumRand <= 0 ){
            Direction = -1;
            transform.localScale = new Vector3(0.7f,0.7f,0.7f);
        }else{
            Direction = 1;
            transform.localScale = new Vector3(-0.7f,0.7f,0.7f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(MoveAround());
    }

    void OnCollisionEnter2D(Collision2D other){
        if (other.gameObject.name == "Dog" || other.gameObject.name == "BordeIzq" || other.gameObject.name == "BordeDer" || other.gameObject.name == "Granny" ){
            Direction = Direction * -1;
            if (Direction == -1){
                transform.localScale = new Vector3(0.7f,0.7f,0.7f);
            }else{
                transform.localScale = new Vector3(-0.7f,0.7f,0.7f);
            }
        }   
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.name == "BordeInt"){
            Direction = Direction * -1;
            if (Direction == -1){
                transform.localScale = new Vector3(0.7f,0.7f,0.7f);
            }else{
                transform.localScale = new Vector3(-0.7f,0.7f,0.7f);
            }
        }
    }

   IEnumerator MoveAround(){
        yield return new WaitForSeconds(0.05f);
        RigidBody.velocity = new Vector2(0.2f * 15 * Direction, RigidBody.velocity.y);
   }
}
