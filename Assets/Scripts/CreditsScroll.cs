using UnityEngine;

public class CreditsScroll : MonoBehaviour
{
    public float scrollSpeed = 30f;
    public float stopY = 2400f;

    void Update()
    {
        if (transform.position.y < stopY)
        {
            transform.Translate(Vector3.up * scrollSpeed * Time.deltaTime, Space.Self);
        }
    }
}
