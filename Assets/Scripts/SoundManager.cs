using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip OverworldMusic;
    public AudioClip encounterMusicOne;
    public AudioClip encounterMusicTwo;
    public AudioClip encounterMusicThree;
    public AudioClip encounterMusicFour;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeIn(audioSource, 0.70f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;

        audioSource.clip = OverworldMusic;
        StartCoroutine(FadeIn(audioSource, 1.0f));
    }

    IEnumerator FadeIn(AudioSource audioSource, float FadeTime)
    {
        audioSource.volume = 0;
        float startVolume = 0.2f;
        audioSource.Play();

        while (audioSource.volume < 1.0f)
        {
            audioSource.volume += startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.volume = 1f;
    }

    public void FadeOutEncounter()
    {
        StartCoroutine(FadeOut(audioSource, 0.70f));
    }
    public void PlayEncounterMusic()
    {

        Random.InitState((int)System.DateTime.Now.Ticks);
        int i = Random.Range(0, 3);

        if(i == 0)
        {
            audioSource.clip = encounterMusicOne;
            StartCoroutine(FadeIn(audioSource, 0.70f));

        }
        else if(i==1)
        {
            audioSource.clip = encounterMusicTwo;
            StartCoroutine(FadeIn(audioSource, 0.70f));

        }
        else if (i == 2)
        {
            audioSource.clip = encounterMusicThree;
            StartCoroutine(FadeIn(audioSource, 0.70f));

        }
        else 
        {
            audioSource.clip = encounterMusicFour;
            StartCoroutine(FadeIn(audioSource, 0.70f));
     
        }
    }
}
