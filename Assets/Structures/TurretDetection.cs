using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretDetection : MonoBehaviour
{

    public TurretStructure TurretStructure;

    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Drone drone))
        {
            TurretStructure.DroneEnter(drone);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Drone drone))
        {
            TurretStructure.DroneExit(drone);
        }
    }

}
