using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class DroneManager : MonoBehaviour
{
    private static DroneManager instance;
    public static DroneManager Instance => instance;

    public GameObject DronePrefab;
    public Transform PlayerTR;

    public Renderer Renderer;

    public Transform DroneContainer;

    public float MaxHeight;

    public int TotalAmount;
    public int DestroyedAmount => TotalAmount - Drones.Count;

    public List<Drone> Drones = new();

    private void Awake()
    {
        instance = this;
    }

    public List<Save.Obj> GetSave()
    {
        List<Save.Obj> drones = new();

        for (int i = 0; i < Drones.Count; i++)
        {
            drones.Add(new()
            {
                Pos = Drones[i].transform.position,
                Rot = Drones[i].transform.rotation,
            });
        }

        return drones;

    }



    public void StartSpawning()
    {
        Bounds bounds = Renderer.bounds;

        for (int i = 0; i < TotalAmount; i++)
        {
            Vector3 start = new(Random.Range(bounds.min.x, bounds.max.x), bounds.min.y, Random.Range(bounds.min.z, bounds.max.z));
            if (Physics.Raycast(start, Vector3.down, out RaycastHit hit, 1 << 10))
            {
                GameObject spawn = DronePrefab;
                GameObject obj = Instantiate(spawn, hit.point, Quaternion.identity);
                obj.transform.SetParent(DroneContainer);
                Drone d = obj.GetComponent<Drone>();
                d.PlayerTR = PlayerTR;
                Drones.Add(d);
            }
            else
            {
                i--;
                continue;
            }

        }

    }

    public void StartSpawning(List<Save.Obj> drones)
    {
        for (int i = 0; i < drones.Count; i++)
        {
            SpawnDrones(drones[i]);
        }
    }

    private void SpawnDrones(Save.Obj saveObj)
    {
        GameObject spawn = DronePrefab;
        GameObject obj = Instantiate(spawn, saveObj.Pos.V(), saveObj.Rot.Q());
        obj.transform.SetParent(DroneContainer);
        Drone d = obj.GetComponent<Drone>();
        d.PlayerTR = PlayerTR;
        Drones.Add(d);
    }

    public void DroneDestroyed(Drone drone)
    {
        Drones.Remove(drone);
        Score.Instance.UpdateScore();
    }

}
