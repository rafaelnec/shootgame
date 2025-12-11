using System.Collections;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Player player;
    private float horizontal;
    private float vertical;
    private Vector2 lookTarget;

    void Start()
    {
        player = GetComponent<Player>();
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        lookTarget = Input.mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Shoot!");
            player.Shoot();
        }
    }

    void FixedUpdate()
    {
        player.Move(new Vector2(horizontal, vertical), lookTarget);
    }
}
