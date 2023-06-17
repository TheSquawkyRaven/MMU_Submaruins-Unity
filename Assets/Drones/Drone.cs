using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Drone : MonoBehaviour
{

    public Light ArmLight;

    public Transform PlayerTR;
    public Collider Collider;
    public GameObject Explosion;

    public AudioSource BeepAudio;

    public float MoveSpeed;

    public bool playerDetected;

    public bool Arming;
    public float ExplodeCountdown;
    private float explodeCount;

    public float ArmBlink;

    public float ExplodeDistance;
    public int Damage;

    public event Action<Drone> OnDestroy = _ => { };

    public void DetectedPlayer(Transform PlayerTR)
    {
        playerDetected = true;
        this.PlayerTR = PlayerTR;
        BeepAudio.Play();
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
                Explode(true);
            }
            ArmLight.enabled = ((int)(explodeCount / ArmBlink)) % 2 == 0;
            float pitch = 1 + explodeCount / ArmBlink;

            BeepAudio.pitch = pitch;

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
        BeepAudio.Play();
        BeepAudio.loop = true;
    }

    private void Explode(bool damagePlayer)
    {
        Collider.enabled = false;
        OnDestroy.Invoke(this);
        DroneManager.Instance.DroneDestroyed(this);
        Instantiate(Explosion, transform.position, transform.rotation);
        if (damagePlayer)
        {
            float dist = Vector3.Distance(Player.Instance.transform.position, transform.position);
            if (dist < ExplodeDistance)
            {
                Player.Instance.DecreaseHealth(Damage);
            }
        }
        Destroy(gameObject);
    }

    public void ShotDestroy()
    {
        Explode(false);
    }


}
