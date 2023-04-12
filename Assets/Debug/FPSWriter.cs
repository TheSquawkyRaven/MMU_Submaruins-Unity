using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSWriter : MonoBehaviour
{

    public TextMeshProUGUI fpsText;

    private void Update()
    {
        fpsText.SetText($"FPS: {Mathf.RoundToInt(1 / Time.deltaTime)}");
    }
}
