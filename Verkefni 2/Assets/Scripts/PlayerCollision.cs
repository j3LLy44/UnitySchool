using UnityEngine;
using UnityEngine.SceneManagement;
//Hjálmar Húnfjörð
public class PlayerCollision : MonoBehaviour
{

    //Component
    [SerializeField] private Material mat;

    //Colors
    public Color32 defaultColor = new Color32(150,150,150,255);
    public Color32 obstacleColor = new Color32(255, 55, 55, 255);

    //Lives
    public float playerLives = 500f;//How many lives the player has
    public float lifeSteal = 10f;   //how many lives are taken when the player collides with obstacle
    public float lifeDrain = 2f;    //how many lives per second the player loses while in contact with obstacle
    public float livesAdded = 25f;  //Lives to add when the player collides with a coin

    //Conditions
    public bool finish = false;

    //Various variables
    private bool collided_Obstacle;

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Obstacle")
        {//Collision with obstacle
            mat.color = obstacleColor;//changes the color of the player
            collided_Obstacle = true;
            
            playerLives -= lifeSteal;//takes an amount of lives from the player.
        }

        if (other.collider.tag == "Coin")
        {//Collision with coin
            Debug.Log("COIN COLLISION");
            Destroy(other.collider.gameObject);

            playerLives += livesAdded;
        }

        if (other.collider.tag == "EndLevel")
        {//collision with the object that signals the end of the level.
            Debug.Log("LEVEL FINISHED");
            finish = true;
            //Loads the next scene/level
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.collider.tag == "Obstacle")
        {
            //changes color back to normal
            mat.color = defaultColor;
            collided_Obstacle = false;
        }
    }

    private void Update()
    {
        if (collided_Obstacle == true) 
        {//if the player stays in contact with the obstacle he will lose lives per second.
            playerLives -= lifeDrain * Time.deltaTime;
        }

        if (playerLives <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
