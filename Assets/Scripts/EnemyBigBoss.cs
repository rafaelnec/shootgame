using UnityEngine;
using System.Collections;

public class EnemyBigBoss : MonoBehaviour
{
    public float fadeDuration = 0.1f;
    private Renderer _renderer;
    private bool isDead = false;

    void Start()
    {
        _renderer = GetComponent<Renderer>();
        StartCoroutine(Fade(0f, 1f)); // Fade In
        StartCoroutine(LifeTimer()); // Start 10s timer
    }

    IEnumerator LifeTimer()
    {
        yield return new WaitForSeconds(10f);
        if (!isDead) StartCoroutine(DieAndDestroy());
    }

    public void Kill()
    {
        if (isDead) return;
        StopCoroutine(LifeTimer());
        StartCoroutine(DieAndDestroy());
    }

    IEnumerator DieAndDestroy()
    {
        isDead = true;
        yield return StartCoroutine(Fade(1f, 0f)); // Fade Out
        Destroy(gameObject);
    }

    IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsed = 0f;
        Color color = _renderer.material.color;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, endAlpha, elapsed / fadeDuration);
            _renderer.material.color = color;
            yield return null;
        }
    }

    void OnDestroy()
    {
        // Cleanup if necessary
    }
}