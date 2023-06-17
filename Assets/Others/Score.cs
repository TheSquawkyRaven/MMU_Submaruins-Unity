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
        UpdateScore(true);
    }

    public void UpdateScore(bool firstLoad = false)
    {
        int score = GetScore();
        ScoreText.SetText($"Score: {score}");
        GarbageText.SetText($"Garbage Removed: {GarbageManager.Instance.CollectedAmount}/{GarbageManager.Instance.TotalAmount}");
        DronesText.SetText($"Drones Destroyed: {DroneManager.Instance.DestroyedAmount}/{DroneManager.Instance.TotalAmount}");

        if (firstLoad)
        {
            return;
        }
        if (GarbageManager.Instance.CollectedAmount == GarbageManager.Instance.TotalAmount && DroneManager.Instance.DestroyedAmount == DroneManager.Instance.TotalAmount)
        {
            SceneData.Instance.ScoreDescription = "You restored the ocean completely 100%!\nCONGRATULATIONS!";
            SceneData.Instance.GoToScoreScreen();
        }
        if (SceneData.Instance != null)
        {
            SceneData.Instance.Score = score;
        }
        if (GameSaveLoader.Instance != null)
        {
            GameSaveLoader.Instance.SaveGame();
        }
    }

    public int GetScore()
    {
        return GarbageManager.Instance.CollectedAmount * GarbageScoreMult + DroneManager.Instance.DestroyedAmount * DroneScoreMult;
    }


}
