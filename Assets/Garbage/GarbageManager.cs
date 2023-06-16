using System.Collections;
using System.Collections.Generic;
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

    public int garbage1Amount;
    public int garbage2Amount;

    public int TotalAmount => garbage1Amount + garbage2Amount;

    public int collectedAmount;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SpawnGarbages(garbage1Amount, Garbage1Prefabs);
        SpawnGarbages(garbage2Amount, Garbage2Prefabs);

    }

    private void SpawnGarbages(int amount, GameObject[] prefabs)
    {
        Bounds bounds = Renderer.bounds;
        for (int i = 0; i < amount; i++)
        {
            Vector3 start = new(Random.Range(bounds.min.x, bounds.max.x), bounds.min.y, Random.Range(bounds.min.z, bounds.max.z));
            if (Physics.Raycast(start, Vector3.down, out RaycastHit hit, 500, 1 << 10))
            {
                GameObject spawn = prefabs[Random.Range(0, prefabs.Length)];
                GameObject obj = Instantiate(spawn, hit.point, Quaternion.identity);
                obj.transform.SetParent(GarbageContainer);
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
        Score.Instance.UpdateScore();
    }

}
