using UnityEngine;
using System.Collections;
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
        StartCoroutine(FeedbackAndNext());
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
    IEnumerator FeedbackAndNext()
    {
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene("Jugar - Scene");

    }
}
