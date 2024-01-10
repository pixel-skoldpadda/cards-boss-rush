using Data;

namespace Infrastructure.Services.Audio
{
    public class AudioService : IAudioService
    {
        private AudioSettings _settings;

        public AudioSettings Settings
        {
            get => _settings;
            set => _settings = value;
        }
    }
}