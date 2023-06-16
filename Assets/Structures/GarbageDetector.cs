using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageDetector : MonoBehaviour
{

    public DetectorStructure DetectorStructure;

    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out GarbageInteractable garbage))
        {
            DetectorStructure.GarbageEnter(garbage);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out GarbageInteractable garbage))
        {
            DetectorStructure.GarbageExit(garbage);
        }
    }

}
