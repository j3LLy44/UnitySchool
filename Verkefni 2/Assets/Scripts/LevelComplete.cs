using UnityEngine;
using UnityEngine.SceneManagement;
//Hj�lmar H�nfj�r�
public class LevelComplete : MonoBehaviour
{
    
    public void LoadNextLevel()
    {
        //Loads the next scene/level (stole it from brackeys and spent 3 hours trying to make it work)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
