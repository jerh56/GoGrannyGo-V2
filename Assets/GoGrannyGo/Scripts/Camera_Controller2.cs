using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller2 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Granny;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;
        position.x = Granny.transform.position.x;
        transform.position = position;
    }
}
