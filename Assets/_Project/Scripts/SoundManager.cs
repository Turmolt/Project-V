using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource fxSource;
    public AudioSource musicSource;
    public static SoundManager instance = null;

    public float highPitchRange = 1.05f;
    public float lowPitchRange = 0.95f;


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
        fxSource.clip = clip;

        fxSource.Play();

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
