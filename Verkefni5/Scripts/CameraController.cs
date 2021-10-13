using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Public variables
    public GameObject target;
    public float xOffset = 0;
    public float yOffset = 0;

    //position of target
    private Vector3 targetPosition;

    // Update is called once per frame
    void Update()
    {
        //Get the position of target and store in target position
        targetPosition = target.transform.position;
        //Change z to the same as before so camera doesnt freak out
        targetPosition.z = transform.position.z;

        //Set the new position of the camera as the same as targetposition + the offsets
        transform.position = targetPosition + new Vector3(xOffset, yOffset, 0);
    }
}
