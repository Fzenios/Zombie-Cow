using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenuScr : MonoBehaviour
{
    MusicScr musicScr;
    public AllData allData;
    public Slider VolumeSlider;
    void Start()
    {
        musicScr = GetComponent<MusicScr>();
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
