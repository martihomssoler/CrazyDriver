using UnityEngine.Audio;
using System;
using UnityEngine;
using TWCore.Variables;

namespace TWCore.Audio
{
    public class AudioSourceHandler: MonoBehaviour
    {
        public bool dontDestroyOnLoad = false;
        public bool playOnStart = false;
        public FloatReference masterVolume;
        public Sound[] sounds;

        private void Awake()
        {
            if (dontDestroyOnLoad) DontDestroyOnLoad(this);

            CreateAudioSources();
        }

        private void CreateAudioSources()
        {
            foreach (var s in sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;

                s.source.volume = masterVolume;
                s.source.pitch = s.pitch;

                s.source.loop = s.loop;
            }
        }

        private void Start()
        {
            if (!playOnStart) return;
            foreach (var s in sounds)
            {
                s.source.Play();
            }
        }

        public void Play(string name)
        {
            var s = Array.Find(sounds, sound => sound.name == name);
            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found!");
                return;
            }
            s.source.Play();
        }

        public void PlayRandom()
        {
            if (sounds.Length == 0) return;
            var i = UnityEngine.Random.Range(0, sounds.Length);
            sounds[i].source.Play();
        }
    }
}