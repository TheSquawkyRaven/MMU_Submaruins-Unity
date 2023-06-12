using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buoyancy : MonoBehaviour
{

    public static float WaterHeight = 482;

    public Rigidbody RB;

    public bool IsUnderwater;

    public bool StopUpwardsFloat;

    public float WaterDrag;
    public float WaterAngularDrag;

    public float AirDrag;
    public float AirAngularDrag;

    public float FloatingStrength;
    public float WavesHeight;
    public float Jankyness;

    private void OnValidate()
    {
        RB = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float yDiff = transform.position.y - WaterHeight + (Random.Range(-WavesHeight, WavesHeight));
        if (yDiff < 0)
        {
            if (!StopUpwardsFloat)
            {
                RB.AddForceAtPosition(Vector3.up * FloatingStrength * Mathf.Abs(yDiff), transform.position + new Vector3(Random.Range(-Jankyness, Jankyness), Random.Range(-Jankyness, Jankyness), Random.Range(-Jankyness, Jankyness)), ForceMode.Force);
            }
            else
            {
                RB.AddForceAtPosition(new Vector3(0, Random.value, 0) * Random.Range(-WavesHeight, WavesHeight), transform.position + new Vector3(Random.Range(-Jankyness, Jankyness), Random.Range(-Jankyness, Jankyness), Random.Range(-Jankyness, Jankyness)), ForceMode.Force);
                RB.useGravity = false;
            }
            if (!IsUnderwater)
            {
                IsUnderwater = true;
                UnderwaterStateChanged();
            }
        }
        else
        {
            RB.useGravity = true;
            IsUnderwater = false;
            UnderwaterStateChanged();
        }

    }

    private void UnderwaterStateChanged()
    {
        if (IsUnderwater)
        {
            RB.drag = WaterDrag;
            RB.angularDrag = WaterAngularDrag;
        }
        else
        {
            RB.drag = AirDrag;
            RB.angularDrag = AirAngularDrag;
        }
    }


}
