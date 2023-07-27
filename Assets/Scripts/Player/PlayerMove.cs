using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    private FloatingJoystick joystick;
    private bool isMoving;

    public event Action<bool> IsMoved;

    public void Init(FloatingJoystick joystick)
    {
        isMoving = false;
        this.joystick = joystick;
        enabled = true;
    }

    private void FixedUpdate()
    {
        Vector3 direction = Vector3.forward * joystick.Vertical + Vector3.right * joystick.Horizontal;
        rb.velocity = new Vector3(joystick.Horizontal * speed, rb.velocity.y , joystick.Vertical * speed);

        if (isMoving)
        {
            if (rb.velocity == Vector3.zero)
            {
                isMoving = false;
                IsMoved?.Invoke(isMoving);
            }
        }
        else
        {
            if (rb.velocity != Vector3.zero)
            {
                isMoving = true;
                IsMoved?.Invoke(isMoving);
            }
        }
    }
}
