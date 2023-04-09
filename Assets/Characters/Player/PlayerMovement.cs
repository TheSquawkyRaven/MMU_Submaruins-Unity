using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody RB;

    public float MaxForwardSpeed;
    public float MaxBackwardSpeed;
    public float MaxSidewaysSpeed;
    public float MaxVerticalSpeed;

    public float SidewaysAcceleration;
    public float ForwardAcceleration;
    public float BackwardAcceleration;
    public float VerticalAcceleration;
    public float Acceleration;
    public float Deceleration;
    public float HoverThreshold;

    public AnimationCurve LookDampening;

    private bool hover;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Update_Movement();
        Update_Looking();
    }

    private void FixedUpdate()
    {
        if (hover)
        {
            RB.AddForce(-Physics.gravity, ForceMode.Acceleration);
        }
    }


    private void Update_Movement()
    {
        GetMoveInput(out Vector3 input);
        Vector3 velocity = RB.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        Vector3 localForce = new();
        if (input.z == 0)
        {
            //No W/S input (forward/backward)
            localForce.z = -Deceleration * localVelocity.z;
        }
        else if ((localVelocity.z > MaxForwardSpeed && input.z > 0) || (localVelocity.z < -MaxBackwardSpeed && input.z < 0))
        {
            //Cannot speed up, if exceed forward/backward speed, based on W/S input direction
            localForce.z = 0;
        }
        else
        {
            localForce.z = input.z * (input.z > 0 ? ForwardAcceleration : BackwardAcceleration);
        }
        if (input.x == 0)
        {
            //No A/D input (left/right)
            localForce.x = -Deceleration * localVelocity.x;
        }
        else if ((localVelocity.x > MaxSidewaysSpeed && input.x > 0) || (localVelocity.x < -MaxSidewaysSpeed && input.x < 0))
        {
            //Cannot speed up, if exceed right/left speed, based on A/D input direction
            localForce.x = 0;
        }
        else
        {
            localForce.x = input.x * SidewaysAcceleration;
        }
        hover = false;
        if (input.y == 0)
        {
            //No Space/Ctrl input (up/down)
            localForce.y = -Deceleration * localVelocity.y;
            if (localVelocity.y < 0 && Mathf.Abs(localVelocity.y) < (HoverThreshold / Deceleration))
            {
                hover = true;
            }
        }
        else if ((localVelocity.y > MaxVerticalSpeed && input.y > 0) || (localVelocity.y < -MaxVerticalSpeed && input.y < 0))
        {
            //Cannot speed up, if exceed up/down speed, based on Space/Ctrl input direction
            localForce.y = 0;
        }
        else
        {
            localForce.y = input.y * VerticalAcceleration;
        }

        Vector3 force = transform.TransformDirection(localForce);
        RB.AddForce(force, ForceMode.Impulse);

    }

    private void Update_Looking()
    {
        GetMouseInput(out Vector2 input);

        Debug.Log(input);

        Vector3 rotation = transform.rotation.eulerAngles;
        float x = input.x;
        float xAbs = Mathf.Abs(x);
        float xDamp = LookDampening.Evaluate(xAbs);
        x *= xDamp;
        float y = input.y;
        float yAbs = Mathf.Abs(y);
        float yDamp = LookDampening.Evaluate(yAbs);
        y *= yDamp;
        rotation += new Vector3(-y, x);
        transform.rotation = Quaternion.Euler(rotation);

    }


    private bool GetMoveInput(out Vector3 input)
    {
        input = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            input.z += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            input.z -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            input.x += 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            input.x -= 1;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            input.y += 1;
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            input.y -= 1;
        }
        if (input != Vector3.zero)
        {
            input.Normalize();
            return true;
        }
        return false;
    }
    private bool GetMouseInput(out Vector2 input)
    {
        input = new(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        return input != Vector2.zero;
    }

}
