using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDataSpawner : MonoBehaviour
{
    private void Start()
    {
        if (SceneData.Instance != null)
        {
            return;
        }
        GameObject sd = Instantiate(new GameObject("Scene Data"));
        sd.AddComponent<SceneData>();
        DontDestroyOnLoad(sd);
    }
}
