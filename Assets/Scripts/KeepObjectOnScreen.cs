using UnityEngine;

public class KeepObjectOnScreen : MonoBehaviour
{
    private Vector3 screenBounds;
    private float objectWidth;
    private float objectHeight;

    void Start()
    {
        // Calculate screen bounds in world space
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        // Get half the width and height of the object (assuming a SpriteRenderer or similar)
        // This is crucial to keep the *entire* object on screen, not just its center point
        if (TryGetComponent<SpriteRenderer>(out SpriteRenderer renderer))
        {
            objectWidth = renderer.bounds.size.x / 2;
            objectHeight = renderer.bounds.size.y / 2;
        }
    }

    void LateUpdate()
    {
        // Use LateUpdate to ensure the position is clamped after all other movements (e.g., player movement in Update/FixedUpdate)
        Vector3 viewPos = transform.position;

        // Clamp the X position
        // The minimum is the left edge plus half the object's width
        // The maximum is the right edge minus half the object's width
        viewPos.x = Mathf.Clamp(viewPos.x, -screenBounds.x + objectWidth, screenBounds.x - objectWidth);

        // Clamp the Y position
        // The minimum is the bottom edge plus half the object's height
        // The maximum is the top edge minus half the object's height
        viewPos.y = Mathf.Clamp(viewPos.y, -screenBounds.y + objectHeight, screenBounds.y - objectHeight - 1);

        // Apply the clamped position back to the object's transform
        transform.position = viewPos;
    }
}