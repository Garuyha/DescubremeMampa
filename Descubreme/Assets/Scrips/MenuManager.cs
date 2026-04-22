using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel, menuOp, lupitaTech;
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

    public void Opcion()
    {
        menuPanel.SetActive(true);
        menuOp.SetActive(true);
        lupitaTech.SetActive(true);
    }

    public void CloseMenu()
    {
        menuPanel.SetActive(false);
        menuOp.SetActive(false);
        lupitaTech.SetActive(false);
    }
}
