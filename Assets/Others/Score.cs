using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{

    private static Score instance;
    public static Score Instance => instance;

    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI GarbageText;
    public TextMeshProUGUI DronesText;

    public int GarbageScoreMult;
    public int DroneScoreMult;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        UpdateScore();
    }

    public void UpdateScore()
    {
        int score = GetScore();
        ScoreText.SetText($"Score: {score}");
        GarbageText.SetText($"Garbage Removed: {GarbageManager.Instance.collectedAmount}/{GarbageManager.Instance.amount}");
        DronesText.SetText($"Drones Destroyed: {DroneManager.Instance.destroyedAmount}/{DroneManager.Instance.amount}");
    }

    public int GetScore()
    {
        return GarbageManager.Instance.collectedAmount * GarbageScoreMult + DroneManager.Instance.destroyedAmount * DroneScoreMult;
    }


}
