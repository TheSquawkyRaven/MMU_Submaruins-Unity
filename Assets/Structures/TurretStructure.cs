using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;

public class TurretStructure : MonoBehaviour {

    public Rigidbody RB;
	public Animator Animator;
	public AudioSource ShootAudio;

	public GameObject ShotPrefab;
	public Transform MuzzleTR;

	public float ShootCooldown;
	private float ShootCount;

    public List<Drone> DronesInRange = new();


	private void Update ()
	{
		if (DronesInRange.Count == 0)
		{
			return;
		}

		Aiming();
		ShootCount -= Time.deltaTime;
		if (ShootCount <= 0)
		{
			ShootCount = ShootCooldown;
			Shoot();

        }
	}
		
	private void Shoot()
    {
        Drone target = DronesInRange[0];

        if (Physics.Raycast(MuzzleTR.position, target.transform.position - MuzzleTR.position, out RaycastHit hit, 1000))
        {
			if (hit.collider.TryGetComponent(out Drone drone))
			{
				drone.ShotDestroy();
				DronesInRange.Remove(drone);
            }
        }

        ShootAudio.Play();
        Animator.SetTrigger("Shot");
        GameObject newShotFX = Instantiate(ShotPrefab, MuzzleTR);
        Destroy(newShotFX, 2);

    }

	public void Aiming()
	{
		if (DronesInRange.Count == 0)
		{
			return;
		}

		Drone target = DronesInRange[0];

		transform.LookAt(target.transform.position);
	}

	public void DroneEnter(Drone drone)
    {
        DronesInRange.Add(drone);
    }
    public void DroneExit(Drone drone)
    {
        DronesInRange.Remove(drone);
    }

}