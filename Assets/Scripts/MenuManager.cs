using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("AlphabetGame");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
