using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu principal - Scene");
    }

    public void Play()
    {
        SceneManager.LoadScene("Jugar - Scene");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
