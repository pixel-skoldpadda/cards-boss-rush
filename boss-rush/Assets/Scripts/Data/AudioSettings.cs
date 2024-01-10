using System;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class AudioSettings
    {
        [SerializeField] private float effectsVolume;
        [SerializeField] private float musicVolume;

        public AudioSettings()
        {
            effectsVolume = .5f;
            musicVolume = .5f;
        }
        
        public float EffectsVolume
        {
            get => effectsVolume;
            set => effectsVolume = value;
        }

        public float MusicVolume
        {
            get => musicVolume;
            set => musicVolume = value;
        }
    }
}