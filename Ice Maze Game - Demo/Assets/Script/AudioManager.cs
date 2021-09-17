using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Slider MasterVolumeSlider;
    public Slider BGMSlider;
    public Slider SFXSlider;
    public static GameObject SliderSetting;
    public LevelSelector SoundData;

    public AudioMixer BGM;
    public AudioMixer SFX;
    public AudioSource AllBGMSource;
    public AudioSource AllSFXSource;

    private void Awake()
    {
     //   AllBGMSource = GameObject.Find("BGM").GetComponent<AudioSource>();
     //   AllSFXSource = GameObject.Find("SFX").GetComponent<AudioSource>();
    }
        // Start is called before the first frame update
        void Start()
    {
        //SoundData = GameObject.Find("Level Selector").GetComponent<LevelSelector>();
        //SliderSetting = GameObject.Find("Sound Manager");
        /*try {
            SoundData.LoadGame();
        }
        catch {
            RefreshSettings();
        }*/
           AllBGMSource = GameObject.Find("BGM").GetComponent<AudioSource>();
           AllSFXSource = GameObject.Find("SFX").GetComponent<AudioSource>();
        if (AllBGMSource && AllSFXSource != null)
        {
            LoadConfig();
        }
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }


    public void SetMasterVolume(float SliderValue) {
        AudioListener.volume = MasterVolumeSlider.value;
        //MasterVolumeSlider.value = SliderValue;
        //PlayerPrefs.SetFloat("MasterVolumeValue", SliderValue);
    }

    public void Setbgm(float SliderValue)
    {
        //BGM.SetFloat("BGMVolume", Mathf.Log10(SliderValue) * 20);
        AllBGMSource.volume = BGMSlider.value;
        //BGMSlider.value = SliderValue;
        PlayerPrefs.SetFloat("BGMValue", BGMSlider.value);      
    }

    public void SetSFX(float SliderValue)
    {
        //SFX.SetFloat("SFXVolume", Mathf.Log10(SliderValue) * 20);
        AllSFXSource.volume = SFXSlider.value;
        //SFXSlider.value = SliderValue;
        PlayerPrefs.SetFloat("SFXValue", SFXSlider.value);
    }

    public void SaveConfig() {
        PlayerPrefs.SetFloat("MasterValue", MasterVolumeSlider.value);
        PlayerPrefs.SetFloat("BGMValue", BGMSlider.value);
        PlayerPrefs.SetFloat("SFXValue", SFXSlider.value);
        PlayerPrefs.Save();
        /*SoundData.MasterVolumeValue = MasterVolumeSlider.value;
         SoundData.BGMValue = BGMSlider.value;
         SoundData.SFXValue = SFXSlider.value;
         SetMasterVolume(AudioListener.volume);
         Setbgm(AllBGMSource.volume);
         SetSFX(AllSFXSource.volume);
         SoundData.SaveGame();*/
        //SaveSystem.SaveGame(this);
        //PrefabUtility.SaveAsPrefabAsset(SliderSetting, "Prefabs");
    }

    public void LoadConfig() {
        MasterVolumeSlider.value = PlayerPrefs.GetFloat("MasterValue", MasterVolumeSlider.value);
        BGMSlider.value = PlayerPrefs.GetFloat("BGMValue", BGMSlider.value);
        SFXSlider.value = PlayerPrefs.GetFloat("SFXValue", SFXSlider.value);
        
        
        /*SoundData.MasterVolumeValue = MasterVolumeSlider.value;
        SoundData.BGMValue = BGMSlider.value;
        SoundData.SFXValue = SFXSlider.value;
        SetMasterVolume(AudioListener.volume);
        Setbgm(AllBGMSource.volume);
        SetSFX(AllSFXSource.volume);*/
    }
}
