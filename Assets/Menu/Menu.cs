using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

    public GameObject Title;
    public GameObject OptionsMenu;

    public AudioMixer AudioMixer;
    public Slider MasterSlider;
    public Slider MusicSlider;
    public Slider SoundsSlider;


    private void Start()
    {
        float masterVol = PlayerPrefs.GetFloat("MasterVolume", 1f);
        float musicVol = PlayerPrefs.GetFloat("MusicVolume", 1f);
        float soundsVol = PlayerPrefs.GetFloat("SoundsVolume", 1f);
        MasterSlider.value = masterVol;
        MusicSlider.value = musicVol;
        SoundsSlider.value = soundsVol;
        SetMasterVolume();
        SetMusicVolume();
        SetSoundsVolume();
    }


    public void Quit()
    {
        Application.Quit();
    }

    public void NewGame()
    {
        SceneData.Instance.LoadingData = null;
        SceneManager.LoadScene(1);
    }
    public void LoadGame()
    {
        Save.Instance.LoadFromFile();
        SceneManager.LoadScene(1);
    }

    public void Options()
    {
        OptionsMenu.SetActive(!OptionsMenu.activeSelf);
    }

    public void SetMasterVolume()
    {
        float vol = Mathf.Log10(MasterSlider.value) * 20;
        AudioMixer.SetFloat("MasterVolume", vol);
        PlayerPrefs.SetFloat("MasterVolume", MasterSlider.value);
        PlayerPrefs.Save();
    }
    public void SetMusicVolume()
    {
        float vol = Mathf.Log10(MusicSlider.value) * 20;
        AudioMixer.SetFloat("MusicVolume", vol);
        PlayerPrefs.SetFloat("MusicVolume", MusicSlider.value);
        PlayerPrefs.Save();
    }
    public void SetSoundsVolume()
    {
        float vol = Mathf.Log10(SoundsSlider.value) * 20;
        AudioMixer.SetFloat("SoundsVolume", vol);
        PlayerPrefs.SetFloat("SoundsVolume", SoundsSlider.value);
        PlayerPrefs.Save();
    }

}
