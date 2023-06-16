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


    public void Quit()
    {
        Application.Quit();
    }

    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadGame()
    {
        // TODO load?
        SceneManager.LoadScene(1);
    }

    public void Options()
    {
        OptionsMenu.SetActive(!OptionsMenu.activeSelf);
    }

    public void SetMasterVolume()
    {
        AudioMixer.SetFloat("MasterVolume", Mathf.Log10(MasterSlider.value) * 20);
    }
    public void SetMusicVolume()
    {
        AudioMixer.SetFloat("MusicVolume", Mathf.Log10(MusicSlider.value) * 20);
    }
    public void SetSoundsVolume()
    {
        AudioMixer.SetFloat("SoundsVolume", Mathf.Log10(SoundsSlider.value) * 20);
    }

}
