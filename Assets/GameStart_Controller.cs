using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameStart_Controller : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject lblLoading;

    void Start()
    {
       Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown (KeyCode.Mouse0))
        {
            StartLevel1();
        }
        
    }



    public void StartLevel1(){
        //SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

    public void Exit(){
        Application.Quit();
    }

     public void Play(){
        lblLoading.SetActive(true);
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }
}
