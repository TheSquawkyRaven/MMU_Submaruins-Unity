using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private static Player instance;
    public static Player Instance => instance;

    public int MaxHealth;
    public int Health;

    public Slider HealthBar;


    public Transform RaycastOrigin;
    public Transform RaycastDirection;
    public float Range;

    public Slot SelectedSlot;
    public Slot Slot1;
    public Slot Slot2;
    private int slotNumber;

    public GameObject Slot1Prefab;
    public GameObject Slot2Prefab;

    public GameObject SelectedRaycastPos;

    public List<GameObject> DetectorStructures = new();
    public List<GameObject> TurretStructures = new();

    public List<Save.Obj> GetSave(bool isDetector)
    {
        List<Save.Obj> save = new();
        List<GameObject> target = isDetector ? DetectorStructures : TurretStructures;

        for (int i = 0; i < target.Count; i++)
        {
            save.Add(new Save.Obj()
            {
                Pos = target[i].transform.position,
                Rot = target[i].transform.rotation,
            });
        }
        return save;
    }


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        HealthBar.maxValue = MaxHealth;
        UpdateBar();
    }

    private void Update()
    {
        KeyUpdate();
        SelectedSlotUpdate();
    }

    public void KeyUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (Slot1.ItemData != null && Slot1.ItemData.amount > 0)
            {
                SelectedSlot = Slot1;
                slotNumber = 1;
            }
            else
            {
                SelectedSlot = null;
                SelectedRaycastPos.SetActive(false);
                slotNumber = 0;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (Slot2.ItemData != null && Slot2.ItemData.amount > 0)
            {
                SelectedSlot = Slot2;
                slotNumber = 2;
            }
            else
            {
                SelectedSlot = null;
                SelectedRaycastPos.SetActive(false);
                slotNumber = 0;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectedSlot = null;
            SelectedRaycastPos.SetActive(false);
            slotNumber = 0;
        }
    }

    public void SelectedSlotUpdate()
    {
        if (SelectedSlot == null)
        {
            return;
        }

        if (Physics.Raycast(RaycastOrigin.position, RaycastDirection.position - RaycastOrigin.position, out RaycastHit hit, Range, 1 << 10))
        {
            SelectedRaycastPos.SetActive(true);
            SelectedRaycastPos.transform.position = hit.point;

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (slotNumber == 1)
                {
                    SpawnStructure(true, hit.point, Quaternion.identity, true);
                }
                else if (slotNumber == 2)
                {
                    SpawnStructure(false, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal), true);
                }
            }
        }
        else
        {
            SelectedRaycastPos.SetActive(false);
        }
    }

    public void StartSpawning(List<Save.Obj> DetectorStructures, List<Save.Obj> TurretStructures)
    {
        for (int i = 0; i < DetectorStructures.Count; i++)
        {
            SpawnStructure(true, DetectorStructures[i].Pos.V(), DetectorStructures[i].Rot.Q(), false);
        }
        for (int i = 0; i < TurretStructures.Count; i++)
        {
            SpawnStructure(false, TurretStructures[i].Pos.V(), TurretStructures[i].Rot.Q(), false);
        }
    }

    public void SpawnStructure(bool isDetector, Vector3 pos, Quaternion rot, bool remove)
    {
        if (isDetector)
        {
            GameObject g = Instantiate(Slot1Prefab, pos, rot);
            DetectorStructures.Add(g);
            if (remove)
            {
                Slot1.RemoveAmount(1);
                if (Slot1.Item == null)
                {
                    SelectedSlot = null;
                    SelectedRaycastPos.SetActive(false);
                    slotNumber = 0;
                }
            }
        }
        else
        {
            GameObject g = Instantiate(Slot2Prefab, pos, rot);
            TurretStructures.Add(g);
            if (remove)
            {
                Slot2.RemoveAmount(1);
                if (Slot2.Item == null)
                {
                    SelectedSlot = null;
                    SelectedRaycastPos.SetActive(false);
                    slotNumber = 0;
                }
            }
        }
    }


    public void SetHealth(int health)
    {
        Health = health;
        UpdateBar();
    }

    public void IncreaseHealth(int health)
    {
        Health += health;
        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
        UpdateBar();
        return;
    }

    public void DecreaseHealth(int health)
    {
        Health -= health;
        if (Health <= 0)
        {
            Health = 0;
            SceneData.Instance.ScoreDescription = "You got blown up by drones!\nTip: Craft Turrets or faster Propellers!";
            SceneData.Instance.GoToScoreScreen();
        }
        UpdateBar();
    }

    private void UpdateBar()
    {
        HealthBar.value = Health;
    }

}
