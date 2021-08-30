using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Hjálmar Húnfjörð
public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset = new Vector3(0, 5, -7);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Change the position of the camera
        transform.position = player.transform.position + offset;
    }
}
