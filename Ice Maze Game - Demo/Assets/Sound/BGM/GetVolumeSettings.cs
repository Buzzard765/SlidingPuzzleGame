using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetVolumeSettings : MonoBehaviour
{
    public AudioSource MusicVolume;
    // Start is called before the first frame update
    void Start()
    {
        MusicVolume = GetComponent<AudioSource>();
        LoadConfig();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadConfig()
    {
        if (PlayerPrefs.HasKey("BGMValue"))
        {
            MusicVolume.volume = PlayerPrefs.GetFloat("BGMValue");
        }
    }
}
