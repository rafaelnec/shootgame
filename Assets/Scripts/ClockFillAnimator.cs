using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ClockFillAnimator : MonoBehaviour
{
    public Image fillImage;
    public float duration = 10f;

    void Start()
    {
        if (fillImage == null)
        {
            fillImage = GetComponent<Image>();
        }

    }

    public void PlayAnimateClockFill()
    {   
        fillImage.fillAmount = 1f;
        StartCoroutine(AnimateClockFill());
    }

    private IEnumerator AnimateClockFill()
    {
        while (true)
        {
            float timer = 0f;

            while (timer < duration)
            {
                timer += Time.deltaTime;
                fillImage.fillAmount = Mathf.Lerp(1, 0, timer / duration);
                yield return null;
            }
           
        }
    }
}
