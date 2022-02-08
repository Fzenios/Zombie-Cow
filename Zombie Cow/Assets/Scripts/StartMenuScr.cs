using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class StartMenuScr : MonoBehaviour
{
    MusicScr musicScr;
    public AllData allData;
    public Slider VolumeSlider;
    public AudioMixer Mixer;
    void Start()
    {
        VolumeSlider.value = allData.VolumeLock;
        Mixer.SetFloat("MasterVolume", VolumeSlider.value);
        musicScr = GetComponent<MusicScr>();
        musicScr.MenuSong("Play");
    }

    public void NewGame()
    {
        allData.NewGame();
        allData.VolumeLock = VolumeSlider.value;
        SceneManager.LoadScene(1);
    }
    public void Continue()
    {
        allData.VolumeLock = VolumeSlider.value;
        SceneManager.LoadScene(1);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
