using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSliders : MonoBehaviour
{
    [Header("Audio Slider Set Up")]
    public AudioMixer theMixer;

    [Tooltip("The audio source for the sfx volume check sound")]
    public AudioSource volumeCheckSound;

    [Header("Slider Text Value")]

    [Tooltip("The text object for the Master Mixer value from 0 - 100")]
    public TMP_Text mastLabel;
    [Tooltip("The text object for the Music Mixer Group value from 0 - 100")]
    public TMP_Text musicLabel;
    [Tooltip("The text object for the SFX Mixer Group value from 0 - 100")]
    public TMP_Text sfxLabel;

    [Header("Volume Sliders")]

    [Tooltip("The slider game object for Master Mixer")]
    public Slider mastSlider;
    [Tooltip("The slider game object for Music Mixer Group")]
    public Slider musicSlider;
    [Tooltip("The slider game object for SFX Mixer Group")]
    public Slider sfxSlider;

    bool SFXSliderChanged = false;

    // Start is called before the first frame update
    void Start()
    {
        //gets the Player Pref volume from the mixer and converts it to linear for the slider value
        float vol = 0f;
        theMixer.GetFloat("MasterVol", out vol);
        mastSlider.value = Mathf.Pow(10, (vol / 20));
        theMixer.GetFloat("MusicVol", out vol);
        musicSlider.value = Mathf.Pow(10, (vol / 20));
        theMixer.GetFloat("SFXVol", out vol);
        sfxSlider.value = Mathf.Pow(10, (vol / 20));

        //sets the slider value using the previous conversion from Player Prefs
        mastLabel.text = Mathf.RoundToInt(mastSlider.value * 100).ToString();
        musicLabel.text = Mathf.RoundToInt(musicSlider.value * 100).ToString();
        sfxLabel.text = Mathf.RoundToInt(sfxSlider.value * 100).ToString();
        SFXSliderChanged = false;
    }

    public void SetMasterVol(float mastSliderVol)
    {
        // reads the slider value, converts the slider 0 - 1 value to 0 - 100 and sets the slider text
        mastLabel.text = Mathf.RoundToInt(mastSlider.value * 100).ToString();

        //sets the mixer value after converting to exponential
        theMixer.SetFloat("MasterVol", Mathf.Log10(mastSliderVol) * 20);

        //stores the value in PlayerPrefs to save the value across playthroughs
        PlayerPrefs.SetFloat("MasterVol", mastSlider.value);
    }

    //repeat for other sliders
    public void SetMusicVol(float musicSliderVol)
    {
        musicLabel.text = Mathf.RoundToInt(musicSlider.value * 100).ToString();

        theMixer.SetFloat("MusicVol", Mathf.Log10(musicSliderVol) * 20);

        PlayerPrefs.SetFloat("MusicVol", musicSlider.value);
    }

    public void SetSFXVol(float SFXSliderVol)
    {
        sfxLabel.text = Mathf.RoundToInt(sfxSlider.value * 100).ToString();

        theMixer.SetFloat("SFXVol", Mathf.Log10(SFXSliderVol) * 20);

        PlayerPrefs.SetFloat("SFXVol", sfxSlider.value);

        SFXSliderChanged = true;
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0) && SFXSliderChanged == true)
        {
            volumeCheckSound.Play();
            SFXSliderChanged = false;
        }
    }
}
