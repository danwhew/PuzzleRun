using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{

    public static AudioController instanceAudio;
    public Slider sliderMasterVolume;
    public Slider sliderSFXVolume;
    public Slider sliderMusicVolume;
    public AudioMixer audioMixer;

    public float teste;

    // Start is called before the first frame update
    void Awake()
    {


        if (instanceAudio == null)
        {
            instanceAudio = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        sliderMasterVolume.value = PlayerPrefs.GetFloat("SliderMasterVolume");
        sliderMusicVolume.value = PlayerPrefs.GetFloat("SliderMusicVolume");
        sliderSFXVolume.value = PlayerPrefs.GetFloat("SliderSFXVolume");
      //  sliderSFXVolume.value = PlayerPrefs.GetFloat("SliderSFXVolume");
        Debug.Log(sliderMasterVolume.value);
    }

    public void changeMasterVol()
    {
        audioMixer.SetFloat("MasterVol", sliderMasterVolume.value);
        //teste = sliderSom.value;
        PlayerPrefs.SetFloat("SliderMasterVolume", sliderMasterVolume.value);
        
    }

    public void changeMusicVol()
    {
        audioMixer.SetFloat("MusicVol", sliderMusicVolume.value);
        //  teste = sliderSom.value;
        PlayerPrefs.SetFloat("SliderMusicVolume", sliderMusicVolume.value);

    }

    public void changeSFXVol()
    {
        audioMixer.SetFloat("SFXVol", sliderSFXVolume.value);
        //  teste = sliderSom.value;
        PlayerPrefs.SetFloat("SliderSFXVolume", sliderSFXVolume.value);

    }
}
