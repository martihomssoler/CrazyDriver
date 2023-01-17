using UnityEngine.Audio;
using System;
using UnityEngine;
using TWCore.Variables;

public class RandomAudioSourceHandler : MonoBehaviour
{
    public bool dontDestroyOnLoad = false;
    public bool playOnStart = false;
    public FloatReference masterVolume;
    public AudioClip[] clips;

    private AudioSource[] sources;

    private void Awake()
    {
        if (dontDestroyOnLoad) DontDestroyOnLoad(this);

        CreateAudioSources();
    }

    private void CreateAudioSources()
    {
        sources = new AudioSource[clips.Length];
        for (int i = 0; i < clips.Length; i++)
        {
            sources[i] = gameObject.AddComponent<AudioSource>();
            sources[i].clip = clips[i];
            sources[i].volume = masterVolume;
        }
    }


    public void PlayRandom()
    {
        if (clips.Length == 0) return;
        var i = UnityEngine.Random.Range(0, sources.Length);
        sources[i].Play();
    }
}
