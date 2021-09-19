using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Hjálmar Húnfjörð
public class DestroyAfterTime : MonoBehaviour
{
    public GameObject otherObject;
    public float timeToDestroy;
    public bool destroySelf = true;


    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.name == otherObject.name && destroySelf == false)
        {
            Destroy(otherObject, timeToDestroy);
        }

        if (other.collider.name == otherObject.name && destroySelf == true)
        {
            Destroy(gameObject, timeToDestroy);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
