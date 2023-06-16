using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;

public class DetectorStructure : MonoBehaviour
{

    public GameObject GarbageLinePrefab;

    public Dictionary<GarbageInteractable, LineRenderer> GarbageInRange = new();

    private Dictionary<GarbageInteractable, LineRenderer> RemovingGarbage = new();

    private void Update()
    {
        foreach (KeyValuePair<GarbageInteractable, LineRenderer> pair in GarbageInRange)
        {
            if (pair.Key == null)
            {
                RemovingGarbage.Add(pair.Key, pair.Value);
                continue;
            }
            Vector3 from = transform.position;
            Vector3 to = pair.Key.transform.position;
            pair.Value.SetPosition(0, from);
            pair.Value.SetPosition(1, to);
        }

        foreach (KeyValuePair<GarbageInteractable, LineRenderer> pair in RemovingGarbage)
        {
            GarbageInRange.Remove(pair.Key);
            Destroy(pair.Value);
        }
        RemovingGarbage.Clear();
    }

    public void GarbageEnter(GarbageInteractable garbage)
    {
        Vector3 from = transform.position;
        Vector3 to = garbage.transform.position;
        LineRenderer LineObject = Instantiate(GarbageLinePrefab).GetComponent<LineRenderer>();
        LineObject.SetPosition(0, from);
        LineObject.SetPosition(1, to);
        GarbageInRange.Add(garbage, LineObject);
    }
    public void GarbageExit(GarbageInteractable garbage)
    {
        LineRenderer LR = GarbageInRange[garbage];
        Destroy(LR.gameObject);
        GarbageInRange.Remove(garbage);
    }

}