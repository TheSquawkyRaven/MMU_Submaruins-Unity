using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneData : MonoBehaviour
{
    private static SceneData instance;
    public static SceneData Instance => instance;



    public int Score;
    public string ScoreDescription;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        instance = this;
    }

    public void GoToScoreScreen()
    {
        SceneManager.LoadScene(2);
    }

}
