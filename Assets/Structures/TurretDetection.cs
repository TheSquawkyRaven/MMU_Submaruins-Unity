using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class TurretDetection : MonoBehaviour
{

    public TurretStructure TurretStructure;

    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Drone drone))
        {
            drone.OnDestroy += OnDetsroyInvoke;
            TurretStructure.DroneEnter(drone);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Drone drone))
        {
            drone.OnDestroy -= OnDetsroyInvoke;
            TurretStructure.DroneExit(drone);
        }
    }

    private void OnDetsroyInvoke(Drone drone)
    {
        TurretStructure.DroneExit(drone);
    }

}
