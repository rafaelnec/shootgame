using UnityEngine;

public class CreditFloat : MonoBehaviour
{
    public float amplitude = 10f;   // how far it moves
    public float speed = 1f;        // how fast

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        float y = Mathf.Sin(Time.time * speed) * amplitude;
        transform.localPosition = startPos + new Vector3(0, y, 0);
    }
}
