using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;  

public class Button_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Granny;
    public GameObject lblGamePaused;
    public GameObject btnGameExit;
    public GameObject pnlExitWhenPause;

    public Button btnGamePaused;
    public TextMeshProUGUI btnText;

    private bool isGrounded;
    private bool isPaused; 


    void Start()
    {
        //Time.timeScale = 1f;
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = (bool)Variables.Object(Granny).Get("Grounded"); 

    }
    public void Jump(){
        Granny_Controller_2 Granny_Controller = Granny.GetComponent<Granny_Controller_2>();
        Granny_Controller.Jump();
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void Reestart(){
        SceneManager.LoadScene("Level-1", LoadSceneMode.Single);
    }

    public void Next(){
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Level-1"){
            SceneManager.LoadScene("Level-2", LoadSceneMode.Single);
        }
        else if (scene.name == "Level-2"){
            SceneManager.LoadScene("Level-3", LoadSceneMode.Single);
        }
    }

    public void ExitGame(){
        SceneManager.LoadScene("StartScene", LoadSceneMode.Single);
    }

    public void PauseResume(){
        btnText = btnGamePaused.GetComponentInChildren<TextMeshProUGUI>();

        if (isPaused){
            isPaused = false;
            btnText.text = "II";
            btnGameExit.SetActive(false);            
            lblGamePaused.SetActive(false);
            pnlExitWhenPause.SetActive(false);
            Time.timeScale = 1;
        }else{
            isPaused = true;
            btnText.text = "=>";
            btnGameExit.SetActive(true);            
            lblGamePaused.SetActive(true);
            pnlExitWhenPause.SetActive(true);
            Time.timeScale = 0;
        }
        
    }

    public void Zoom(){
        Camera.main.fieldOfView = 10;
        Debug.Log("Zoom");
    }
}
