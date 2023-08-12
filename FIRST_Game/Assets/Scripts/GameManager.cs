
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    bool hasEnded = false;
    public GameObject completeLevelUI;
    int i = 0;

    bool gameEnded = false;
    public void CompleteLevel()
    {
        FindObjectOfType<PlayerMovement>().Stop();
        completeLevelUI.SetActive(true);

        Invoke("NextLevel", 3f);


    }

    public void EndGame()
    {
        hasEnded = true;
        if (hasEnded)
        {

            Invoke("Restart", 1f);
        }
        else
        {
            hasEnded = true;
        }
         
    }



    public void OyunBitti()
    {
        hasEnded = true;
        if (hasEnded)
        {
            gameEnded = true;

        }
        else
        {
            hasEnded = true;
        }

    }


    void Restart()
    {
        if (!gameEnded)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);   // Aktif olan sahneyi restartlar

        }
        

    }
    void NextLevel() 

    {
        if (SceneManager.GetActiveScene().name == "Level 4")
        {
            Debug.Log("OYUN BETTEEEEEEEEEEee");
            OyunBitti();

        }
        else if (SceneManager.GetActiveScene().name == "Level 3")
        {
            Debug.Log("Level BETTEEEEEEEEEEee3");
            SceneManager.LoadScene("Level 4");

        }
        else if (SceneManager.GetActiveScene().name == "Level 2")
        {
            Debug.Log("Level BETTEEEEEEEEEEee2");
            SceneManager.LoadScene("Level 3");


        }
        else if (SceneManager.GetActiveScene().name == "Level 1")
        {
            Debug.Log("Level BETTEEEEEEEEEEee1");
            SceneManager.LoadScene("Level 2");


        }

    }

    public bool GethasEnded()
    {
        return hasEnded;
    }
}
