using Configs;
using Data;
using Infrastructure.Services.Audio;
using Infrastructure.Services.State;
using UnityEngine;
using Zenject;
using AudioSettings = Data.AudioSettings;

namespace Infrastructure.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private readonly IGameStateService _gameStateService;
        private readonly IAudioService _audioService;

        [Inject]
        public SaveLoadService(IGameStateService gameStateService, IAudioService audioService)
        {
            _gameStateService = gameStateService;
            _audioService = audioService;
        }
        
        public void SaveGameState()
        {
            string json = _gameStateService.State.ToJson();
            PlayerPrefs.SetString(DataConfig.GameStatePrefsKey, json);
            
            Debug.Log($"Game state saved {json}");
        }

        public GameState LoadGameState()
        {
            var json = PlayerPrefs.GetString(DataConfig.GameStatePrefsKey);
            if (!string.IsNullOrEmpty(json))
            {
                Debug.Log($"Game state loaded {json}");
                return  json.ToDeserialized<GameState>();
            }
            return null;
        }
        
        public void SaveAudioSettings()
        {
            var settings = _audioService.Settings;
            
            var json = settings.ToJson();
            PlayerPrefs.SetString(DataConfig.AudioSettingsPrefsKey, json);
            
            Debug.Log($"Audio settings saved {json}");
        }

        public AudioSettings LoadAudioSettings()
        {
            var json = PlayerPrefs.GetString(DataConfig.AudioSettingsPrefsKey);
            if (!string.IsNullOrEmpty(json))
            {
                Debug.Log($"Audio settings loaded {json}");
                
                return json.ToDeserialized<AudioSettings>();
            }
            return null;
        }
    }
}