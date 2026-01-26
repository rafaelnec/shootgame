using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void LoadScene(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
        Time.timeScale = 1.0f;
    }

    public void ToggleInstructions(GameObject instructionsPanel)
    {
        if (instructionsPanel.gameObject.activeSelf == false)
        {
            instructionsPanel.SetActive(true);
        }
        else
        {
            instructionsPanel.SetActive(false);
        }
    }

    public void QuitGame()
    {
        // Debug.Log("Quitting game!");
        EditorApplication.ExitPlaymode();
        Application.Quit();
    }
}
