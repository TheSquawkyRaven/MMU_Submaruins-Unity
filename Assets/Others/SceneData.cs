using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneData : MonoBehaviour, Save.ISaver
{
    private static SceneData instance;
    public static SceneData Instance => instance;

    public Save.Data LoadingData;

    public int Score;
    public string ScoreDescription;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        instance = this;
        StartInit();
    }

    public void GoToScoreScreen()
    {
        SceneManager.LoadScene(2);
    }

    public void StartInit()
    {
        Save.Instance.Init();
        Save.Instance.InitSaver(this);
    }

    public void SaveData(Save.Data data)
    {
        data.PlayerPos = Player.Instance.transform.position;
        data.PlayerRot = Player.Instance.transform.rotation;
        data.Health = Player.Instance.Health;

        data.MetalGarbages = GarbageManager.Instance.GetSave(true);
        data.PlasticGarbages = GarbageManager.Instance.GetSave(false);
        data.Drones = DroneManager.Instance.GetSave();

        data.DetectorStructure = Player.Instance.GetSave(true);
        data.TurretStructure = Player.Instance.GetSave(false);

        data.Storage = PlayerInventory.Instance.Storage.GetSave();
        data.Equipment = PlayerInventory.Instance.Equipment.GetSave();
        data.Toolbar = PlayerInventory.Instance.Toolbar.GetSave();
    }

    public void LoadData(Save.Data data)
    {
        LoadingData = data;
    }
}
