using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private void Awake()
    {
        // Only one copy of this music in the game
        if (Object.FindAnyObjectByType<MusicPlayer>() != null && Object.FindAnyObjectByType<MusicPlayer>() != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
}
