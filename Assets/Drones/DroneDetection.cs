using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneDetection : MonoBehaviour
{

    public Drone Drone;

    public void OnTriggerEnter(Collider other)
    {
        Drone.DetectedPlayer(other.transform);
        Debug.Log("Detected Player");
    }

}
