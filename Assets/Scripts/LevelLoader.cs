using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public void StartGame()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        Time.timeScale = 1.0f;
        Application.Quit();
    }

    public void StartLevel1()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(2);
    }
    public void StartLevel2()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(3);
    }
}
