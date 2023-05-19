using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{

    public PlayerMovement Movement;

    public Animator ShipAnimator;

    private float turbineSpeed;

    private void Awake()
    {
        ShipAnimator.SetFloat("Speed", turbineSpeed);
        ShipAnimator.SetFloat("Speed", turbineSpeed);
    }

    private void Update()
    {
        Vector3 force = Movement.ApplyingForceLocal;
        float f = force.magnitude;

        float referenceSpeed = Mathf.Clamp(f, -1f, 1f);
        turbineSpeed = Mathf.Lerp(turbineSpeed, referenceSpeed, 0.01f);

        ShipAnimator.SetFloat("Speed", turbineSpeed);
        ShipAnimator.SetFloat("Speed", turbineSpeed);
    }

}
