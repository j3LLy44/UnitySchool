using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;

    public float xOffset;
    public float yOffset = 3.5f;
    public float zOffset = -6f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + new Vector3(xOffset, yOffset, zOffset);
    }
}
