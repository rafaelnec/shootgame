using UnityEngine;

public class CreditController : MonoBehaviour
{
    public void MainMenuButtonOnClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
