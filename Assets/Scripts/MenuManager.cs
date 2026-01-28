using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject helpMe;

    public void StartGame()
    {
        SceneManager.LoadScene("AlphabetGame");
    }

    public void CreditGame()
    {
        SceneManager.LoadScene("Credits");
    }

    public void HelpGame()
    {
        helpMe.SetActive(true);
    }

    public void HelpExitGame()
    {
        helpMe.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
