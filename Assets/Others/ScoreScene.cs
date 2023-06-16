using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreScene : MonoBehaviour
{

    public TextMeshProUGUI ScoreNumberText;
    public TextMeshProUGUI ScoreDescriptionText;


    private void Start()
    {
        ScoreNumberText.SetText($"Score: {SceneData.Instance.Score}");
        ScoreDescriptionText.SetText(SceneData.Instance.ScoreDescription);
    }

}
