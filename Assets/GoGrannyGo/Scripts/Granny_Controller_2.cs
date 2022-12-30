using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.UI;
 

public class Granny_Controller_2 : MonoBehaviour
{
    // Start is called before the first frame update

    Collider2D m_Collider;
    private Rigidbody2D RigidBody;
    public GameObject Granny;
    public Joystick Joystick;
    public GameObject Game;
    public AudioClip Scream_Sound;
    public AudioClip Power_Sound;
    public AudioClip Splash_Sound;
    public GameObject SplashPrefab;
    private Animator Animator;
    private AudioSource AudioData;
    private float Horizontal;
    private float Vertical;
    private bool isGrounded;
    private bool isFalling;
    private bool Crouched;
    private bool Grounded;
    private bool ButtonJumpPushed;
    private bool isOnPlatform_2;
    private bool isGameOver;
    private bool Granny_Die;
    private bool isGrannyDyieng;
    private bool isFlashing;
    private int Energy;
    private int Direction; //1:Der -1:Izq 
    private float timeRemaining;
    private float DieTime;
    private float lastCP; // last Control Point

    void Start()
    {
        m_Collider = GetComponent<Collider2D>();
        Animator = GetComponent<Animator>();
        RigidBody = GetComponent<Rigidbody2D>();
        AudioData = GetComponent<AudioSource>();
        Variables.Object(Granny).Set("isGameOver", false);
        Granny_Die = false;
        DieTime = 2;
        timeRemaining = 2;
        lastCP = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Vertical = Joystick.Vertical;
        Horizontal = Joystick.Horizontal;
        Crouched = (bool)Variables.Object(Granny).Get("Crouched");
        Grounded = (bool)Variables.Object(Granny).Get("Grounded"); 
        ButtonJumpPushed = (bool)Variables.Object(Granny).Get("ButtonJumpPushed"); 
        isGameOver = (bool)Variables.Object(Granny).Get("isGameOver");
        isGrounded = (bool)Variables.Object(Granny).Get("Grounded"); 
        Variables.Object(Granny).Set("isCrouched", Crouched);
        Animator.SetBool("crouch", Crouched);
        Energy = (int)Variables.Object(Granny).Get("Energy");
        // Debug.Log(Energy);
        if (Granny_Die){
           if (DieTime > 0){
                DieTime -= Time.deltaTime;
           }else if (!isGameOver) {
                StartCoroutine(Flasher());
                Animator.SetBool("hurt", false);
                Variables.Object(Granny).Set("isGrannyDyieng", false);
                DieTime = 2;
                Granny_Die = false;
                // Granny returns to the last control point
                //Granny.transform.position = new Vector2(Granny.transform.position.x - 3, 4);
                Granny.transform.position = new Vector2(lastCP, 4);

                Image sprHeart1 =  GameObject.FindWithTag("heart_01").GetComponent<Image>();
                sprHeart1.color = new Color(1, 1, 1,1);
                Image sprHeart2 =  GameObject.FindWithTag("heart_02").GetComponent<Image>();
                sprHeart2.color = new Color(1, 1, 1,1);
                Image sprHeart3 =  GameObject.FindWithTag("heart_03").GetComponent<Image>();
                sprHeart3.color = new Color(1, 1, 1,1);
                Energy = 3;
                Variables.Object(Granny).Set("Energy", Energy);
           }
           else if (isGameOver){
                StartCoroutine(waiter());
                Time.timeScale = 0;
           }
              
        }
        

        if (Granny_Die){
            //Granny.transform.localScale = new Vector3(1,-1,1);
        }else{
            //Animator.SetBool("jump", !Grounded);
        }
        
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.7f)){
            Variables.Object(Granny).Set("Grounded", true);
            Animator.SetBool("jump", false);

        }
        else{
            Variables.Object(Granny).Set("Grounded", false);
        }

        if ( Mathf.Abs(Vertical) < 0.4 ){
            Vertical = 0;
        }

        if ( Mathf.Abs(Horizontal) < 0.4 ){
            Horizontal = 0;
        }

        if (Horizontal == 0 && !Granny_Die && !isGameOver){
            Horizontal = Input.GetAxis("Horizontal");
        }
        
        //Debug.Log("Horizontal: " + Horizontal);
        Debug.Log("Vertical: " + Vertical);
    

        Variables.Object(Granny).Set("Horizontal", Horizontal);
        Animator.SetBool("run", (Horizontal != 0));
        // Debug.Log(Horizontal);
        if (Vertical == 0){
            Vertical = Input.GetAxis("Vertical");
        }

        if (Vertical < 0){
            if (isGrounded){
                Variables.Object(Granny).Set("Crouched", true);
                Horizontal = 0;
            }
        }
        else{
            Variables.Object(Granny).Set("Crouched", false);
        }

        if (Vertical > 0 &&  m_Collider.isTrigger == false && !isFalling){
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.Space) && m_Collider.isTrigger == false && !isFalling){
            Jump();
        }

        isOnPlatform_2 = (bool)Variables.Object(Granny).Get("isOnPlatform_2"); 
        // Debug.Log(isOnPlatform_2);
        // Debug.Log(Crouched);
        // Debug.Log(isGrounded);

        if (isGrounded){
            if (Crouched && isOnPlatform_2){
                timeRemaining -= Time.deltaTime;
                if (timeRemaining <= 0){
                    m_Collider.isTrigger = true;
                    Animator.SetBool("fall", true);
                    isFalling = true;
                    timeRemaining = 2;
                }
            }
        }

    }
    
    void FixedUpdate(){
        Crouched = (bool)Variables.Object(Granny).Get("Crouched");
        Horizontal = (float)Variables.Object(Granny).Get("Horizontal");
        isGrannyDyieng = (bool)Variables.Object(Granny).Get("isGrannyDyieng");

        if (!Crouched && !isGrannyDyieng){
            RigidBody.velocity = new Vector2(Horizontal * 5, RigidBody.velocity.y);
            if (Horizontal < 0){
                Granny.transform.localScale = new Vector3(-1,1,1);
                Direction = -1;
            }
            else if(Horizontal > 0){
                Granny.transform.localScale = new Vector3(1,1,1);
                Direction = 1;
            }
        }
        else{
            RigidBody.velocity = new Vector2(0, RigidBody.velocity.y);
        }

    }

    void OnTriggerExit2D(Collider2D other){
        // Debug.Log("OnTriggerExit2D" + other.gameObject.name);
        if (other.gameObject.name == "Platform_2" && m_Collider.isTrigger == true && !Granny_Die){
            m_Collider.isTrigger = false;
            Variables.Object(Granny).Set("isOnPlatform_2", false);            
        }

        if (other.gameObject.name == "Square" && Granny_Die){
          
        }
    }
    
    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.name == "grandson") {
            Game_Controller Game_Controller = Game.GetComponent<Game_Controller>();
            Game_Controller.GetLevelDone();
        }

        if (other.gameObject.name == "capsule"){
            SetEnergy(1);
            Destroy(other.gameObject);           
        }

        if (other.gameObject.name == "cp"){
            lastCP = other.gameObject.transform.position.x;
            Debug.Log(lastCP);
        }
    }

    void OnCollisionEnter2D(Collision2D other){
        string objectName = "";
        // Debug.Log("OnCollisionEnter2D" + other.gameObject.name);
        objectName = other.gameObject.name;
        if (other.gameObject.name == "Platform_2"){
            Variables.Object(Granny).Set("isOnPlatform_2", true);
            // Debug.Log(Variables.Object(Granny).Get("isOnPlatform_2"));
        }
        else{    
            Variables.Object(Granny).Set("isOnPlatform_2", false);
            Animator.SetBool("fall", false);
            isFalling = false;
        }

        if (objectName == "alligator" || objectName == "Dog" || objectName == "Skater"){
            if (!isFlashing){
                StartCoroutine(Flasher());
                if (Direction > 0){
                    Granny.transform.position = new Vector2(Granny.transform.position.x - 2, Granny.transform.position.y);
                }
                else{
                    Granny.transform.position = new Vector2(Granny.transform.position.x + 2, Granny.transform.position.y);
                }
                SetEnergy(-1);
            }
        }
    }

    public void SetEnergy(int energy){
        // scream
        if (energy < 0){
            Camera.main.GetComponent<AudioSource>().PlayOneShot(Scream_Sound);
        }else{
            Camera.main.GetComponent<AudioSource>().PlayOneShot(Power_Sound);
        }
        //Energy -= 1; 
        Energy += energy;
        if (Energy > 3){
            Energy = 3;
        }
        Variables.Object(Granny).Set("Energy", Energy);
        if (Energy == 3 ){
            Image sprHeart3 =  GameObject.FindWithTag("heart_03").GetComponent<Image>();
            sprHeart3.color = new Color(1, 1, 1, 1);
        }
        
        if (Energy == 2 ){
            Image sprHeart3 =  GameObject.FindWithTag("heart_03").GetComponent<Image>();
            sprHeart3.color = new Color(0, 0, 0,0);
            Image sprHeart2 =  GameObject.FindWithTag("heart_02").GetComponent<Image>();
            sprHeart2.color = new Color(1, 1, 1,1);
        }
        else if (Energy == 1){
            Image sprHeart2 =  GameObject.FindWithTag("heart_02").GetComponent<Image>();
            sprHeart2.color = new Color(0, 0, 0,0);

           // GameObject.FindGameObjectWithTag("FBProfilePic").GetComponent<Image>()
            Image sprHeart1 =  GameObject.FindWithTag("heart_01").GetComponent<Image>();
            sprHeart1.color = new Color(1, 1, 1, 1);
        }
        else if (Energy == 0){
            //SpriteRenderer sprHeart1 =  GameObject.FindWithTag("heart_x").GetComponent<SpriteRenderer>();
            //sprHeart1.color = new Color(0, 0, 0,0);

            Image sprHeart1 =  GameObject.FindWithTag("heart_01").GetComponent<Image>();
            sprHeart1.color = new Color(0, 0, 0, 0);

            // Debug.Log(Energy);
            Game_Controller Game_Controller = Game.GetComponent<Game_Controller>();
            Game_Controller.GetDeath();
            Die();
        } 
        

    }

    public void SetAlive(){
        Granny.transform.localScale = new Vector3(1,1,1);
        Variables.Object(Granny).Set("isGrannyDyieng", false);
        DieTime = 1;
        Granny_Die = false;
        Granny.transform.position = new Vector2(Granny.transform.position.x, 4);
        Image sprHeart1 =  GameObject.FindWithTag("heart_01").GetComponent<Image>();
        sprHeart1.color = new Color(1, 1, 1,1);
        Image sprHeart2 =  GameObject.FindWithTag("heart_02").GetComponent<Image>();
        sprHeart2.color = new Color(1, 1, 1,1);
        Image sprHeart3 =  GameObject.FindWithTag("heart_03").GetComponent<Image>();
        sprHeart3.color = new Color(1, 1, 1,1);
        Energy = 3;
        Variables.Object(Granny).Set("Energy", Energy);
    }

    public void Jump(){
        //isGrounded = (bool)Variables.Object(Granny).Get("Grounded"); 
        Crouched = (bool)Variables.Object(Granny).Get("Crouched");
        isOnPlatform_2 = (bool)Variables.Object(Granny).Get("isOnPlatform_2"); 
                     
        if (isGrounded && !Granny_Die){
            if (Crouched && isOnPlatform_2){
                m_Collider.isTrigger = true;
                Animator.SetBool("fall", true);
                // Debug.Log("Crouched");
            }
            else if (!Crouched){
                AudioData.Play(0);
                Animator.SetBool("jump", true);
                //RigidBody.velocity = Vector2.up * 6;
                // Debug.Log("AddForce");
                // Debug.Log(Vector2.up);
                // Debug.Log("Grounded");
                // Debug.Log(isGrounded);     
            }

        }
    }

    public void JumpEvent(){

        RigidBody.velocity = Vector2.up * 6;
        Debug.Log("Jumping");
    } 
    public void Die(){
        Granny_Die = true;
        Variables.Object(Granny).Set("isGrannyDyieng", true);
        Animator.SetBool("hurt", true);
        Granny.transform.position = new Vector2(Granny.transform.position.x, Granny.transform.position.y + 1);
    }

    public void Splash(){
        Vector2 splash_position = new Vector2(transform.position.x, transform.position.y + 0.5f);
        Camera.main.GetComponent<AudioSource>().PlayOneShot(Splash_Sound);
        Instantiate(SplashPrefab, splash_position, Quaternion.identity);
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(3);
        // Debug.Log("Game Over");
    }

    // Functions to be used as Coroutines MUST return an IEnumerator
    IEnumerator Flasher() 
    {
        SpriteRenderer playerSprite =  GetComponent<SpriteRenderer>();
        Color defaultColor = new Color(1, 1, 1, 1);
        for (int i = 0; i < 25; i++){
            playerSprite.color = new Color(0, 0, 0, 0);
            yield return new WaitForSeconds(0.05f);
            playerSprite.color = defaultColor;
            yield return new WaitForSeconds(0.05f);
            isFlashing = true;

        }
       isFlashing = false;
    }
}
