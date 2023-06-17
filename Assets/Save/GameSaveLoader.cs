using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSaveLoader : MonoBehaviour
{
    private static GameSaveLoader instance;
    public static GameSaveLoader Instance => instance;
    

    public SceneData SceneData => SceneData.Instance;
    public Save.Data SaveData => SceneData?.LoadingData;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (SaveData == null)
        {
            Debug.Log("No SaveData");
            GarbageManager.Instance.StartSpawning();
            DroneManager.Instance.StartSpawning();

            Score.Instance.UpdateScore(true);
            return;
        }
        else
        {

            Player.Instance.transform.position = SaveData.PlayerPos.V();
            Player.Instance.transform.rotation = SaveData.PlayerRot.Q();
            Player.Instance.SetHealth(SaveData.Health);

            GarbageManager.Instance.StartSpawning(SaveData.MetalGarbages, SaveData.PlasticGarbages);
            DroneManager.Instance.StartSpawning(SaveData.Drones);

            Player.Instance.StartSpawning(SaveData.DetectorStructure, SaveData.TurretStructure);

            PlayerInventory.Instance.Storage.LoadSave(SaveData.Storage);
            PlayerInventory.Instance.Equipment.LoadSave(SaveData.Equipment);
            PlayerInventory.Instance.Toolbar.LoadSave(SaveData.Toolbar);

            Score.Instance.UpdateScore(true);

            Debug.Log("Load");

        }

    }

    public void SaveGame()
    {
        Debug.Log("Save");
        Save.Instance.SaveRequest();
    }

}
