using UnityEngine;

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
    public float livesAdded = 25f;     //Lives to add when the player collides with a coin

    //Various variables
    private bool collided_Obstacle;

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Obstacle")
        {
            mat.color = obstacleColor;
            collided_Obstacle = true;

            playerLives -= 10f;
        }

        if (other.collider.tag == "Coin")
        {
            Debug.Log("COIN COLLISION");
            Destroy(other.collider.gameObject);

            playerLives += livesAdded;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.collider.tag == "Obstacle")
        {
            mat.color = defaultColor;
            collided_Obstacle = false;
        }
    }

    private void Update()
    {
        if (collided_Obstacle == true) 
        {
            playerLives -= lifeDrain * Time.deltaTime;
        }
    }
}
