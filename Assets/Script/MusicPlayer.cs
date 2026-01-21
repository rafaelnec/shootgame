using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private void Awake()
    {
        // Only one copy of this music in the game
        if (FindObjectsOfType<MusicPlayer>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
}
