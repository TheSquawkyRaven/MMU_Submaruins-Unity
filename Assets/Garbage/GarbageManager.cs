using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageManager : MonoBehaviour
{
    private static GarbageManager instance;
    public static GarbageManager Instance => instance;

    public GameObject[] GarbagePrefabs;
    public int[] ItemIDs;

    public Renderer Renderer;

    public int amount;

    public int collectedAmount;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Bounds bounds = Renderer.bounds;

        for (int i = 0; i < amount; i++)
        {
            Vector3 start = new(Random.Range(bounds.min.x, bounds.max.x), bounds.min.y, Random.Range(bounds.min.z, bounds.max.z));
            if (Physics.Raycast(start, Vector3.down, out RaycastHit hit, 500, 1 << 10))
            {
                GameObject spawn = GarbagePrefabs[Random.Range(0, GarbagePrefabs.Length)];
                GameObject obj = Instantiate(spawn, hit.point, Quaternion.identity);
                obj.transform.SetParent(transform);
                obj.GetComponent<Rigidbody>().mass = Random.Range(12f, 20f);
            }
            else
            {
                i--;
                continue;
            }

        }

    }

    public void Collected()
    {
        collectedAmount++;
    }

}
