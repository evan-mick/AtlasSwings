using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoundManager : MonoBehaviour
{

	// Singleton instance.
	public static SoundManager Instance = null;
	
	public AudioSource MusicSource;
	public AudioSource SFXSource;

	private float MusicVolume = 1f ;
	private float SFXVolume = 0.5f;

    
    public void setSFXVolume(float volume)
    {
		//SFXSource.volume = volume;
		SFXVolume = volume;
    }

	public void setMusicVolume(float volume)
	{
		//MusicSource.volume = volume;
		MusicVolume = volume;
	}
	// Initialize the singleton instance.
	private void Awake()
	{
		// If there is not already an instance of SoundManager, set it to this.
		if (Instance == null)
		{
			Instance = this;
		}
		//If an instance already exists, destroy whatever this object is to enforce the singleton.
		else if (Instance != this)
		{
			Destroy(gameObject);
		}
		//Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
		DontDestroyOnLoad(gameObject);
	}
	// Play a single clip through the sound effects source.
	public void PlaySFX(AudioSource source)
	{
		source.volume = SFXVolume;
		source.Play();

	}

	public void PlaySFXClip(AudioClip clip)
	{
		if (!SFXSource.isPlaying)
        {
			SFXSource.clip = clip;
			SFXSource.volume = SFXVolume;
			SFXSource.Play();

		}
	}
	// Play a single source through the music source.
	public void PlayMusic(AudioSource source)
	{
		
		source.volume = MusicVolume;
		source.Play();
        //MusicSource.source = source;
        //MusicSource.Play();
	}
	
	public void SetSourceVolume(AudioSource source)
    {
		source.volume = MusicVolume;
    }

	public float GetMusicVolume()
    {
		return MusicVolume;
    }

}