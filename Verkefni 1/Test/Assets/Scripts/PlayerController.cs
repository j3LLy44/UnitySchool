using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Hjálmar Húnfjörð
public class PlayerController : MonoBehaviour
{

    //Variables
    private float speed = 10f;
    private float turnSpeed = 25f;
    private float horizontalInput;
    private float forwardInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //Get inputs from unity
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        //Vehicle moves
        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);

        //Vehicle turns
        transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);
    }
}
