using System.Collections;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Player player;
    private float horizontal;
    private float vertical;
    private Vector2 lookTarget;

    private float holdThreshold = 0.5f; // Time in seconds to consider it a "hold"
    private float mouseDownTime;
    private bool isHolding = false;

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
            mouseDownTime = Time.time;
            isHolding = false;
        }

        if (Input.GetMouseButton(0))
        {
            if (Time.time - mouseDownTime >= holdThreshold)
            {
                // Action for *starting* a hold
                Debug.Log("Hold started!");
                HandleHoldClick();
                isHolding = true;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (!isHolding)
            {
                // Action for a *single, quick* click
                Debug.Log("Single click detected!");
                HandleSingleClick();
            }
            else
            {
                // Action for *releasing* a hold
                Debug.Log("Hold released.");
                HandleHoldRelease();
            }
            isHolding = false;
        }

        if (Input.GetMouseButtonDown(1))
        {
            player.ShootNuke();
        }
    }

    void HandleSingleClick()
    {
        player.Shoot();
    }

    void HandleHoldClick()
    {
        player.ShootPowerGunUp();
    }
    
    void HandleHoldRelease()
    {
        player.ShootPowerGunUpReset();
    }

    void FixedUpdate()
    {
        player.Move(new Vector2(horizontal, vertical), lookTarget);
    }
}
