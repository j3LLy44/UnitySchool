using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Kassi : MonoBehaviour
{
    Text countText;
    private static int count;
    public GameObject sprenging;
    void Start()
    {
        countText = FindObjectOfType<Text>();
       //sprenging= GetComponent<Explosion>
        count = 0;
        SetCountText();
    }
    private void Update()
    {
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "kula2")
        {
            Debug.Log("varð fyrir kúlu2");
            count = count + 1;//færð stig
            SetCountText();//kallar í aðferðina
            Sprengin();
        }
    }
    void SetCountText()//hér er aðferðin
    {
        countText.text = "Stig: " + count.ToString();
    }
    void Sprengin()
    {
        Instantiate(sprenging, transform.position, transform.rotation);
    }
}
