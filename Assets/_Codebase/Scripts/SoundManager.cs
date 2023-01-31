using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoundManager : MonoBehaviour
{

	// Singleton instance.
	public static SoundManager Instance = null;
	
	public AudioSource SFXSource;

	private float MusicVolume = 1f ;
	private float SFXVolume = 0.5f;


	private AudioSource musicPlaying;
	private AudioSource SFXPlaying;

	public void setSFXVolume(float volume)
    {
		SFXVolume = volume;
    }

	public void setMusicVolume(float volume)
	{
		MusicVolume = volume;
		if (musicPlaying != null)
        {
			musicPlaying.volume = volume;
        }
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
	public void PlaySFX(AudioSource source, float delay)
	{
		if (delay == 0)
        {
			source.volume = SFXVolume;
			source.Play();
        }
        else
        {
			source.PlayDelayed(delay);
		}
	}

	
	public void PlaySFXButton(AudioSource source)
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
		if (musicPlaying != null)
        {
			musicPlaying.Stop();
        }	

		source.volume = MusicVolume;
		source.Play();
		musicPlaying = source;
	}

	public void SaveSFX(AudioSource source)
    {
		SFXPlaying = source;
    }
  
	public IEnumerator FadeOutSFX()
    {
		if (SFXSource != null){
			var targetVolume = 0;
			var duration = .5f;

			float currentTime = 0;
			float start = SFXPlaying.volume;
			while (currentTime < duration)
			{
				currentTime += Time.deltaTime;
				SFXPlaying.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
				yield return null;
			}
			
			SFXSource = null;
			yield break;

		}
	}

	public void SFXInterrupt()
    {
		if (SFXPlaying != null){
		SFXPlaying.Stop();

		}
    }

	public void StopMusic(AudioSource source){
		source.Stop();
	}

}