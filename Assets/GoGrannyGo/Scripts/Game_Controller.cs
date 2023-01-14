using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class Game_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject Coin;
    public GameObject Granny;
    public GameObject Panel;
    public TMPro.TMP_Text lblCoins;
    public static int totalCoins;
    public static int totalLives;
    public AudioClip Coin_Sound;
    public AudioClip Death_Sound;
    public AudioClip Done_Sound;
    private float timeRemaining;
    private int timeRemain;
    private int score;
    private TMP_Text lblTime;
    private TMP_Text lblScore;
    //private GameObject Panel;
    public GameObject lblGameOver;
    public GameObject btnPlay;
    public GameObject btnExit;
    public GameObject btnGamePaused;
    public GameObject pnlPausePlay;

    private AudioSource AudioData;
    public GameObject lblLevelDone;
    private bool isGrannyDyieng;
    
    void Start()
    {
        Time.timeScale = 1f;
        totalCoins = 0;
        totalLives = 99;
        timeRemaining = 180;
        AudioData = GetComponent<AudioSource>();
        //Panel = GameObject.FindWithTag("panelPortrait");
        TMP_Text Lives = GameObject.FindWithTag("lblLives").GetComponent<TMP_Text>();
        Lives.text = totalLives.ToString();
    }

    // Update is called once per frame
    void Update()
    {

        isGrannyDyieng =  (bool)Variables.Object(Granny).Get("isGrannyDyieng");

        if (totalLives < 1){
            //lblGameOver = GameObject.FindWithTag("lblGameOver").GetComponent<TMP_Text>();
            lblGameOver.SetActive(true);
            btnPlay.SetActive(true);
            btnExit.SetActive(true);
            btnGamePaused.SetActive(false);
            pnlPausePlay.SetActive(false);
            AudioData.Stop();
            //Time.timeScale = 0; 	//que la velocidad del juego sea 0}
            Variables.Object(Granny).Set("isGameOver", true);
        } 

        timeRemaining -= Time.deltaTime;
        timeRemain = Mathf.RoundToInt(timeRemaining);
        lblTime = GameObject.FindWithTag("lblTime").GetComponent<TMP_Text>();
        lblTime.text = timeRemain.ToString();

        if (timeRemain < 1){
            timeRemaining = 180;
            GetDeath();
            Granny_Controller_2 Granny_Controller = Granny.GetComponent<Granny_Controller_2>();
            Granny_Controller.Die();
        }

        if (Granny.transform.position.y < -4f && !isGrannyDyieng ){
            GetDeath();
            Granny_Controller_2 Granny_Controller = Granny.GetComponent<Granny_Controller_2>();
            Granny_Controller.Splash();
            Granny_Controller.Die();

        }


        Debug.Log(Screen.orientation);
        if(Screen.orientation == ScreenOrientation.Portrait && IsMobileBrowser())
        {
            Panel.SetActive(true);      
        }
        if(Screen.orientation == ScreenOrientation.PortraitUpsideDown && IsMobileBrowser())
        {
            Panel.SetActive(true);
        }  
        if(Screen.orientation == ScreenOrientation.LandscapeLeft)
        {
            Panel.SetActive(false);
        }  
        if(Screen.orientation == ScreenOrientation.LandscapeRight)
        {
            Panel.SetActive(false);
        }
    }

    public void GetCoin(){
        TMP_Text Coin = GameObject.FindWithTag("lblCoins").GetComponent<TMP_Text>();
        //Text txtPhraseHide = GameObject.FindWithTag("PhraseTextHide").GetComponent<Text>();
        totalCoins += 1;
        Coin.text = totalCoins.ToString();
        Camera.main.GetComponent<AudioSource>().PlayOneShot(Coin_Sound);

    }

    public void GetDeath(){
        TMP_Text Lives = GameObject.FindWithTag("lblLives").GetComponent<TMP_Text>();
        totalLives -= 1;
        Lives.text = totalLives.ToString();
        Camera.main.GetComponent<AudioSource>().PlayOneShot(Death_Sound);
    }

    public void GetLevelDone(){
        //TMP_Text Lives = GameObject.FindWithTag("lblLives").GetComponent<TMP_Text>();
        //totalLives -= 1;
        //Lives.text = totalLives.ToString();
        lblTime = GameObject.FindWithTag("lblTime").GetComponent<TMP_Text>();
        lblCoins = GameObject.FindWithTag("lblCoins").GetComponent<TMP_Text>();
        lblScore = GameObject.FindWithTag("lblScore").GetComponent<TMP_Text>();
        score = int.Parse(lblCoins.text)  * int.Parse(lblTime.text);
        lblScore.text = score.ToString();
        lblLevelDone.SetActive(true);
        btnPlay.SetActive(true);
        btnExit.SetActive(true);
        btnGamePaused.SetActive(false);
        pnlPausePlay.SetActive(false);
        AudioData.Stop();
        Time.timeScale = 0; 	//que la velocidad del juego sea 0
        Camera.main.GetComponent<AudioSource>().PlayOneShot(Done_Sound);
    }

    public static bool IsMobileBrowser()
    {
        //return WebGLHandler.IsMobileBrowser(); // value based on the current browser

       /* #if UNITY_EDITOR
                return false; // value to return in Play Mode (in the editor)
        #elif UNITY_WEBGL
            return WebGLHandler.IsMobileBrowser(); // value based on the current browser
        #else
            return false; // value for builds other than WebGL
        #endif*/
        //return false;

        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            return false;      
        }
        else{
            return true;
        }
    }
 
}
