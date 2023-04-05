using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Rigidbody RB;
    public Camera Camera;

    public float MaxHorizontalSpeed;
    public float MaxVerticalSpeed;

    private void Update()
    {
        bool isMoving = GetMoveInput(out Vector3 move);
        Vector3 velocity = RB.velocity;
        if (isMoving)
        {
            Vector3 movement = Camera.transform.right * move.x + Camera.transform.forward * move.z + Camera.transform.up * move.y;
            Vector2 horizontalVelocity = new(velocity.x, velocity.z);
            if (velocity.x > MaxHorizontalSpeed && velocity.x > 0)
            {
                movement.x = 0;
            }
            else if (velocity.x < MaxHorizontalSpeed && velocity.x < 0)
            {
                movement.x = 0;
            }
            if (movement.z > MaxHorizontalSpeed && velocity.z > 0)
            {
                movement.z = 0;
            }
            else if (movement.z < MaxHorizontalSpeed && velocity.z < 0)
            {
                movement.z = 0;
            }
            if (velocity.y > MaxVerticalSpeed && movement.y > 0)
            {
                movement.y = 0;
            }
            else if (velocity.y < MaxVerticalSpeed && movement.y < 0)
            {
                movement.y = 0;
            }
            RB.AddForce(movement, ForceMode.Impulse);
            Debug.Log($"Moving {movement}");
        }
        else
        {
            RB.AddForce(-RB.velocity);
        }
    }

    private bool GetMoveInput(out Vector3 move)
    {
        move = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            move.z += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            move.z -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            move.x += 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            move.x -= 1;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            move.y += 1;
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            move.y -= 1;
        }
        if (move != Vector3.zero)
        {
            move.Normalize();
            return true;
        }
        return false;
    }

}
