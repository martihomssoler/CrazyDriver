using UnityEngine.Audio;
using UnityEngine;
using System;

namespace TWCore.Audio
{
    [Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;

        //[Range(0f, 1f)]
        //public float volume;
        [Range(0.1f, 3f)]
        public float pitch = 1.3f;

        public bool loop;

        [HideInInspector]
        public AudioSource source;
    }
}
