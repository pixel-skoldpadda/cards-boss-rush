using Data;

namespace Infrastructure.Services.SaveLoad
{
    public interface ISaveLoadService
    {
        void SaveGameState();
        GameState LoadGameState();
        void SaveAudioSettings();
        AudioSettings LoadAudioSettings();
    }
}