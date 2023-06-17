using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using Unity.VisualScripting;
using System;
using System.ComponentModel;

public class Save
{
    private readonly static Save instance = new();
    public static Save Instance => instance;


    private readonly string savePath = Application.persistentDataPath + "/save.json";
    private readonly List<ISaver> savers = new();
    private Data saveData;

    private bool hasFirstLoaded = false;

    public interface ISaver
    {
        void StartInit();
        void SaveData(Data data);
        void LoadData(Data data);
    }

    [Serializable]
    public struct V3
    {
        [JsonProperty("X")]
        public float X { get; set; }
        [JsonProperty("Y")]
        public float Y { get; set; }
        [JsonProperty("Z")]
        public float Z { get; set; }

        public V3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3 V()
        {
            return new(X, Y, Z);
        }
        public Quaternion Q()
        {
            return Quaternion.Euler(X, Y, Z);
        }

        public static implicit operator V3(Vector3 v)
        {
            return new V3(v.x, v.y, v.z);
        }
        public static implicit operator V3(Quaternion q)
        {
            return q.eulerAngles;
        }
    }

    [Serializable]
    public struct Obj
    {
        [JsonProperty("Pos")]
        public V3 Pos { get; set; }

        [JsonProperty("Rot")]
        public V3 Rot { get; set; }

        [JsonProperty("Dat")]
        public int Dat { get; set; }

    }

    [Serializable]
    public class Data
    {

        [JsonProperty("PlayerPos")]
        public V3 PlayerPos { get; set; }
        [JsonProperty("PlayerRot")]
        public V3 PlayerRot { get; set; }
        [JsonProperty("PlayerHealth")]
        public int Health { get; set; }

        [JsonProperty("MetalGarbages")]
        public List<Obj> MetalGarbages { get; set; }
        [JsonProperty("PlasticGarbages")]
        public List<Obj> PlasticGarbages { get; set; }

        [JsonProperty("Drones")]
        public List<Obj> Drones { get; set; }


        [JsonProperty("DetectorStructure")]
        public List<Obj> DetectorStructure { get; set; }
        [JsonProperty("TurretStructure")]
        public List<Obj> TurretStructure { get; set; }


        [JsonProperty("Storage")]
        public List<(int, ItemData)> Storage { get; set; }
        [JsonProperty("Equipment")]
        public List<(int, ItemData)> Equipment { get; set; }
        [JsonProperty("Toolbar")]
        public List<(int, ItemData)> Toolbar { get; set; }




    }

    private Save()
    {
    }

    public void Init()
    {
        savers.Clear();
    }

    public void InitSaver(ISaver saver)
    {
        savers.Add(saver);
    }

    public void SaveRequest()
    {
        //TODO queue based on number of requests, don't save every time there is a request
        SaveToFile();
    }

    public void Delete()
    {
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
        }
        LoadFromFile();
    }

    private void SaveToFile()
    {
        if (saveData == null)
        {
            saveData = new();
        }
        for (int i = 0; i < savers.Count; i++)
        {
            savers[i].SaveData(saveData);
        }

        string text = JsonConvert.SerializeObject(saveData);
        File.WriteAllText(savePath, text);
    }

    public void LoadFromFile()
    {
        saveData = null;
        try
        {
            string text = File.ReadAllText(savePath);
            saveData = JsonConvert.DeserializeObject<Data>(text);
        }
        catch (Exception)
        {
            //Debug.LogError(e);
        }
        saveData ??= new();

        for (int i = 0; i < savers.Count; i++)
        {
            savers[i].LoadData(saveData);
        }
    }


}
