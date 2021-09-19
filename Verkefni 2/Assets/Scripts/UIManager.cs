using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Hjálmar Húnfjörð
public class UIManager : MonoBehaviour
{
    
    [SerializeField]
    private Text livesText; //Reference to the text displaying lives
    [SerializeField]
    private Text gameOverText; //Reference to the text displaying the game over screen
    [SerializeField]
    private Text levelCompleteText; //Reference to the text displaying the Level Complete screen

    private float lives;
    private bool levelFinished;
    //Access the playercollision script
    PlayerCollision playerCollision;

    private void Start()
    {
        playerCollision  = GameObject.Find("Player").GetComponent<PlayerCollision>();
        gameOverText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //gets playerlives
        lives = playerCollision.playerLives;

        //gets finish bool from playerCollision script
        levelFinished = playerCollision.finish;

        //Makes sure that negative numbers are not displayed for playerLives
        if (lives <= 0)
        {
            lives = 0;
            gameOverText.gameObject.SetActive(true);
        }

        //Sets the text displaying playerLives 
        livesText.text = "Lives: " + ((int)lives).ToString();
        
        if (levelFinished)
        {
            levelCompleteText.gameObject.SetActive(true);
        }
    }
}