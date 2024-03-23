using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{

    public static AudioController instanceAudio;
    public Slider sliderSom;
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
        sliderSom.value = PlayerPrefs.GetFloat("SliderVol");
        Debug.Log(sliderSom.value);
    }

    public void changeVol()
    {
        audioMixer.SetFloat("MusicVol", sliderSom.value);
        teste = sliderSom.value;
        PlayerPrefs.SetFloat("SliderVol", teste);
        
    }
}
