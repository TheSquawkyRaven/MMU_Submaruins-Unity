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
        GarbageText.SetText($"Garbage Removed: {GarbageManager.Instance.collectedAmount}/{GarbageManager.Instance.TotalAmount}");
        DronesText.SetText($"Drones Destroyed: {DroneManager.Instance.destroyedAmount}/{DroneManager.Instance.amount}");

        if (GarbageManager.Instance.collectedAmount == GarbageManager.Instance.TotalAmount && DroneManager.Instance.destroyedAmount == DroneManager.Instance.amount)
        {
            SceneData.Instance.ScoreDescription = "You restored the ocean completely 100%!\nCONGRATULATIONS!";
            SceneData.Instance.GoToScoreScreen();
        }
        SceneData.Instance.Score = score;
    }

    public int GetScore()
    {
        return GarbageManager.Instance.collectedAmount * GarbageScoreMult + DroneManager.Instance.destroyedAmount * DroneScoreMult;
    }


}
