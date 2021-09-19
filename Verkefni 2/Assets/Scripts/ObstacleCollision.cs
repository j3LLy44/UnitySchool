using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Hjálmar Húnfjörð
public class ObstacleCollision : MonoBehaviour
{
    public Color32 defaultColor = new Color32(255, 0, 0, 0);
    public Color32 onCollColor = new Color32(200, 0, 0, 0);

    private bool isCollided;

    private Material mat;

    private void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Player")
        {
            isCollided = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.collider.tag == "Player")
        {
            isCollided = false;
        }
    }

    private void Update()
    {
        if (isCollided)
        {
            mat.color = onCollColor;
        }

        if (isCollided == false)
        {
            mat.color = defaultColor;
        }
    }
}