using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource fxSource;
    public AudioSource musicSource;
    public static SoundManager instance = null;

    public float highPitchRange = 1.15f;
    public float lowPitchRange = 0.85f;


    private void Awake()
    {
        
        if (instance == null)
            instance = this;
 
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }


public void PlaySomething(AudioClip clip)
    {
        musicSource.volume = 0;
        musicSource.clip = clip;
        musicSource.Play();
        StartCoroutine(FadeAudioSource.StartFade(musicSource, 5f, .5f));

    }

    public void ClipArrayVariation(params AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        fxSource.pitch = randomPitch;
        fxSource.clip = clips[randomIndex];

        fxSource.Play();
    }
}
