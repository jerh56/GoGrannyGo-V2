using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Game;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.name == "Granny"){
            Game_Controller Game_Controller = Game.GetComponent<Game_Controller>();
            Game_Controller.GetCoin();
            Destroy(gameObject);           
        }
    }

}
