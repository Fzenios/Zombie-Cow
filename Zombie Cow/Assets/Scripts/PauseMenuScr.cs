using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuScr : MonoBehaviour
{
    bool PauseMenuBool;
    public GameObject PauseMenuObj;
    public GameObject PauseBtn;
    public AllData allData;
    public Slider VolumeSlider;
    void Start() 
    {
        PauseMenuBool = false;        
    }

    public void PauseMenu()
    {
        ChangePause();

        if(PauseMenuBool)
        {
            StartCoroutine(IntoMenu());
        }
        else
        {
            PauseBtn.SetActive(true);
            Time.timeScale = 1;
            PauseMenuObj.SetActive(false);
        }
    }
    public void ChangePause()
    {
        PauseMenuBool =! PauseMenuBool;
    }
    
    IEnumerator IntoMenu()
    {
        yield return new WaitForSeconds(0.5f);
        
        Time.timeScale = 0;
        PauseMenuObj.SetActive(true);
        PauseBtn.SetActive(false);
    }
    public void ExitGame()
    {
        Time.timeScale = 1;
        allData.VolumeLock = VolumeSlider.value;
        SceneManager.LoadScene(0);
    }
}
