using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{

    public PlayerMovement Movement;

    public Animator TurbineAnimatorL;
    public Animator TurbineAnimatorR;

    private float turbineSpeed;

    private void Awake()
    {
        TurbineAnimatorL.SetFloat("Speed", turbineSpeed);
        TurbineAnimatorR.SetFloat("Speed", turbineSpeed);
    }

    private void Update()
    {
        Vector3 force = Movement.ApplyingForceLocal;

        float referenceSpeed = Mathf.Clamp(force.z, -1f, 1f);
        turbineSpeed = Mathf.Lerp(turbineSpeed, referenceSpeed, 0.01f);

        TurbineAnimatorL.SetFloat("Speed", turbineSpeed);
        TurbineAnimatorR.SetFloat("Speed", turbineSpeed);
    }

}
