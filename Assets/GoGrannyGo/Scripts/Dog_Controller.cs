using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class Dog_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Game;
    public GameObject Granny;
    private bool isGrannyDyieng;
    private Rigidbody2D RigidBody;
    private float timeRemaining; 
    private int Direction;
    private float NumRand;


    void Start()
    {
        RigidBody = GetComponent<Rigidbody2D>();
        timeRemaining = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timeRemaining -= Time.deltaTime;
        if (timeRemaining < 1){
            timeRemaining = Random.Range(3.0f, 5.0f);
            NumRand = Random.Range(-1.0f, 1.0f);
            //Direction = (int)(Direction * NumRand);
            if (NumRand >= 0){
                Direction = 1;
                transform.localScale = new Vector3(-1,1,1);

            }
             else{
                Direction = -1;
                transform.localScale = new Vector3(1,1,1);
            }

        }
        StartCoroutine(MoveAround());
        /*Debug.Log(timeRemaining);
        Debug.Log(Direction);
        Debug.Log(NumRand);*/
    }

   /*  void OnTriggerEnter2D(Collider2D other){
        isGrannyDyieng = (bool)Variables.Object(Granny).Get("isGrannyDyieng");
        if (other.gameObject.name == "Granny" && !isGrannyDyieng){
            Game_Controller Game_Controller = Game.GetComponent<Game_Controller>();
            Game_Controller.GetDeath();
            Granny_Controller_2 Granny_Controller = Granny.GetComponent<Granny_Controller_2>();
            Granny_Controller.Die();
           // Destroy(other.gameObject);           
        }
    }
 */
    IEnumerator MoveAround(){
        yield return new WaitForSeconds(0.05f);
        RigidBody.velocity = new Vector2(0.2f * 5 * Direction, RigidBody.velocity.y);
        // if (Horizontal < 0){
        //     Granny.transform.localScale = new Vector3(-1,1,1);
        // }
        // else if(Horizontal > 0){
        //     Granny.transform.localScale = new Vector3(1,1,1);
        // }
    }
}
