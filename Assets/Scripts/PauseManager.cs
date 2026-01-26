using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private AudioSource _menuAudioSource;

    private void Awake()
    {
        // Create or get audio source for menu music
        _menuAudioSource = GetComponent<AudioSource>();
        _menuAudioSource.ignoreListenerPause = true;
    }

    public void MainMenuButtonOnClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }

    public void ContinueButtonOnClick()
    {
        this.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Time.timeScale = 0f;
        AudioListener.pause = true;
        _menuAudioSource.Play();
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
        _menuAudioSource.Stop();
        AudioListener.pause = false;
    }

}
