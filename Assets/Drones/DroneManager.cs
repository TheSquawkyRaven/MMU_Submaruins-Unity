using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneManager : MonoBehaviour
{
    private static DroneManager instance;
    public static DroneManager Instance => instance;

    public GameObject DronePrefab;
    public Transform PlayerTR;

    public Renderer Renderer;

    public float MaxHeight;

    public int amount;

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
            if (Physics.Raycast(start, Vector3.down, out RaycastHit hit, 1 << 10))
            {
                GameObject spawn = DronePrefab;
                Vector3 spawnPos = hit.point;
                spawnPos.y = Random.Range(spawnPos.y, MaxHeight);
                GameObject obj = Instantiate(spawn, hit.point, Quaternion.identity);
                obj.transform.SetParent(transform);
                obj.GetComponent<Drone>().PlayerTR = PlayerTR;
            }
            else
            {
                i--;
                continue;
            }

        }

    }

}
