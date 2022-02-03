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
    } 

    public void IntroSong()
    {
        Sound s = Array.Find(Sounds, Sound => Sound.Name == "Intro");
        if(s != null)
        {
            s.source.Play();
            s.source.loop = true;
        }    
    }
    public void MainGameSong(string Play)
    {
        Sound s = Array.Find(Sounds, Sound => Sound.Name == "Main");
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
    public void CarHorn()
    {
        Sound s = Array.Find(Sounds, Sound => Sound.Name == "CarHorn");
        if(s != null)
        {
            s.source.volume = 250;
            s.source.PlayOneShot(s.source.clip);
        }
    }
    public void Cat()
    {
        Sound s = Array.Find(Sounds, Sound => Sound.Name == "Cat");
        if(s != null)
            s.source.PlayOneShot(s.source.clip);
    }
    public void Coin()
    {
        Sound s = Array.Find(Sounds, Sound => Sound.Name == "Coin");
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
    public void Dog()
    {
        Sound s = Array.Find(Sounds, Sound => Sound.Name == "Dog");
        if(s != null)
            s.source.PlayOneShot(s.source.clip);
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
