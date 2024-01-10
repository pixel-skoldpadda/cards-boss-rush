using Infrastructure.Services.Audio;
using Infrastructure.Services.SaveLoad;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Zenject;
using AudioSettings = Data.AudioSettings;

namespace Ui.Menu
{
    public class AudioSettingsView : MonoBehaviour
    {
        private const string MusicVolume = "MusicVolume";
        private const string SoundsVolume = "SoundsVolume";

        [SerializeField] private AudioMixerGroup musicAudioMixerGroup;
        [SerializeField] private AudioMixerGroup soundsAudioMixerGroup;
        
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider effectsSlider;

        [SerializeField] private TextMeshProUGUI musicVolume;
        [SerializeField] private TextMeshProUGUI effectsVolume;

        [SerializeField] private GameObject menu;

        private IAudioService _audioService;
        private ISaveLoadService _saveLoad;

        [Inject]
        public void Construct(IAudioService audioService, ISaveLoadService saveLoad)
        {
            _audioService = audioService;
            _saveLoad = saveLoad;
        }

        private void Start()
        {
            AudioSettings settings = _audioService.Settings;
            musicSlider.value = settings.MusicVolume;
            effectsSlider.value = settings.EffectsVolume;

            ChangeMusicVolume();
            ChangeEffectsVolume();
        }

        public void ChangeMusicVolume()
        {
            ChangeTextVolume(musicSlider, musicVolume);

            float value = musicSlider.value;
            _audioService.Settings.MusicVolume = value;
            
            musicAudioMixerGroup.audioMixer.SetFloat(MusicVolume, Mathf.Log10(value) * 20f);
        }

        public void ChangeEffectsVolume()
        {
            ChangeTextVolume(effectsSlider, effectsVolume);

            float value = effectsSlider.value;
            _audioService.Settings.EffectsVolume = value;
            
            soundsAudioMixerGroup.audioMixer.SetFloat(SoundsVolume, Mathf.Log10(value) * 20f);
        }

        public void OnBackButtonClicked()
        {
            gameObject.SetActive(false);
            menu.SetActive(true);
        }

        private void ChangeTextVolume(Slider slider, TextMeshProUGUI text)
        {
            text.text = $"{(int)(slider.value * 100)}";
        }

        private void OnDestroy()
        {
            _saveLoad.SaveAudioSettings();
        }
    }
}