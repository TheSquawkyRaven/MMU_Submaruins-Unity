using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buoyancy : MonoBehaviour
{

    public static float WaterHeight = 76;

    public Rigidbody RB;

    public bool IsUnderwater;

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
            RB.AddForceAtPosition(Vector3.up * FloatingStrength * Mathf.Abs(yDiff), transform.position + new Vector3(Random.Range(-Jankyness, Jankyness), Random.Range(-Jankyness, Jankyness), Random.Range(-Jankyness, Jankyness)), ForceMode.Force);
            if (!IsUnderwater)
            {
                IsUnderwater = true;
                UnderwaterStateChanged();
            }
        }
        else
        {
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
