using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;
using UnityEngine.UI;

public class MusicScr : MonoBehaviour
{
    public Sound[] Sounds;
    public AudioMixerGroup MasterGroup, YobGroup;
    public AudioMixer Mixer;
    public AllData allData;
    public Slider VolumeSlider;

    void Awake() 
    {
        foreach (Sound s in Sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.loop = false;
            s.source.clip = s.Clip; 
            s.source.outputAudioMixerGroup = MasterGroup; 
        }
    }   

    void Start() 
    {
        VolumeSlider.value = allData.VolumeLock;
        Mixer.SetFloat("MasterVolume", VolumeSlider.value);
    } 

    public void GameSong(string Play)
    {
        Sound s = Array.Find(Sounds, Sound => Sound.Name == "Game");
        if(s != null)
        {
            if(Play == "Play")
            {
                s.source.Play();
                s.source.loop = true;
            }
            else if (Play == "Stop")
            {
                s.source.Stop();
                Debug.Log("stop");
            }
        }
    }
    public void Jump()
    {
        Sound s = Array.Find(Sounds, Sound => Sound.Name == "Jump");
        if(s != null)
            s.source.PlayOneShot(s.source.clip);
    }
    public void YobSong(string Play)
    {
        Sound s = Array.Find(Sounds, Sound => Sound.Name == "Yob");
        s.source.outputAudioMixerGroup = YobGroup; 
        if(s != null)
        {
            if(Play == "Play")
            {
                s.source.Play();
                s.source.loop = true;
            }
            else if (Play == "Stop")
            {
                s.source.Stop();
            }
        }  
    }
    public void MenuSong(string Play)
    {
        Sound s = Array.Find(Sounds, Sound => Sound.Name == "Menu");
        s.source.outputAudioMixerGroup = MasterGroup; 
        if(s != null)
        {
            if(Play == "Play")
            {
                s.source.Play();
                s.source.loop = true;
            }
            else if (Play == "Stop")
            {
                s.source.Stop();
            }
        }  
    }

    public void SetVolume (float volume)
    { 
        Mixer.SetFloat("MasterVolume", volume);
        if(volume <= -50)
        {
            Mixer.SetFloat("MasterVolume", -80);
        }
    }
     
}
