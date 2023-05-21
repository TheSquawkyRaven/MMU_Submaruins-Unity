using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public MainMenu MainMenu;

    public Rigidbody RB;
    public Buoyancy Buoyancy;

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

    public float UnderwaterMass;
    public float AirMass;

    public Vector3 ApplyingForce;
    public Vector3 ApplyingForceLocal;

    public ParticleSystem SpeedBoostParticles;
    private float speedBoost = 1;
    private float speedBoostTime;
    public float SpeedBoost => speedBoost;

    private void Awake()
    {
        speedBoost = 1;
    }

    private void Update()
    {
        if (!MainMenu.Started)
        {
            return;
        }

        UpdateBoost();
        Update_Movement();
        if (!PlayerInventory.Instance.IsActive)
        {
            Update_Looking();
        }
    }

    public void SetSpeedBoost(float speedBoost, float time)
    {
        this.speedBoost = speedBoost;
        speedBoostTime = time;
        ParticleSystem.EmissionModule emission = SpeedBoostParticles.emission;
        emission.rateOverDistance = speedBoost * 10;
        SpeedBoostParticles.Play();
    }

    private void UpdateBoost()
    {
        if (speedBoostTime < 0)
        {
            return;
        }

        speedBoostTime -= Time.deltaTime;
        if (speedBoostTime <= 0)
        {
            speedBoost = 1;
            SpeedBoostParticles.Stop();
        }
    }

    private void FixedUpdate()
    {
        RB.useGravity = !Buoyancy.IsUnderwater;
        RB.mass = Buoyancy.IsUnderwater ? UnderwaterMass : AirMass;
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
            localForce.z = -Deceleration * speedBoost * localVelocity.z;
        }
        else if ((localVelocity.z > MaxForwardSpeed * speedBoost && input.z > 0) || (localVelocity.z < -MaxBackwardSpeed * speedBoost && input.z < 0))
        {
            //Cannot speed up, if exceed forward/backward speed, based on W/S input direction
            localForce.z = 0;
        }
        else
        {
            localForce.z = input.z * (input.z > 0 ? ForwardAcceleration * speedBoost : BackwardAcceleration * speedBoost);
        }
        if (input.x == 0)
        {
            //No A/D input (left/right)
            localForce.x = -Deceleration * speedBoost * localVelocity.x;
        }
        else if ((localVelocity.x > MaxSidewaysSpeed * speedBoost && input.x > 0) || (localVelocity.x < -MaxSidewaysSpeed * speedBoost && input.x < 0))
        {
            //Cannot speed up, if exceed right/left speed, based on A/D input direction
            localForce.x = 0;
        }
        else
        {
            localForce.x = input.x * SidewaysAcceleration * speedBoost;
        }
        if (input.y == 0)
        {
            //No Space/Ctrl input (up/down)
            if (!Buoyancy.IsUnderwater)
            {
                localForce.y = -Deceleration * speedBoost * localVelocity.y;
            }
        }
        else if ((localVelocity.y > MaxVerticalSpeed * speedBoost && input.y > 0) || (localVelocity.y < -MaxVerticalSpeed * speedBoost && input.y < 0))
        {
            //Cannot speed up, if exceed up/down speed, based on Space/Ctrl input direction
            localForce.y = 0;
        }
        else
        {
            localForce.y = input.y * VerticalAcceleration * speedBoost;
        }

        Vector3 force = transform.TransformDirection(localForce);
        RB.AddForce(force, ForceMode.Impulse);
        ApplyingForce = force;
        ApplyingForceLocal = localForce;

    }

    private void Update_Looking()
    {
        GetMouseInput(out Vector2 input);

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
