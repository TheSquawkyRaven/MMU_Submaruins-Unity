using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class GarbageManager : MonoBehaviour
{
    private static GarbageManager instance;
    public static GarbageManager Instance => instance;

    public GameObject[] Garbage1Prefabs;
    public GameObject[] Garbage2Prefabs;

    public Renderer Renderer;

    public Transform GarbageContainer;

    public int TotalGarbage1Amount;
    public int TotalGarbage2Amount;

    public int TotalAmount => TotalGarbage1Amount + TotalGarbage2Amount;

    public List<GarbageInteractable> MetalGarbages = new();
    public List<GarbageInteractable> PlasticGarbages = new();

    public int CollectedAmount => TotalAmount - (MetalGarbages.Count + PlasticGarbages.Count);

    private void Awake()
    {
        instance = this;
    }


    public List<Save.Obj> GetSave(bool isMetal)
    {
        List<Save.Obj> garbages = new();

        List<GarbageInteractable> target = isMetal ? MetalGarbages : PlasticGarbages;

        for (int i = 0; i < target.Count; i++)
        {
            garbages.Add(new()
            {
                Pos = target[i].transform.position,
                Rot = target[i].transform.rotation,
                Dat = target[i].index,
            });
        }

        return garbages;

    }

    public void StartSpawning()
    {
        SpawnGarbages(TotalGarbage1Amount, Garbage1Prefabs, true);
        SpawnGarbages(TotalGarbage2Amount, Garbage2Prefabs, false);
    }

    public void StartSpawning(List<Save.Obj> MetalGarbages, List<Save.Obj> PlasticGarbages)
    {
        for (int i = 0; i < MetalGarbages.Count; i++)
        {
            SpawnGarbages(Garbage1Prefabs, MetalGarbages[i], true);
        }
        for (int i = 0; i < PlasticGarbages.Count; i++)
        {
            SpawnGarbages(Garbage2Prefabs, PlasticGarbages[i], false);
        }
    }


    private void SpawnGarbages(int amount, GameObject[] prefabs, bool isMetal)
    {
        Bounds bounds = Renderer.bounds;
        for (int i = 0; i < amount; i++)
        {
            Vector3 start = new(Random.Range(bounds.min.x, bounds.max.x), bounds.min.y, Random.Range(bounds.min.z, bounds.max.z));
            if (Physics.Raycast(start, Vector3.down, out RaycastHit hit, 500, 1 << 10))
            {
                int r = Random.Range(0, prefabs.Length);
                GameObject spawn = prefabs[r];
                GameObject obj = Instantiate(spawn, hit.point, Quaternion.identity);
                obj.transform.SetParent(GarbageContainer);
                obj.GetComponent<Rigidbody>().mass = Random.Range(12f, 20f);
                GarbageInteractable g = obj.GetComponent<GarbageInteractable>();
                g.index = r;
                g.IsMetal = isMetal;
                if (isMetal)
                {
                    MetalGarbages.Add(g);
                }
                else
                {
                    PlasticGarbages.Add(g);
                }
            }
            else
            {
                i--;
                continue;
            }

        }
    }
    private void SpawnGarbages(GameObject[] prefabs, Save.Obj saveObj, bool isMetal)
    {
        GameObject spawn = prefabs[saveObj.Dat];
        GameObject obj = Instantiate(spawn, saveObj.Pos.V(), saveObj.Rot.Q());
        obj.transform.SetParent(GarbageContainer);
        obj.GetComponent<Rigidbody>().mass = Random.Range(12f, 20f);
        GarbageInteractable g = obj.GetComponent<GarbageInteractable>();
        g.index = saveObj.Dat;
        g.IsMetal = isMetal;
        if (isMetal)
        {
            MetalGarbages.Add(g);
        }
        else
        {
            PlasticGarbages.Add(g);
        }
    }

    public void Collected(GarbageInteractable garbage, bool isMetal)
    {
        if (isMetal)
        {
            MetalGarbages.Remove(garbage);
        }
        else
        {
            PlasticGarbages.Remove(garbage);
        }
    }

}
