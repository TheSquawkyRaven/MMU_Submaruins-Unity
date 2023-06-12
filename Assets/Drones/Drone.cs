using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{

    public Light ArmLight;

    public Transform PlayerTR;
    public float MoveSpeed;

    public bool playerDetected;

    public bool Arming;
    public float ExplodeCountdown;
    private float explodeCount;

    public float ArmBlink;

    public void DetectedPlayer(Transform PlayerTR)
    {
        playerDetected = true;
        this.PlayerTR = PlayerTR.parent.parent;
    }

    private void Update()
    {
        if (!playerDetected)
        {
            return;
        }
        if (Arming)
        {
            explodeCount -= Time.deltaTime;
            if (explodeCount <= 0)
            {
                Explode();
            }
            ArmLight.enabled = ((int)(explodeCount / ArmBlink)) % 2 == 0;
            return;
        }
        transform.LookAt(PlayerTR.position);
        transform.position = transform.position + (PlayerTR.position - transform.position).normalized * MoveSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!playerDetected || Arming)
        {
            return;
        }
        if (collision.transform == PlayerTR)
        {
            Arm();
        }
    }

    private void Arm()
    {
        Arming = true;
        explodeCount = ExplodeCountdown;
    }

    private void Explode()
    {
        Destroy(gameObject);
    }


}
