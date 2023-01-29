using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class VolumeControl : MonoBehaviour
{
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] GameObject SoundManager;
    // Start is called before the first frame update

    private float currSfx = 0;
    private float currMusic = 0;

    // Update is called once per frame
    void Update()
    {
        var newSfx = sfxSlider.value;
        var newMusic = musicSlider.value;

        if (newSfx != currSfx)
        {
            SoundManager.GetComponent<SoundManager>().setSFXVolume(newSfx);
        }

        if (newMusic != currMusic)
        {
            SoundManager.GetComponent<SoundManager>().setMusicVolume(newMusic);
        }

        currSfx = newSfx;
        currMusic = newMusic;


    }
}
